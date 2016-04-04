using MapleCLB.Types.Items;
using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class General {
        public static byte[] Pong() {
            var pw = new PacketWriter(SendOps.PONG);
            pw.Timestamp(); //Is this actually timestamp?

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
            var pw = new PacketWriter(SendOps.ENTER_PORTAL);

            return pw.ToArray();
        }

        public static byte[] DropItem(InventoryTab tab, byte slot, short amount) {
            var pw = new PacketWriter(SendOps.DROP_ITEM);
            pw.Timestamp();
            pw.WriteByte((byte) tab);
            pw.WriteByte(slot);
            //pw.WriteInt(1);
            pw.WriteZero(3);
            pw.WriteShort(amount);

            return pw.ToArray();
        }
    }
}
