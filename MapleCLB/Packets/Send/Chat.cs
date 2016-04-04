using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Chat {
        public static byte[] All(string msg) {
            var pw = new PacketWriter(SendOps.GENERAL_CHAT);
            pw.Timestamp();
            pw.WriteMapleString(msg);
            pw.WriteByte();

            return pw.ToArray();
        }

        // whisper header is used for find?? function = 5 no message
        // [Func 05 (1)] [Timestamp (4)] [IGN]
        public static byte[] Whisper(string ign, string msg) {
            var pw = new PacketWriter(SendOps.WHISPER);
            pw.WriteByte(6);
            pw.Timestamp();
            pw.WriteMapleString(ign);
            pw.WriteMapleString(msg);

            return pw.ToArray();
        }
    }
}
