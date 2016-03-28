using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal enum ShopType : byte { PERMIT = 5, MUSHY = 6 }

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

        public static byte[] CreateShop(ShopType type, string title, short slot, int id) {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(0x10);
            pw.WriteByte((byte) type);
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
            pw.WriteByte(0x52);

            return pw.ToArray();
        }

        public static byte[] CollectSales() {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(0x33);

            return pw.ToArray();
        }

        public static byte[] CloseShop() {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(0x34);

            return pw.ToArray();
        }

        public static byte[] ChangeShopName(string name) {
            var pw = new PacketWriter(SendOps.TRADE);
            pw.WriteByte(0x3D); // 3D mushy, 55 permit
            pw.WriteMapleString(name);

            return pw.ToArray();
        }
    }
}
