using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Recv;

namespace MapleCLB.Packets.Function.MapInfo {
    class Mushrooms {

        public static void loadMushrooms(object o, PacketReader r) {
            var c = o as Client;
            Load.Mushrooms(c, r);
        }

    }
}
