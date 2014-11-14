using MaplePacketLib;

namespace MapleCLB.Packets
{
    class Login
    {
        public static PacketWriter Validate(short mapleversion, short subversion)
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.CLIENT_HELLO);
            pw.WriteByte(8);
            pw.WriteShort(mapleversion);
            pw.WriteShort(subversion);

            return pw;
        }

        public static PacketWriter SelectServer(string auth, byte world, byte channel) //weblogin select world+channel
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.SERVER_LOGIN);
            pw.WriteByte(1);
            pw.WriteMapleString(auth);
            pw.WriteZero(6);
            pw.WriteInt(Program.hwid1);
            pw.WriteInt(0);
            pw.WriteInt(Program.hwid2);
            pw.WriteShort(0);
            pw.WriteByte(1);
            pw.WriteByte(world);
            pw.WriteByte(channel);
            pw.Timestamp(); //Is this actually timestamp?

            return pw;
        }

        public static PacketWriter SelectCharacter(int uid, string pic)
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.CHAR_SELECT);
            pw.WriteMapleString(pic);
            pw.WriteInt(uid);
            pw.WriteByte(0);
            pw.WriteMapleString(Tools.HexEncoding.getRandomHexString(6, "-"[0])); //Blank works too! (MAC address)
            pw.WriteMapleString(""); //Blank works too! (HWID)

            return pw;
        }

        public static PacketWriter EnterServer(int worldID, int uid, long sessionID)
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.PLAYER_LOGGEDIN);
            pw.WriteInt(worldID);
            pw.WriteInt(uid);
            pw.WriteZero(6);
            pw.WriteInt(Program.hwid1);
            pw.WriteInt(0);
            pw.WriteInt(Program.hwid2);
            pw.WriteLong(sessionID);

            return pw;
        }
    }
}
