using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Send;

namespace MapleCLB.Packets.Recv.Map {
    internal class FMMovement {
        public static void moveFM1(object o, PacketReader r) {
            var c = o as Client;
            if (c.doWhat == 1 && c.Mapler.Map == 910000001) {//check map id toos
                c.SendPacket(Tools.HexEncoding.GetBytes("75 02 63 21 3A 18 00 00"));
                c.SendPacket(Tools.HexEncoding.GetBytes("75 02 63 21 3A 18 00 00"));
                c.SendPacket(Tools.HexEncoding.GetBytes("B9 00 01 28 C2 7A 2A 9B 89 3F 18 00 00 00 00 00 75 01 15 FE 00 00 00 00 07 0C 01 0C 02 0C 03 0C 04 00 75 01 5E FE 00 00 1C 02 00 00 00 00 00 00 06 0E 01 00 00 75 01 60 FE 00 00 00 00 00 00 00 00 00 00 06 03 00 00 00 75 01 60 FE 00 00 00 00 0F 00 00 00 00 00 04 ED 00 00 11 00 00 00 00 00 00 00 00 00"));
                c.WriteLog.Report("Movement Sent!");
            }
            else if (c.doWhat == 1 && c.Mapler.Map != 910000001) //Temp until rusher works
            {
                c.WriteLog.Report("Not in FM Room 1, Disconnecting");
                c.shouldCC = false;
                c.Disconnect();
            }
        }

        public static void shouldCC(object o) //Temp until rusher works
        {
            var c = o as Client;
            if (c.shouldCC && c.doWhat == 1)
            {
                if (c.Mapler.Map == 910000001)
                {
                    c.shouldCC = false;
                    c.SendPacket(General.ChangeChannel(0x00));
                }
                else
                {
                    c.WriteLog.Report("Not In FM Room 1, Disconnecting");
                    c.shouldCC = false;
                    c.Disconnect();
                }
            }
        }
    }
}
