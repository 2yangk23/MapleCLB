using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    class Npc {
        public static byte[] Talk(int id, short x = 0, short y = 0) {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK);
            pw.WriteInt(id);
            pw.WriteShort(x);
            pw.WriteShort(y);

            return pw.ToArray();
        }

        public static byte[] Select(int n) {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(5);
            pw.WriteByte(1);
            pw.WriteInt(n);

            return pw.ToArray();
        }

        public static byte[] Yes() {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(2);
            pw.WriteByte(1);

            return pw.ToArray();
        }

        public static byte[] No() {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(2);
            pw.WriteByte();

            return pw.ToArray();
        }

        public static byte[] Next() {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte();
            pw.WriteByte(1);

            return pw.ToArray();
        }

        public static byte[] End() {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(5);
            pw.WriteByte();

            return pw.ToArray();
        }
    }
}
