using MaplePacketLib;

namespace MapleCLB.Packets
{
    class General
    {
        public static PacketWriter Pong()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.PONG);
            pw.Timestamp(); //Is this actually timestamp?

            return pw;
        }

        public static PacketWriter ChangeChannel(byte channel)
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.CHANGE_CHANNEL);
            pw.WriteByte(channel);
            pw.Timestamp();

            return pw;
        }

        public static PacketWriter EnterCS()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.ENTER_CASHSHOP);
            pw.Timestamp();
            pw.WriteByte();

            return pw;
        }

        public static PacketWriter ExitCS()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.CHANGE_MAP);

            return pw;
        }
    }
}
