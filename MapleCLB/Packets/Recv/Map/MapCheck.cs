using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Recv.Map {
    internal class MapCheck {
        public static void Check(object o, PacketReader r) {
            var c = o as Client;
            Load.CharInfo(c, r);
        }
    }
}
