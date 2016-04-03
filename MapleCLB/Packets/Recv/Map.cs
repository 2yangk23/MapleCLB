using MapleCLB.MapleClient;
using MapleLib.Packet;
using SharedTools;

namespace MapleCLB.Packets.Recv {
    internal static class Map {
        public static void SpawnPlayer(object o, PacketReader pr) {
            var c = o as Client;
            Precondition.NotNull(c);

            int uid = pr.ReadInt();
            pr.ReadByte(); // [Level (1)]
            string ign = pr.ReadMapleString(); // Name

            c.UidMap[uid] = ign;
        }

        public static void RemovePlayer(object o, PacketReader pr) {
            var c = o as Client;
            Precondition.NotNull(c);

            int uid = pr.ReadInt();

            string trash;
            c.UidMap.TryRemove(uid, out trash);
        }
    }
}
