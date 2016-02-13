using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Recv;

namespace MapleCLB.Packets.Function.MapInfo {
    class Player {
        public static void SpawnPlayer(object o, PacketReader r) {
            var c = o as Client;
            Load.AddPlayer(c, r);
        }

        public static void RemovePlayer(object o, PacketReader r) {
            var c = o as Client;
            Load.RemovePlayer(c, r);
        }
    }
}
