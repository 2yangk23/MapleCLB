using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class General {
        public static byte[] Pong() {
            var pw = new PacketWriter(SendOps.PONG);
            pw.Timestamp(); //Is this actually timestamp?

            return pw.ToArray();
        }

        public static byte[] RandomChannel() {
            var pw = new PacketWriter(SendOps.CHANGE_CHANNEL);
            pw.WriteByte(0x03); //fix later so its acutally random :D
            pw.Timestamp();

            return pw.ToArray();
        }

        public static byte[] ChangeChannel(byte channel) {
            var pw = new PacketWriter(SendOps.CHANGE_CHANNEL);
            pw.WriteByte(channel);
            pw.Timestamp();

            return pw.ToArray();
        }

        public static byte[] EnterCS() {
            var pw = new PacketWriter(SendOps.ENTER_CASHSHOP);
            pw.Timestamp();
            pw.WriteByte();

            return pw.ToArray();
        }

        public static byte[] ExitCS() {
            var pw = new PacketWriter(SendOps.CHANGE_MAP);

            return pw.ToArray();
        }

        public static byte[] DropItem(byte tab, byte slot) {
            // 01 = Equip tab, 02 = Use tab, 04 = Etc tab, 03 = SetUp Tab
            var pw = new PacketWriter(SendOps.DROP_ITEM);
            pw.Timestamp();
            pw.WriteByte(tab);
            pw.WriteByte(slot);
            pw.WriteInt(1);
            pw.WriteZero(1);
            return pw.ToArray();
        }
    }
}
