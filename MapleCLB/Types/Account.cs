using System;
using System.IO;
using MapleCLB.Tools;

namespace MapleCLB.Types {
    public enum SelectMode : byte {
        SLOT = 0,
        NAME = 1
        //UID = 2
    }

    public sealed class Account {
        private string username;

        public int Hwid1 { get; private set; }
        public short Hwid2 { get; private set; }
        public string MacAddress { get; private set; }
        public string Hwid { get; private set; }

        public string Username {
            get { return username; }
            set {
                var rng = new Random(value.GetHashCode());
                Hwid1 = rng.Next();
                Hwid2 = (short) rng.Next();

                byte[] macBuffer = new byte[6];
                rng.NextBytes(macBuffer);
                // XX-XX-XX-XX-XX-XX
                MacAddress = HexEncoding.ToHexString(macBuffer, '-');

                byte[] hwidBuffer1 = new byte[6];
                byte[] hwidBuffer2 = new byte[4];
                rng.NextBytes(hwidBuffer1);
                rng.NextBytes(hwidBuffer2);
                // XXXXXXXXXXXX_XXXXXXXX
                Hwid = HexEncoding.ToHexString(hwidBuffer1) + '_' + HexEncoding.ToHexString(hwidBuffer2);

                username = value;
            }
        }

        public string Password { get; set; }
        public string Pic { get; set; }

        public SelectMode SelectMode { get; set; }
        public string Select { get; set; }

        public byte World { get; set; }
        public byte Channel { get; set; }

        public Account() { }

        public Account(BinaryReader br) {
            Username = br.ReadString();
            Password = br.ReadString();
            Pic = br.ReadString();
            SelectMode = (SelectMode) br.ReadByte();
            Select = br.ReadString();
            World = br.ReadByte();
            Channel = br.ReadByte();
        }

        public void WriteTo(BinaryWriter bw) {
            bw.Write(Username);
            bw.Write(Password);
            bw.Write(Pic);
            bw.Write((byte) SelectMode);
            bw.Write(Select);
            bw.Write(World);
            bw.Write(Channel);
        }
    }
}
