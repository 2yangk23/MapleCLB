using MaplePacketLib;

namespace MapleCLB.Packets
{
    class NPC
    {
        public static PacketWriter Talk(int id, short x = 0, short y = 0)
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK);
            pw.WriteInt(id);
            pw.WriteShort(x);
            pw.WriteShort(y);

            return pw;
        }

        public static PacketWriter Select(int n)
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(5);
            pw.WriteByte(1);
            pw.WriteInt(n);

            return pw;
        }

        public static PacketWriter Yes()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(2);
            pw.WriteByte(1);

            return pw;
        }

        public static PacketWriter No()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(2);
            pw.WriteByte();

            return pw;
        }

        public static PacketWriter Next()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte();
            pw.WriteByte(1);

            return pw;
        }

        public static PacketWriter End()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.NPC_TALK_MORE);
            pw.WriteByte(5);
            pw.WriteByte();

            return pw;
        }
    }
}
