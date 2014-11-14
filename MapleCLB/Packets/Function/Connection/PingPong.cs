using MapleCLB.MaplePacketLib;
using MapleCLB.User;
using MaplePacketLib;

namespace MapleCLB.Packets.Function
{
    class PingPong : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            c.SendPacket(General.Pong());
        }
    }
}
