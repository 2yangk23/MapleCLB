using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Login {
        public static byte[] Validate(byte locale, short mapleversion, short subversion) {
            var pw = new PacketWriter(SendOps.CLIENT_HELLO);
            pw.WriteByte(locale);
            pw.WriteShort(mapleversion);
            pw.WriteShort(subversion);
            pw.WriteZero(1);

            return pw.ToArray();
        }

        public static byte[] GetServers() {
            var pw = new PacketWriter(SendOps.GET_SERVERS);

            return pw.ToArray();
        }

        public static byte[] ClientLogin(string password, string auth, int hwid1, short hwid2) {
            var pw = new PacketWriter(SendOps.CLIENT_LOGIN);
            pw.WriteMapleString(password);
            pw.WriteMapleString(auth);
            pw.WriteZero(6);
            pw.WriteInt(hwid1);
            pw.WriteZero(4);
            pw.WriteInt(hwid2);
            pw.WriteZero(2);
            pw.WriteByte(2);
            pw.WriteZero(6);

            return pw.ToArray();
        }

        // Select world + channel
        public static byte[] SelectServer(byte world, byte channel) {
            var pw = new PacketWriter(SendOps.SERVER_LOGIN);
            pw.WriteByte(0x02);
            pw.WriteByte(world);
            pw.WriteByte(channel);
            pw.Timestamp(); //Is this actually timestamp?

            return pw.ToArray();
        }

        public static byte[] SelectCharacter(int uid, string pic) {
            var pw = new PacketWriter(SendOps.CHAR_SELECT);
            pw.WriteMapleString(pic);
            pw.WriteInt(uid);
            pw.WriteByte();
            //pw.WriteHexString("11 00 37 41 2D 37 39 2D 31 39 2D 41 46 2D 41 38 2D 44 44");
            pw.WriteHexString(Tools.HexEncoding.MacAddress(uid));
            //pw.WriteMapleString(Tools.HexEncoding.GetRandomHexString(6, "-")); //Blank works too! (MAC address)
            pw.WriteMapleString(Tools.HexEncoding.GetRandomHexString(6) + "_" + Tools.HexEncoding.GetRandomHexString(4)); //Blank works too! (HWID)

            return pw.ToArray();
        }

        public static byte[] EnterServer(int worldId, int uid, long sessionId, int hwid1, short hwid2) {
            var pw = new PacketWriter(SendOps.PLAYER_LOGGEDIN);
            pw.WriteInt(worldId);
            pw.WriteInt(uid);
            pw.WriteZero(6);
            pw.WriteInt(hwid1);
            pw.WriteZero(4);
            pw.WriteInt(hwid2);
            pw.WriteLong(sessionId);

            return pw.ToArray();
        }
    }
}
