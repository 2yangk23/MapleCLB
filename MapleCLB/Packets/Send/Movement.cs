using MaplePacketLib;

namespace MapleCLB.Packets {
    class Movement {
        public static PacketWriter Teleport(int crc, short x, short y, short pid) {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.MOVE_PLAYER);
            pw.WriteByte(Portal.count);
            pw.WriteInt(crc);
            pw.Timestamp();
            pw.WriteZero(5);
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteZero(4);
            pw.WriteShort(1); //number of movement things  1 minimum
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteZero(4);
            pw.WriteShort(pid);
            pw.WriteZero(8);
            // Technically dont need these but I keep for consistency
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteShort(x);
            pw.WriteShort(y);

            return pw;
        }
    }
}
