using MapleCLB.MapleClient;
using MapleCLB.Packets.Send;
using MapleLib.Packet;
using MapleCLB.Types;

namespace MapleCLB.Packets.Recv.Maps {
    internal class FMMovement {
        public static void moveFM1(Client c, PacketReader r) {
            if (c.doWhat == 1 && c.Mapler.Map == 910000001) {
                c.SendPacket(Movement.beforeTeleport());
                c.SendPacket(Movement.beforeTeleport());
                c.SendPacket(Movement.Teleport(c.PortalCount, GameConsts.FM1_CRC, new Position(80, 34), 52)); //Lands on the ground
            } 
            else if (c.doWhat == 1 && c.Mapler.Map != 910000001){
                c.Log.Report("Not in FM Room 1, Disconnecting");
                c.dcst.Enabled = false;
                c.Disconnect();
            }
        }
    }
}
