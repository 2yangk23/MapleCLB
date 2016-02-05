using System;
using System.Diagnostics;
using System.Text;

namespace MapleCLB.MapleLib.Packet {
    public class PacketReader {
        public byte[] Buffer { get; private set; }
        public int Position { get; private set; }

        public int Available {
            get { return Buffer.Length - Position; }
        }

        public PacketReader(byte[] packet) {
            Buffer = packet;
        }

        public byte ReadByte() {
            Debug.Assert(Position + 1 <= Buffer.Length);
            return Buffer[Position++];
        }

        public bool ReadBool() {
            return ReadByte() == 1;
        }

        public byte[] ReadBytes(int count) {
            //Debug.Assert(Position + count <= Buffer.Length);
           // Console.WriteLine("COUNT IS? " + count);
            byte[] bytes = new byte[count];
           // Console.WriteLine("POS IS? " + count);
            System.Buffer.BlockCopy(Buffer, Position, bytes, 0, count);
            Position += count;
          //  Console.WriteLine("Pos of ReadBytes : " + Position);
           // Console.WriteLine("Count of ReadBytes : " + count);

            return bytes;
        }

        public unsafe short ReadShort() {
          //  Debug.Assert(Position + 2 <= Buffer.Length);
            fixed (byte* ptr = Buffer) {
                short value = *(short*)(ptr + Position);
                Position += 2;
                return value;
            }
        }

        public unsafe short ReadShortBigEndian() {
       //     Debug.Assert(Position + 2 <= Buffer.Length);
            fixed (byte* ptr = Buffer) {
                short value = (short)(*(ptr + Position) << 8 | *(ptr + Position + 1));
                Position += 2;
                return value;
            }
        }

        public unsafe int ReadInt() {
            Debug.Assert(Position + 4 <= Buffer.Length);
            fixed (byte* ptr = Buffer) {
                int value = *(int*)(ptr + Position);
                Position += 4;
                return value;
            }
        }

        public unsafe uint ReadUInt() {
            Debug.Assert(Position + 4 <= Buffer.Length);
            fixed (byte* ptr = Buffer) {
                uint value = *(uint*)(ptr + Position);
                Position += 4;
                return value;
            }
        }

        public unsafe int ReadIntBigEndian() {
            Debug.Assert(Position + 4 <= Buffer.Length);
            fixed (byte* ptr = Buffer) {
                int value = *(ptr + Position) << 24 | *(ptr + Position + 1) << 16 | *(ptr + Position + 2) << 8 | *(ptr + Position + 3);
                Position += 4;
                return value;
            }
        }

        public unsafe long ReadLong() {
            Debug.Assert(Position + 8 <= Buffer.Length);
            fixed (byte* ptr = Buffer) {
                long value = *(long*)(ptr + Position);
                Position += 8;
                return value;
            }
        }

        public string ReadString(int count) {
            byte[] bytes = ReadBytes(count);
            return Encoding.UTF8.GetString(bytes);
        }

        public string ReadUnicodeString() {
            short count = ReadShort();
            byte[] bytes = ReadBytes(count * 2);
            return Encoding.Unicode.GetString(bytes);
        }

        public string ReadMapleString() {
            short count = ReadShort();
            //Console.WriteLine("SHORT VALUE? " + count);
            return ReadString(count);
        }

        public string ReadMapleStringv2()
        {
            byte count = ReadByte();
        //    Console.WriteLine("BYTE VALUE? " + count);
            return ReadString(count);
        }


        public string ReadHexString(int count) {
            return HexEncoding.ToHexString(ReadBytes(count));
        }

        public void Skip(int count) {
          //  Console.WriteLine("Pos Before: "+ Position);
            //Debug.Assert(Position + count <= Buffer.Length);
            Position += count;
            //Console.WriteLine("Pos After: " + Position);
        }

        public void Next(byte b) {
            int pos = Array.IndexOf(Buffer, b, Position);
            Skip(pos - Position + 1);
        }

        public byte[] ToArray() {
            byte[] copy = new byte[Buffer.Length];
            System.Buffer.BlockCopy(Buffer, 0, copy, 0, Buffer.Length);
            return copy;
        }

        public override string ToString() {
            return HexEncoding.ToHexString(Buffer);
        }
    }
}
