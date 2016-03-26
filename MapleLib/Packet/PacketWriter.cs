﻿using System;
using System.Text;
using MapleCLB.Tools;

namespace MapleLib.Packet {
    public class PacketWriter {
        private const int DEFAULT_SIZE = 64;
        private byte[] buffer;
        public int Position { get; private set; }
        // There should be no way to seek so Length = Position

        public PacketWriter() {
            buffer = new byte[DEFAULT_SIZE];
        }

        public PacketWriter(ushort opcode, int size = DEFAULT_SIZE) {
            buffer = new byte[size];
            WriteUShort(opcode);
        }

        private void EnsureCapacity(int length) {
            if (Position + length < buffer.Length) return;
            int newSize = buffer.Length * 2;
            while (newSize < Position + length) {
                newSize *= 2;
            }
            byte[] newBuffer = new byte[newSize];
            System.Buffer.BlockCopy(buffer, 0, newBuffer, 0, Position);
            buffer = newBuffer;
        }

        public void WriteBool(bool value) {
            WriteByte(value ? (byte)1 : (byte)0);
        }

        public unsafe void WriteByte(byte value = 0) {
            EnsureCapacity(1);
            fixed (byte* ptr = buffer) {
                *(ptr + Position) = value;
                ++Position;
            }
        }

        public void WriteBytes(params byte[] value) {
            EnsureCapacity(value.Length);
            System.Buffer.BlockCopy(value, 0, buffer, Position, value.Length);
            Position += value.Length;
        }

        public unsafe void WriteShort(short value = 0) {
            EnsureCapacity(2);
            fixed (byte* ptr = buffer) {
                *(short*)(ptr + Position) = value;
                Position += 2;
            }
        }

        public unsafe void WriteUShort(ushort value = 0) {
            EnsureCapacity(2);
            fixed (byte* ptr = buffer) {
                *(ushort*)(ptr + Position) = value;
                Position += 2;
            }
        }

        public unsafe void WriteShortBigEndian(short value = 0) {
            EnsureCapacity(2);
            fixed (byte* ptr = buffer) {
                *(ptr + Position) = (byte)(value >> 8);
                *(ptr + Position + 1) = (byte)value;
                Position += 2;
            }
        }

        public unsafe void WriteInt(int value = 0) {
            EnsureCapacity(4);
            fixed (byte* ptr = buffer) {
                *(int*)(ptr + Position) = value;
                Position += 4;
            }
        }

        public unsafe void WriteUInt(uint value = 0) {
            EnsureCapacity(4);
            fixed (byte* ptr = buffer) {
                *(uint*)(ptr + Position) = value;
                Position += 4;
            }
        }

        public unsafe void WriteIntBigEndian(int value = 0) {
            EnsureCapacity(4);
            fixed (byte* ptr = buffer) {
                *(ptr + Position) = (byte)(value >> 24);
                *(ptr + Position + 1) = (byte)(value >> 16);
                *(ptr + Position + 2) = (byte)(value >> 8);
                *(ptr + Position + 3) = (byte)value;
                Position += 4;
            }
        }

        public unsafe void WriteLong(long value = 0) {
            EnsureCapacity(8);
            fixed (byte* ptr = buffer) {
                *(long*)(ptr + Position) = value;
                Position += 8;
            }
        }

        public void Timestamp() {
            WriteInt(Environment.TickCount);
        }

        public void WriteString(string value) {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            WriteBytes(bytes);
        }

        public void WritePaddedString(string value, int length, char pad = '\0') {
            WriteString(value);
            for (int i = value.Length; i < length; i++) {
                WriteByte((byte)pad);
            }
        }

        public void WriteUnicodeString(string value) {
            WriteShort((short)value.Length);
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            WriteBytes(bytes);
        }

        public void WriteMapleString(string value) {
            WriteShort((short)value.Length);
            WriteString(value);
        }

        public void WriteHexString(string value) {
            byte[] bytes = HexEncoding.ToByteArray(value);
            WriteBytes(bytes);
        }

        public void WriteZero(int count) {
            WriteBytes(new byte[count]);
        }

        public byte[] ToArray() {
            byte[] copy = new byte[Position];
            System.Buffer.BlockCopy(buffer, 0, copy, 0, Position);
            return copy;
        }

        // Be careful when using this method
        // If you modify the buffer, it will modify the PacketWriter
        public byte[] GetBuffer() {
            return buffer;
        }

        public override string ToString() {
            return HexEncoding.ToHexString(ToArray(), ' ');
        }
    }
}