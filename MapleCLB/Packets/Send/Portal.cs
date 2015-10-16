using MaplePacketLib;

namespace MapleCLB.Packets {
    class Portal {
        public static byte count = 1;

        public static PacketWriter Enter(int crc, string command, short x, short y) {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.CHANGE_MAP);
            pw.WriteByte(count);
            pw.WriteHexString("FF FF FF FF"); //what is this
            pw.WriteInt(crc);
            pw.WriteMapleString(command);
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteZero(3); //what is this

            count++; //increase count
            if (count > 255) //loop around
                count = 1;

            return pw;
        }
    }
}
