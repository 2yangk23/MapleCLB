using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Send;

namespace MapleCLB.Packets.Recv.Map {
    internal class Shop {

        public static void SpawnMushy(object o, PacketReader r) {
            var c = o as Client;
            int uid = r.ReadInt();
            r.Skip(4);
            short x = r.ReadShort();
            short y = r.ReadShort();
            short pid = r.ReadShort();
            string ign = r.ReadMapleString();

            c.UidMap[uid] = ign; 
            c.UidMovementPacket[uid] = Movement.Teleport(Client.FM1CRC, x, y, pid);

            c.WriteLog.Report("Added : " + ign + " to UID : " + uid + " @ " + x + " " + y);
        }

        public static void RemoveMushy(object o, PacketReader r) {
            var c = o as Client;

            int uid = r.ReadInt();
            c.UidMovementPacket.Remove(uid);
        }
    }
}
