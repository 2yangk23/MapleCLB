using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace MapleCLB.MapleLib.Packet {
    public static class HexEncoding {
        private static int seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static bool IsHexDigit(char c) {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }

        public static byte ToByte(string hex) {
            return Convert.ToByte(hex, 16);
        }

        public static string ToHex(byte b) {
            char[] buf = new char[2];
            buf[0] = GetHexValue(b / 16);
            buf[1] = GetHexValue(b % 16);
            return new string(buf);
        }

        public static byte[] ToByteArray(string hex) {
            hex = hex.Replace(" ", string.Empty);
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ToAsciiString(byte[] bytes) {
            char[] buf = new char[bytes.Length];
            for (int i = 0; i < bytes.Length; i++) {
                if (bytes[i] < 32) {
                    buf[i] = '.';
                } else {
                    buf[i] = (char)bytes[i];
                }
            }
            return new string(buf);
        }

        public static string ToHexString(byte[] bytes) {
            char[] buf = new char[bytes.Length * 3];
            int index = 0;
            for (int i = 0; i < bytes.Length * 3; i += 3) {
                byte b = bytes[index++];
                buf[i] = GetHexValue(b / 16);
                buf[i + 1] = GetHexValue(b % 16);
                buf[i + 2] = ' ';
            }

            return new string(buf, 0, buf.Length - 1);
        }

        public static string RandomHexString(int length, string spacer = "") {
            var sb = new StringBuilder();
            sb.Append(ToHex((byte)Rng.Value.Next(0xFF)));
            for (int i = 0; i < length - 1; i++) {
                sb.Append(spacer);
                sb.Append(ToHex((byte)Rng.Value.Next(0xFF)));
            }
            return sb.ToString();
        }

        public static unsafe string FillRandom(string packet) {
            fixed (char* pch = packet) {
                for (int i = 0; i < packet.Length; i++) //randomizes wildcards
                {
                    if (pch[i] == '*') {
                        pch[i] = $"{Rng.Value.Next(16):X}"[0];
                    }
                }
            }
            return packet;
        }

        private static char GetHexValue(int b) {
            if (b < 10) {
                return (char)(b + '0');
            }
            return (char)(b - 10 + 'A');
        } 
    }
}