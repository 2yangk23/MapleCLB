using System;
using System.Diagnostics;
using System.Net.Sockets;
using MapleLib.Crypto;
using MapleLib.Packet;
using SharedTools;

namespace MapleLib {
    public enum SessionType {
        CLIENT,
        SERVER
    }

    public sealed class Session {
        private const short RECEIVE_SIZE = 1024;
        private const int HANDSHAKE_HEADER_SIZE = 2;
        private const int PACKET_HEADER_SIZE = 4;

        private readonly AesCipher aesCipher;
        private readonly byte[] recvBuffer;
        private readonly object sendLock;

        public readonly SessionType SessionType;

        private readonly Socket socket;
        private MapleCipher clientCipher;
        private int cursor;
        private byte[] packetBuffer;
        private MapleCipher serverCipher;

        public bool Connected { get; private set; }
        public bool Encrypted { get; private set; }

        public event EventHandler<ServerInfo> OnHandshake;
        public event EventHandler<byte[]> OnPacket;
        public event EventHandler OnDisconnected;
        public event EventHandler<Session> OnReconnect;

        internal Session(Socket socket, SessionType type, AesCipher aesCipher) {
            this.socket = socket;
            SessionType = type;

            Encrypted = type != SessionType.CLIENT;
            this.aesCipher = aesCipher;
            Connected = true;

            sendLock = new object();
            packetBuffer = new byte[RECEIVE_SIZE];
            recvBuffer = new byte[RECEIVE_SIZE];
            cursor = 0;
        }

        internal void Start(ServerInfo info) {
            if (info != null) {
                byte[] siv = new byte[4];
                byte[] riv = new byte[4];
                var rng = new Random();

                rng.NextBytes(siv);
                rng.NextBytes(riv);

                clientCipher = new MapleCipher(info.Version, siv, aesCipher);
                serverCipher = new MapleCipher(info.Version, riv, aesCipher);

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
            if (!Connected) {
                return;
            }

            SocketError error;
            socket.BeginReceive(recvBuffer, 0, RECEIVE_SIZE, SocketFlags.None, out error, PacketCallback, null);
            if (error != SocketError.Success) {
                Console.WriteLine("Bug Testing 101");
                Disconnect();
            }
        }

        private void PacketCallback(IAsyncResult iar) {
            if (!Connected) {
                return;
            }

            SocketError error;
            // TODO: Fix Diposed Socket Bug
            // If client is in process of receiving packet right when you disconnect
            // Socket will be disposed, and throw exception
            int length = socket.EndReceive(iar, out error);
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
            if (packetBuffer.Length - cursor < length) {
                int newSize = packetBuffer.Length * 2;
                while (newSize < cursor + length) {
                    newSize *= 2;
                }
                byte[] newBuffer = new byte[newSize];
                Buffer.BlockCopy(packetBuffer, 0, newBuffer, 0, cursor);
                packetBuffer = newBuffer;
            }
            Buffer.BlockCopy(recvBuffer, 0, packetBuffer, cursor, length);
            cursor += length;
        }

        private void ManipulateBuffer() {
            if (Encrypted) {
                ProcessPacket();
            } else if (cursor >= HANDSHAKE_HEADER_SIZE) {
                ProcessHandshake();
            }
        }

        private void ProcessPacket() {
            while (cursor > PACKET_HEADER_SIZE && Connected) {
                int packetSize = MapleCipher.GetPacketLength(packetBuffer);
                if (cursor < packetSize + PACKET_HEADER_SIZE || OnPacket == null) {
                    return;
                }

                byte[] buffer = new byte[packetSize];
                Buffer.BlockCopy(packetBuffer, PACKET_HEADER_SIZE, buffer, 0, packetSize);
                serverCipher.Transform(buffer);

                cursor -= packetSize + PACKET_HEADER_SIZE;
                if (cursor > 0) {
                    Buffer.BlockCopy(packetBuffer, packetSize + PACKET_HEADER_SIZE, packetBuffer, 0, cursor);
                }
                OnPacket(this, buffer);
            }
        }

        private void ProcessHandshake() {
            short packetSize = BitConverter.ToInt16(packetBuffer, 0);
            if (cursor < packetSize + HANDSHAKE_HEADER_SIZE || OnHandshake == null) {
                return;
            }

            byte[] buffer = new byte[packetSize];
            Buffer.BlockCopy(packetBuffer, HANDSHAKE_HEADER_SIZE, buffer, 0, packetSize);

            var packet = new PacketReader(buffer);
            var info = new ServerInfo {
                Version = packet.ReadShort(),
                Subversion = packet.ReadMapleString(),
                SIV = packet.ReadBytes(4),
                RIV = packet.ReadBytes(4),
                Locale = packet.ReadByte()
            };

            clientCipher = new MapleCipher(info.Version, info.SIV, aesCipher);
            serverCipher = new MapleCipher(info.Version, info.RIV, aesCipher);
            Encrypted = true; //start waiting for encrypted packets

            OnHandshake(this, info);
            cursor = 0; //reset stream
        }

        public void SendPacket(PacketWriter packet) {
            SendPacket(packet.ToArray());
        }

        public void SendPacket(byte[] packet) {
            Precondition.Check<InvalidOperationException>(Connected, "Socket is not connected");
            Precondition.Check<InvalidOperationException>(Encrypted, "Handshake has not been received yet");
            Precondition.Check<ArgumentException>(packet.Length >= 2, @"Packet length must be greater than 2");

            lock (sendLock) {
                byte[] final = new byte[packet.Length + PACKET_HEADER_SIZE];

                switch (SessionType) {
                    case SessionType.CLIENT:
                        clientCipher.GetHeaderToServer(packet.Length, final);
                        break;
                    case SessionType.SERVER:
                        clientCipher.GetHeaderToClient(packet.Length, final);
                        break;
                }

                clientCipher.Transform(packet);
                Buffer.BlockCopy(packet, 0, final, PACKET_HEADER_SIZE, packet.Length);
                SendRawPacket(final);
            }
        }

        private void SendRawPacket(byte[] packet) {
            int offset = 0;
            while (offset < packet.Length) {
                SocketError errorCode;
                int sent = socket.Send(packet, offset, packet.Length - offset, SocketFlags.None, out errorCode);

                if (sent == 0 || errorCode != SocketError.Success) {
                    Console.WriteLine("Bug Testing 103");
                    Disconnect();
                    return;
                }
                offset += sent;
            }
        }

        public void Disconnect(bool finished = true) {
            if (!Connected) {
                return;
            }

            cursor = 0;
            socket.Shutdown(SocketShutdown.Both);
            socket.Disconnect(false);
            socket.Dispose();

            if (!Encrypted && OnReconnect != null) {
                OnReconnect(this, null);
                Debug.WriteLine("FORCING A RECONNECT");
                return;
            }

            Encrypted = false;
            Connected = false;

            if (!finished) {
                return;
            }

            clientCipher = null;
            serverCipher = null;
            OnDisconnected?.Invoke(this, null);
        }
    }
}
