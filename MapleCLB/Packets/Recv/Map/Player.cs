using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Recv.Map {
    internal class Player {
        public static void SpawnPlayer(object o, PacketReader r) {
            var c = o as Client;
            int uid = r.ReadInt();
            r.ReadByte();
            string ign = r.ReadMapleString();

            c.UidMap[uid] = ign;
            c.WriteLog.Report("Added " + uid + "(" + ign + ")");
        }

        public static void RemovePlayer(object o, PacketReader r) {
            var c = o as Client;
            int uid = r.ReadInt();

            c.UidMap.Remove(uid);
            c.WriteLog.Report("Removed " + uid);
        }
    }
}
