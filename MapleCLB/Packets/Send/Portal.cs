using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    class Portal {
        public static byte Count = 1;

        public static byte[] Enter(int crc, string command, short x, short y) {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.CHANGE_MAP);
            pw.WriteByte(Count);
            pw.WriteHexString("FF FF FF FF"); //what is this
            pw.WriteInt(crc);
            pw.WriteMapleString(command);
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteZero(3); //what is this

            if (++Count > 255) //loop around
                Count = 1;

            return pw.ToArray();
        }
    }
}
