using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    class General {
        public static byte[] Pong() {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.PONG);
            pw.Timestamp(); //Is this actually timestamp?

            return pw.ToArray();
        }


        public static byte[] RandomChannel()
        {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.CHANGE_CHANNEL);
            pw.WriteByte(0x03);//fix later so its acutally random :D
            pw.Timestamp();

            return pw.ToArray();
        }

        public static byte[] ChangeChannel(byte channel) {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.CHANGE_CHANNEL);
            pw.WriteByte(channel);
            pw.Timestamp();

            return pw.ToArray();
        }

        public static byte[] EnterCS() {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.ENTER_CASHSHOP);
            pw.Timestamp();
            pw.WriteByte();

            return pw.ToArray();
        }


        public static byte[] ExitCS() {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.CHANGE_MAP);

            return pw.ToArray();
        }
    }
}
