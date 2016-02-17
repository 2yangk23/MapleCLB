using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Portal {
        //TODO: This needs to go in client so each client has its counter
        public static byte Count = 1;

        public static byte[] Enter(int crc, string command, short x, short y) {
            var pw = new PacketWriter(SendOps.CHANGE_MAP);
            pw.WriteByte(Count);
            pw.WriteInt(-1); // FF FF FF FF
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
