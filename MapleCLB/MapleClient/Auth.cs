using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MapleCLB.MapleLib.Crypto;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Tools;

namespace MapleCLB.MapleClient {
    internal static class Auth {
        private const short AUTH_1 = 0x33; //Headers
        private const short AUTH_2 = 0x2D;
        private const short AUTH_3 = 0x35;

        private static readonly byte[] AuthKey1 = { 0x1D, 0x6A, 0x20, 0xCE };
        private static readonly byte[] AuthKey2 = { 0xEB, 0x29, 0x72, 0x30 }; //{ 0xEB, 0x29, 0x72, 0x31 };
        private static readonly byte[] AuthKey3 = { 0xF7, 0xDD, 0xB1, 0x35 }; //{ 0xF7, 0xDD, 0xB0, 0x35 };

        private static readonly IPAddress AuthIp = IPAddress.Parse("208.85.110.166");
        private const ushort AUTH_PORT = 47611;
        private static int seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
        private static readonly ThreadLocal<byte[]> Buffer = new ThreadLocal<byte[]>(() => new byte[1024]); 

        private class SocketAndAuth {
            public Socket Socket { get; set; }
            public string Auth { get; private set; }

            public SocketAndAuth(Socket socket, string auth) {
                Socket = socket;
                Auth = auth;
            }
        }

        public static string GetAuth(string user, string pass) {
            var authSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Testing();
            authSocket.Connect(AuthIp, AUTH_PORT);
            authSocket.Send(AuthFirst(user, pass));
            int length = authSocket.Receive(Buffer.Value);
            if (length == 0) {
                Debug.WriteLine("Invalid username or password");
                authSocket.Shutdown(SocketShutdown.Both);
                authSocket.Close();
                return string.Empty;
            }
          //  string auth = ParseAuth(Buffer.Value);
            else
            {
            string auth = ParseAuth(Buffer.Value);
            authSocket.Shutdown(SocketShutdown.Both);
            authSocket.Close();

            // do remaining useless communication async in separate thread
            authSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var state = new SocketAndAuth(authSocket, auth);
            authSocket.BeginConnect(AuthIp, AUTH_PORT, CompleteAuth, state);
            return auth;
            }
            //Debug.WriteLine("Finished first step of auth");
           // return auth;
        }

        private static void CompleteAuth(IAsyncResult iar) {
            var state = iar.AsyncState as SocketAndAuth;
            if (state == null) return; // Better hope this doesn't trigger bans
            
            state.Socket.EndConnect(iar);
            state.Socket.Send(AuthSecond(state.Auth));
            state.Socket.Receive(Buffer.Value);
            state.Socket.Shutdown(SocketShutdown.Both);
            state.Socket.Close();
            //Debug.WriteLine("Finished second step of auth");

            state.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            state.Socket.Connect(AuthIp, AUTH_PORT);
            state.Socket.Send(AuthThird(state.Auth));
            state.Socket.Receive(Buffer.Value);
            state.Socket.Shutdown(SocketShutdown.Both);
            state.Socket.Close();
            //Debug.WriteLine("Finished third step of auth");
        }

        private static byte[] AuthFirst(string user, string pass) {
            var data = new PacketWriter();
            data.WriteInt(8);
            data.WriteUnicodeString(user);
            data.WriteUnicodeString(pass);
            data.WriteBytes(0x00, 0x00, 0x13, 0x22, 0x00, 0x02, 0x01, 0x00);
            data.WriteZero(10);
            data.WriteUnicodeString(GetRandomString(23)); // 23 Random characters (Length 46 as unicode)
            //data.WriteByte(0x01);
            data.WriteZero(6);
            //data.WriteZero(3);
            //data.WriteBytes(0xC3, 0x2D);//idk what these are
            //data.WriteHexString("08000000180062006F006E00790062006F006E0062006F006E003100390039003300400067006D00610069006C002E0063006F006D00090074006500650077006F0072006C006400730000001322000201000000000000000000000017004700330048005100310059006A0052004600470055006F00450043004E004C0071006600630054007E0059006A00010000007446");

            return AuthCipher.WriteHeader(AUTH_1, AuthKey1, data.ToArray());
        }


        private static void Testing()
        {
            PacketReader pr = new PacketReader(Tools.HexEncoding.GetBytes("00a200331800009e02000092176552fc1d6a20ce8c479957f33e3c069298e094323e70f56346f9639747cf5d5040b76c66ad7517bd22b7dba7de126c6bea72bdf6ca63c95deedb05c6e85c487cfefc07ec8fb6a68e479c57e03e4806fa988894441c7cf76764f5619b47ce5d5c40e96c26ad3a179322c1db91de466c65ea2dbdf0ca08c947ee8905e0e82a4849fe9b07f28f9ba6ae478b57d13e4306e898f8947c3e16f50000"));
            pr.Skip(10);
            short readLength = pr.ReadShortBigEndian();
            uint readSeed = (uint)pr.ReadIntBigEndian();
            pr.Skip(4);
            Console.WriteLine("Decoding with seed " + readSeed + " length " + readLength);
            byte[] readBytes = pr.ReadBytes(readLength);

            try
            {
                AuthCipher.Decrypt(readBytes, readSeed);
                Console.WriteLine(Tools.HexEncoding.ByteArrayToString(readBytes));
            }
            catch { }

        }


        private static byte[] AuthSecond(string auth) {
            var data = new PacketWriter();
            data.WriteUnicodeString(auth);

            return AuthCipher.WriteHeader(AUTH_2, AuthKey2, data.ToArray());
        }

        private static byte[] AuthThird(string auth) {
            var data = new PacketWriter();
            data.WriteInt(2);
            data.WriteUnicodeString(auth);
            data.WriteBytes(0x12, 0x22, 0x00, 0x02);

            return AuthCipher.WriteHeader(AUTH_3, AuthKey3, data.ToArray());
        }

        private static string ParseAuth(byte[] packet) {
            byte[] data = AuthCipher.ReadHeader(packet);
            var pr = new PacketReader(data);
            pr.Skip(10);

            return pr.ReadUnicodeString();
        }

        // Generates a random alphanumeric string
        private static string GetRandomString(int length) {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++) {
                result.Append(characters[Rng.Value.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}