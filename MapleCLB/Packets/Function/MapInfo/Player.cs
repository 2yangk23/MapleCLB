using MapleCLB.MaplePacketLib;
using MapleCLB.User;
using MaplePacketLib;

namespace MapleCLB.Packets.Function
{
    class SpawnPlayer : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            Load.Player(c, r);
        }
    }

    class RemovePlayer : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            Load.RemovePlayer(c, r);
        }
    }
}
