using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    class Shop {

        public static PacketWriter CreateShop(byte type, string title, short slot, int id)
        {
            //Type :05 = permit, 06 = mushy
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.TRADE);
            pw.WriteByte(0x10);
            pw.WriteByte(type);
            pw.WriteMapleString(title);
            pw.WriteByte();
            pw.WriteShort(slot);
            pw.WriteInt(id);

            return pw;
        }


    }
}
