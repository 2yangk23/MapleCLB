﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MapleCLB.MapleLib.Crypto;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Types;

namespace MapleCLB.MapleClient {
    internal static class Auth {
        private const short AUTH_1 = 0x33;
        private const short AUTH_2 = 0x2D;
        private const short AUTH_3 = 0x35;

        private static readonly byte[] AuthKey1 = { 0x1D, 0x6A, 0x20, 0xCE };
        private static readonly byte[] AuthKey2 = { 0xEB, 0x29, 0x72, 0x30 }; // { 0xEB, 0x29, 0x72, 0x31 };
        private static readonly byte[] AuthKey3 = { 0xF7, 0xDD, 0xB1, 0x35 }; // { 0xF7, 0xDD, 0xB0, 0x35 };

        private static readonly IPAddress[] AuthIps = {
            IPAddress.Parse("208.85.110.164"),
            IPAddress.Parse("208.85.110.166"),
            IPAddress.Parse("208.85.110.169"),
            IPAddress.Parse("208.85.110.170"),
            IPAddress.Parse("208.85.110.171")
        };
        private const ushort AUTH_PORT = 47611;
        private static int seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
        private static readonly ThreadLocal<byte[]> Buffer = new ThreadLocal<byte[]>(() => new byte[1024]);

        public static string GetAuth(Account account) {
            string auth = string.Empty;
            for (int i = 1; i <= 3; ++i) {
                var authSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                authSocket.Connect(AuthIps, AUTH_PORT);
                switch (i) {
                    case 1:
                        authSocket.Send(AuthFirst(account.Username, account.Password));
                        int length = authSocket.Receive(Buffer.Value);
                        if (length == 0) {
                            i = 4; // Skip the rest of the auths
                            Debug.WriteLine("Invalid username or password");
                        } else {
                            auth = ParseAuth(Buffer.Value);
                            //Debug.WriteLine("Finished first step of auth");
                        }
                        break;
                    case 2:
                        authSocket.Send(AuthSecond(auth));
                        authSocket.Receive(Buffer.Value);
                        //Debug.WriteLine("Finished second step of auth");
                        break;
                    case 3:
                        authSocket.Send(AuthThird(auth));
                        authSocket.Receive(Buffer.Value);
                        //Debug.WriteLine("Finished third step of auth");
                        break;
                    default:
                        Debug.WriteLine(i + " isnt even a valid auth sequence.");
                        break;
                }
                authSocket.Shutdown(SocketShutdown.Both);
                authSocket.Close();
            }

            return auth;
        }

        private static byte[] AuthFirst(string user, string pass) {
            var data = new PacketWriter();
            data.WriteInt(8);
            data.WriteUnicodeString(user);
            data.WriteUnicodeString(pass);
            data.WriteBytes(0x00, 0x00, 0x13, 0x22, 0x00, 0x02, 0x01, 0x00);
            data.WriteZero(10);
            data.WriteUnicodeString(GetRandomString(23)); // 23 Random characters (Length 46 as unicode)
            data.WriteInt(1);
            data.WriteZero(2);

            return AuthCipher.WriteHeader(AUTH_1, AuthKey1, data.ToArray());
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
            data.WriteBytes(0x13, 0x22, 0x00, 0x02);

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
            const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_~";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++) {
                result.Append(characters[Rng.Value.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}