using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    class Login {
        public static byte[] Validate(byte locale, short mapleversion, short subversion)
        {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.CLIENT_HELLO);
            pw.WriteByte(locale);
            pw.WriteShort(mapleversion);
            pw.WriteShort(subversion);
            pw.WriteZero(1);
            return pw.ToArray();
        }

        public static byte[] acceptWorld()
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteBytes(0x82, 0x00);
            return pw.ToArray();
        }

        public static byte[] ClientLogin(string password, string auth) {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.CLIENT_LOGIN);
            pw.WriteMapleString(password);
            pw.WriteMapleString(auth);
            pw.WriteZero(6);
            // pw.WriteInt(Program.Hwid1);
            pw.WriteHexString(Program.Hwid1);
            pw.WriteInt();
            //pw.WriteInt(Program.Hwid1);
            pw.WriteHexString(Program.Hwid2);
            pw.WriteZero(2);
            pw.WriteByte(2);
            pw.WriteZero(6);
            return pw.ToArray();
        }

        // Select world + channel
        public static byte[] SelectServer(byte world, byte channel) {
            PacketWriter pw = new PacketWriter();
            pw.WriteShort(SendOps.SERVER_LOGIN);
            pw.WriteByte(2);
            pw.WriteByte(world);
            pw.WriteByte(channel);
            pw.Timestamp(); //Is this actually timestamp?

            return pw.ToArray();
        }

        public static byte[] SelectCharacter(int uid, string pic)
        {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.CHAR_SELECT);

            pw.WriteMapleString(pic);
            pw.WriteInt(uid);
            pw.WriteByte();
            pw.WriteMapleString(Tools.HexEncoding.GetRandomHexString(6, "-")); //Blank works too! (MAC address)
            pw.WriteMapleString(Program.Hwid1 + "_" + Program.Hwid2);
           // pw.WriteMapleString(Tools.HexEncoding.GetRandomHexString(6) + "_" + Tools.HexEncoding.GetRandomHexString(4)); //Blank works too! (HWID)

            return pw.ToArray();
        }

        public static byte[] EnterServer(int worldId, int uid, long sessionId)
        {
            var pw = new PacketWriter();
            pw.WriteShort(SendOps.PLAYER_LOGGEDIN);
            pw.WriteInt(worldId);
            pw.WriteInt(uid);
            pw.WriteZero(6);
            // pw.WriteInt(Program.Hwid1);
            pw.WriteHexString(Program.Hwid1);
            pw.WriteInt();
            // pw.WriteInt(Program.Hwid2);
            pw.WriteHexString(Program.Hwid2);
            pw.WriteLong(sessionId);

            return pw.ToArray();
        }
    }
}
