using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Recv.Map {
    class MapCheck {
        public static void Check(object o, PacketReader r) {
            var c = o as Client;
            Load.CharInfo(c, r);
            FMMovement.shouldCC(c);
        }
    }
}
