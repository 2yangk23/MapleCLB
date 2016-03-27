using System;
using System.Threading;
using MapleCLB.Packets;
using ScriptLib;
using MapleLib.Packet;

namespace MapleCLB.MapleClient.Scripts {
    internal class IgnBot : Script<Client> {
        private const string NAME = "dismyign123";
        private readonly byte[] creationPacket, okPacket;

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

            creationPacket = w.ToArray();

            w = new PacketWriter(0x276);
            w.WriteHexString("00 01 01 00 00 40 A8 61 A0 45 42 0F 01 00 00");

            okPacket = w.ToArray();
        }

        protected override void Execute() {
            Console.WriteLine("Waiting for charlist...");
            WaitRecv(RecvOps.CHARLIST);
            SendPacket(okPacket);
            SendPacket(okPacket);
            Console.WriteLine("Starting...");
            while (true) {
                Thread.Sleep(25);
                Console.WriteLine("Trying ign " + NAME);
                var w = new PacketWriter(0x74);
                w.WriteMapleString(NAME);
                SendPacket(w.ToArray());

                var r = WaitRecv(0x10, true);
                Console.WriteLine(r.ToString());
                r.ReadMapleString();

                byte b = r.ReadByte();
                if (b == 0) { // Ign is available
                    SendPacket(creationPacket);
                    break;
                }

                SendPacket(okPacket); // Ign taken or error processing request
            }
            Console.WriteLine("Successfully created character: " + NAME);
        }
    }
}
