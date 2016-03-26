using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Trade {
        public static byte[] Close() {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(0x1C);

            return pw.ToArray();
        }

        public static byte[] UseMushy(int slot) {
            var pw = new PacketWriter(SendOps.USE_MUSHY);
            pw.WriteByte();
            pw.WriteInt(slot);

            return pw.ToArray();
        }

        /* type: 05 = permit, 06 = mushy */
        public static byte[] CreateShop(byte type, string title, short slot, int id) {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(0x10);
            pw.WriteByte(type);
            pw.WriteMapleString(title);
            pw.WriteByte();
            pw.WriteShort(slot);
            pw.WriteInt(id);

            return pw.ToArray();
        }

        public static byte[] PutItem(byte inventory, byte type, short slot, short amount, short stacks, long price) {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(type); //This Might change? 41 = permit, 21 = mush
            pw.WriteByte(inventory);
            pw.WriteShort(slot);
            pw.WriteShort(amount);
            pw.WriteShort(stacks);

            pw.WriteLong(price);

            return pw.ToArray();
        }

        public static byte[] OpenShop() {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteBytes(0x1A, 0x01);

            return pw.ToArray();
        }

        public static byte[] OpenShop2() {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(82);

            return pw.ToArray();
        }

        public static byte[] CloseShop() {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(0x34);

            return pw.ToArray();
        }
    }
}
