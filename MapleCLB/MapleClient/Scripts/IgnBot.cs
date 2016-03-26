using System;
using System.Threading;
using MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.ScriptLib;

namespace MapleCLB.MapleClient.Scripts {
    internal class IgnBot : Script {
        private const string NAME = "dismyign123";
        private readonly byte[] CreationPacket, OkPacket;

        public IgnBot(Client client) : base(client) {
            var w = new PacketWriter(0x87);
            w.WriteMapleString(NAME);
            w.WriteInt(0); // Key Settings - = basic
            w.WriteInt(-1);
            w.WriteInt(1); // Job
            w.WriteShort(0); // Sub Job
            w.WriteByte(1); // gender
            w.WriteByte(0); // Skin
                            // [Blocks (1)] [4 * Blocks]
            w.WriteHexString("06 D1 52 00 00 9F 93 00 00 00 00 00 00 DA 0A 10 00 C1 5E 10 00 F0 DD 13 00");

            CreationPacket = w.ToArray();
            
            w = new PacketWriter(0x276);
            w.WriteHexString("00 01 01 00 00 40 A8 61 A0 45 42 0F 01 00 00");

            OkPacket = w.ToArray();
        }

        protected override void Execute() {
            Console.WriteLine("Waiting for charlist...");
            WaitRecv(RecvOps.CHARLIST);
            SendPacket(OkPacket);
            SendPacket(OkPacket);
            Console.WriteLine("Starting...");
            while (true) {
                Thread.Sleep(25);
                Console.WriteLine("Trying ign " + NAME);
                var w = new PacketWriter(0x74);
                w.WriteMapleString(NAME);
                SendPacket(w.ToArray());

                var r = WaitRecv2(0x10);
                Console.WriteLine(r.ToString());
                r.ReadMapleString();

                byte b = r.ReadByte();
                if (b == 0) { // Ign is available
                    SendPacket(CreationPacket);
                    break;
                }

                SendPacket(OkPacket); // Ign taken or error processing request
            }
            Console.WriteLine("Successfully created character: " + NAME);
        }
    }
}
