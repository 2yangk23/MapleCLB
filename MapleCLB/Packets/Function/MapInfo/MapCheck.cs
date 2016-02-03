using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Recv;

namespace MapleCLB.Packets.Function.MapInfo {
    class MapCheck {
        public static void mapCheck(object o, PacketReader r) {
            var c = o as Client;
            Load.mapID(c, r);
        }

    }
}
