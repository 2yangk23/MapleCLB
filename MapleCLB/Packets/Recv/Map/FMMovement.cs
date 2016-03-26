using MapleCLB.MapleClient;
using MapleLib.Packet;
using MapleCLB.Packets.Send;

namespace MapleCLB.Packets.Recv.Map {
    internal class FMMovement {
        public static void moveFM1(object o, PacketReader r) {
            var c = o as Client;
            if (c.doWhat == 1 && c.Mapler.Map == 910000001) {
                c.SendPacket(Movement.beforeTeleport());
                c.SendPacket(Movement.beforeTeleport());
                c.SendPacket(Movement.Teleport(c.PortalCount, SendOps.FM1_CRC, 80, 34, 52)); //Lands on the ground
            } 
            else if (c.doWhat == 1 && c.Mapler.Map != 910000001){
                c.WriteLog.Report("Not in FM Room 1, Disconnecting");
                c.dcst.Enabled = false;
                c.Disconnect();
            }
        }
    }
}
