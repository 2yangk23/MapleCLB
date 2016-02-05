using System;
using System.Threading;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.MapleLib.Crypto {
    public static class AuthCipher {
        private const short PRIMARY_LENGTH = 16;
        private const short SECONDARY_LENGTH = 12;

        private static int rngSeed = Environment.TickCount;
        private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref rngSeed)));

        private static readonly uint[] XorTable = { 0x040FC1578, 0x0113B6C1F, 0x08389CA19, 0x0E2196CD8,
                                                    0x074901489, 0x04AAB1566, 0x07B8C12A0, 0x00018FFCD,
                                                    0x0CCAB704B, 0x07B5A8C0F, 0x0AA13B891, 0x0DE419807,
                                                    0x012FFBCAE, 0x05F5FBA34, 0x010F5AC99, 0x0B1C1DD01 };

        public unsafe static void Encrypt(byte[] buffer, uint seed) {
            uint prev = 0;
            fixed (byte* ptr = buffer) {
                for (int i = 0; i < buffer.Length; i += 4) {
                    uint temp = seed ^ prev ^ XorTable[i / 4 % 16];
                    prev = *(uint*)(ptr + i);
                    *(uint*)(ptr + i) ^= temp;
                }
            }
        }

        public unsafe static void Decrypt(byte[] buffer, uint seed) {
            uint prev = 0;
            fixed (byte* ptr = buffer) {
                for (int i = 0; i < buffer.Length; i += 4) {
                    *(uint*)(ptr + i) ^= seed ^ prev ^ XorTable[i / 4 % 16];
                    prev = *(uint*)(ptr + i);
                }
            }
        }


        public static byte[] WriteHeader(short header, byte[] code, byte[] data) {
            uint seed = (uint)Rng.Value.Next();
            Encrypt(data, seed);

            var pw = new PacketWriter();
            pw.WriteShortBigEndian((short)(PRIMARY_LENGTH + data.Length));
            pw.WriteShortBigEndian(header);
            pw.WriteBytes(0x18, 0x00);
            pw.WriteShortBigEndian((short)(SECONDARY_LENGTH + data.Length));
            pw.WriteBytes(0x02, 0x00);
            pw.WriteShortBigEndian((short)data.Length);
            pw.WriteIntBigEndian((int)seed);
            pw.WriteBytes(code);
            pw.WriteBytes(data);
            //Debug.WriteLine("Data Send: " + pw.ToString());
            return pw.ToArray();
        }

        public static byte[] ReadHeader(byte[] packet) {
            var pr = new PacketReader(packet);
            pr.Skip(10);
            short length = pr.ReadShortBigEndian();
            int seed = pr.ReadIntBigEndian();
            pr.Skip(4);
            byte[] data = pr.ReadBytes(length);

            Decrypt(data, (uint)seed);
            //Debug.WriteLine("Data: "+data);
            return data;
        }
    }
}
