using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Movement {
        public static byte[] Teleport(int crc, short x, short y, short pid) {
            var pw = new PacketWriter(SendOps.MOVE_PLAYER);
            pw.WriteByte(Portal.Count);
            pw.WriteInt(crc);
            pw.Timestamp();
            pw.WriteZero(5);
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteZero(4);
            pw.WriteShort(1); //number of movement things  1 minimum
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteZero(10);
            pw.WriteShort(pid);
            pw.WriteZero(3);
            // Technically dont need these but I keep for consistency
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteZero(16);

            return pw.ToArray();

            //CRC = 9F F5 D0 03
            // y = 13 01
            // X = 5C FE 
            //PID = 03 5A
        }

    }
}
