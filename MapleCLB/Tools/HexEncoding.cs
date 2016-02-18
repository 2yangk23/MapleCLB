using System;

namespace MapleCLB.Tools {
    public class HexEncoding {
        private static readonly Random Rng = new Random();

        public static bool IsHexDigit(char c) {
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = char.ToUpper(c);
            int numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < numA + 6)
                return true;
            if (numChar >= num1 && numChar < num1 + 10)
                return true;
            return false;
        }

        private static byte HexToByte(string hex) {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }

        public static byte[] GetBytes(string hexString) {
            string newString = string.Empty;
            // remove all none A-F, 0-9, characters
            foreach (char c in hexString) {
                if (IsHexDigit(c))
                    newString += c;
            }
            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0) {
                newString = newString.Substring(0, newString.Length - 1);
            }

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];
            int j = 0;
            for (int i = 0; i < bytes.Length; i++) {
                string hex = new string(new[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }

        public static string ToStringFromAscii(byte[] bytes) {
            char[] ret = new char[bytes.Length];
            for (int x = 0; x < bytes.Length; x++) {
                if (bytes[x] < 32) {
                    ret[x] = '.';
                } else {
                    int chr = bytes[x] & 0xFF;
                    ret[x] = (char)chr;
                }
            }
            return new string(ret);
        }

        public static string ByteArrayToString(byte[] array) {
            string temp = "";
            foreach (byte bit in array) {
                temp += $"{bit:X2} ";
            }
            return temp;
        }

        public static string ToHex(byte b) {
            return $"{b:X2}";
        }

        public static string GetRandomHexString(int digits, string spacer = "") {
            string toreturn = string.Empty;
            toreturn += ToHex((byte)Rng.Next(0xFF));
            for (int i = 0; i < digits - 1; i++)
                toreturn += spacer + ToHex((byte)Rng.Next(0xFF));
            return toreturn;
        }

        public static string MacAddress(int uid){
            string toreturn = string.Empty;
            int hash = uid.GetHashCode();
            string temp = hash.ToString("X8");
            toreturn = temp + temp + temp;
            for (int i = 4; i <= 28; i = i + 6){
                toreturn = toreturn.Insert(i, "2D");
            }
            toreturn = toreturn.Insert(0, "1100");
            return toreturn;
        }

        public static unsafe string FillRandom(string packet) {
            fixed (char* pch = packet) {
                for (int i = 0; i < packet.Length; i++) //randomizes wildcards
                {
                    if (pch[i] == '*') {
                        pch[i] = $"{Rng.Next(16):X}"[0];
                    }
                }
            }
            return packet;
        }
    }
}