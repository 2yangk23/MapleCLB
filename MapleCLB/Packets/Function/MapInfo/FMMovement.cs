using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Recv;
using MapleCLB.Tools;

namespace MapleCLB.Packets.Function.MapInfo {
    class FMMovement {
        public static void moveFM1(object o, PacketReader r) {
            var c = o as Client;
            if (c.doWhat == 1)//check map id too
            {
                c.SendPacket(MapleCLB.Tools.HexEncoding.GetBytes("75 02 3D DE 77 42 00 00"));
                c.SendPacket(MapleCLB.Tools.HexEncoding.GetBytes("75 02 3D DE 77 42 00 00"));
                c.SendPacket(MapleCLB.Tools.HexEncoding.GetBytes("B9 00 01 28 C2 7A 2A 1D E0 77 42 00 00 00 00 00 75 01 15 FE 00 00 00 00 07 0C 01 00 75 01 16 FE 00 00 3C 00 00 00 00 00 00 00 06 1E 00 00 0C 02 00 75 01 5E FE 00 00 1C 02 00 00 00 00 00 00 06 F0 00 00 0C 03 00 75 01 60 FE 00 00 00 00 00 00 00 00 00 00 06 03 00 00 00 75 01 60 FE 00 00 00 00 0F 00 00 00 00 00 04 ED 00 00 11 00 00 00 00 00 00 00 00 00"));
                Program.WriteLog("Movement Sent!");
            }

        }

    }
}
