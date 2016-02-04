using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Recv;
using MapleCLB.Tools;

namespace MapleCLB.Packets.Function.MapInfo {
    class FMMovement {
        public static void moveFM1(object o, PacketReader r) {
            var c = o as Client;
            if (c.doWhat == 1)//check map id toos
            {
                c.SendPacket(MapleCLB.Tools.HexEncoding.GetBytes("75 02 63 21 3A 18 00 00"));
                c.SendPacket(MapleCLB.Tools.HexEncoding.GetBytes("75 02 63 21 3A 18 00 00"));
                c.SendPacket(MapleCLB.Tools.HexEncoding.GetBytes("B9 00 01 28 C2 7A 2A 9B 89 3F 18 00 00 00 00 00 75 01 15 FE 00 00 00 00 07 0C 01 0C 02 0C 03 0C 04 00 75 01 5E FE 00 00 1C 02 00 00 00 00 00 00 06 0E 01 00 00 75 01 60 FE 00 00 00 00 00 00 00 00 00 00 06 03 00 00 00 75 01 60 FE 00 00 00 00 0F 00 00 00 00 00 04 ED 00 00 11 00 00 00 00 00 00 00 00 00"));
                Program.WriteLog("Movement Sent!");
            }

        }

    }
}
