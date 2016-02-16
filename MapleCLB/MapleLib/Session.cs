using System;
using System.Diagnostics;
using System.Net.Sockets;
using MapleCLB.MapleLib.Crypto;
using MapleCLB.MapleLib.Packet;


namespace MapleCLB.MapleLib {
    public sealed class Session {
        private static readonly Random Random = new Random();
        public const short RECEIVE_SIZE = 1024;
        private const int HANDSHAKE_HEADER_SIZE = 2;
        private const int PACKET_HEADER_SIZE = 4;

        private readonly Socket Socket;

        public bool Connected { get; private set; }
        public bool Encrypted { get; private set; }

        public SessionType SessionType { get; }

        private MapleCipher ClientCipher;
        private MapleCipher ServerCipher;

        private readonly object SendLock;
        private readonly byte[] RecvBuffer;
        private byte[] PacketBuffer;
        private int Cursor;

        public event EventHandler<ServerInfo> OnHandshake;
        public event EventHandler<byte[]> OnPacket;
        public event EventHandler OnDisconnected;
        public event EventHandler<Session> OnReconnect;

        internal Session(Socket socket, SessionType type) {
            Socket = socket;
            SessionType = type;

            Encrypted = type != SessionType.CLIENT;
            Connected = true;

            SendLock = new object();
            PacketBuffer = new byte[RECEIVE_SIZE];
            RecvBuffer = new byte[RECEIVE_SIZE];
            Cursor = 0;
        }

        internal void Start(ServerInfo info) {
            if (info != null) {
                byte[] siv = new byte[4];
                byte[] riv = new byte[4];

                Random.NextBytes(siv);
                Random.NextBytes(riv);

                ClientCipher = new MapleCipher(info.Version, siv, Program.AesCipher);
                ServerCipher = new MapleCipher(info.Version, riv, Program.AesCipher);

                var p = new PacketWriter(14, 16);
                p.WriteShort(info.Version);
                p.WriteMapleString(info.Subversion);
                p.WriteBytes(riv);
                p.WriteBytes(siv);
                p.WriteByte(info.Locale);

                SendRawPacket(p.ToArray());
            }

            Receive();
        }

        private void Receive() {
            if (!Connected) return;

            SocketError error;
            Socket.BeginReceive(RecvBuffer, 0, RECEIVE_SIZE, SocketFlags.None, out error, PacketCallback, null);
            if (error != SocketError.Success) {
                Console.WriteLine("Bug Testing 101");
                Disconnect();
            }
        }

        private void PacketCallback(IAsyncResult iar) {
            if (!Connected) return;

            SocketError error;
            // TODO: Fix Diposed Socket Bug
            // If client is in process of receiving packet right when you disconnect
            // Socket will be disposed, and throw exception
            int length = Socket.EndReceive(iar, out error);
            if (length == 0 || error != SocketError.Success) {
                Console.WriteLine("Bug Testing 102");
                Disconnect();
            } else {
                Append(length);
                ManipulateBuffer();
                Receive();
            }
        }

        private void Append(int length) {
            if (PacketBuffer.Length - Cursor < length) {
                int newSize = PacketBuffer.Length * 2;
                while (newSize < Cursor + length) {
                    newSize *= 2;
                }
                byte[] newBuffer = new byte[newSize];
                Buffer.BlockCopy(PacketBuffer, 0, newBuffer, 0, Cursor);
                PacketBuffer = newBuffer;
            }
            Buffer.BlockCopy(RecvBuffer, 0, PacketBuffer, Cursor, length);
            Cursor += length;
        }

        private void ManipulateBuffer() {
            if (Encrypted) {
                ProcessPacket();
            } else if (Cursor >= HANDSHAKE_HEADER_SIZE) {
                ProcessHandshake();
            }
        }

        private void ProcessPacket() {
            while (Cursor > PACKET_HEADER_SIZE && Connected) {
                int packetSize = MapleCipher.GetPacketLength(PacketBuffer);
                if (Cursor < packetSize + PACKET_HEADER_SIZE || OnPacket == null) {
                    return;
                }

                byte[] buffer = new byte[packetSize];
                Buffer.BlockCopy(PacketBuffer, PACKET_HEADER_SIZE, buffer, 0, packetSize);
                ServerCipher.Transform(buffer);

                Cursor -= packetSize + PACKET_HEADER_SIZE;
                if (Cursor > 0) {
                    Buffer.BlockCopy(PacketBuffer, packetSize + PACKET_HEADER_SIZE, PacketBuffer, 0, Cursor);
                }
                OnPacket(this, buffer);
            }
        }

        private void ProcessHandshake() {
            short packetSize = BitConverter.ToInt16(PacketBuffer, 0);
            if (Cursor < packetSize + HANDSHAKE_HEADER_SIZE || OnHandshake == null) return;

            byte[] buffer = new byte[packetSize];
            Buffer.BlockCopy(PacketBuffer, HANDSHAKE_HEADER_SIZE, buffer, 0, packetSize);

            var packet = new PacketReader(buffer);
            var info = new ServerInfo {
                Version = packet.ReadShort(),
                Subversion = packet.ReadMapleString(),
                SIV = packet.ReadBytes(4),
                RIV = packet.ReadBytes(4),
                Locale = packet.ReadByte()
            };

            ClientCipher = new MapleCipher(info.Version, info.SIV, Program.AesCipher);
            ServerCipher = new MapleCipher(info.Version, info.RIV, Program.AesCipher);
            Encrypted = true; //start waiting for encrypted packets

            OnHandshake(this, info);
            Cursor = 0; //reset stream
        }

        public void SendPacket(PacketWriter packet) {
            SendPacket(packet.ToArray());
        }

        public void SendPacket(byte[] packet) {
            if (!Connected) {
                throw new InvalidOperationException("Socket is not connected");
            }
            if (!Encrypted) {
                throw new InvalidOperationException("Handshake has not been received yet");
            }
            if (packet.Length < 2) {
                throw new ArgumentOutOfRangeException(nameof(packet), @"Packet length must be greater than 2");
            }

            lock (SendLock) {
                byte[] final = new byte[packet.Length + PACKET_HEADER_SIZE];

                switch (SessionType) {
                    case SessionType.CLIENT:
                        ClientCipher.GetHeaderToServer(packet.Length, final);
                        break;
                    case SessionType.SERVER:
                        ClientCipher.GetHeaderToClient(packet.Length, final);
                        break;
                }

                ClientCipher.Transform(packet);
                Buffer.BlockCopy(packet, 0, final, PACKET_HEADER_SIZE, packet.Length);
                SendRawPacket(final);
            }
        }

        private void SendRawPacket(byte[] packet) {
            int offset = 0;
            while (offset < packet.Length) {
                SocketError errorCode;
                int sent = Socket.Send(packet, offset, packet.Length - offset, SocketFlags.None, out errorCode);

                if (sent == 0 || errorCode != SocketError.Success) {
                    Console.WriteLine("Bug Testing 103");
                    Disconnect();
                    return;
                }
                offset += sent;
            }
        }

        public void Disconnect(bool finished = true) {
            if (!Connected) return;

            Cursor = 0;

            Socket.Shutdown(SocketShutdown.Both);
            Socket.Disconnect(false);
            Socket.Dispose();

            if (!Encrypted && OnReconnect != null) {
                OnReconnect(this, null);
                Debug.WriteLine("FORCING A RECONNECT");
                return;
            }

            Encrypted = false;
            Connected = false;

            if (!finished) return;

            ClientCipher = null;
            ServerCipher = null;

            if (OnDisconnected != null) {
                OnDisconnected(this,null);
            }
        }
    }
}
