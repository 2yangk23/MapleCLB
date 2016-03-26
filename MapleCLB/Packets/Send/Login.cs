using MapleLib.Packet;
using MapleCLB.Types;

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

        public static byte[] ClientLogin(Account account, string auth) {
            var pw = new PacketWriter(SendOps.CLIENT_LOGIN);
            pw.WriteBool(true);
            pw.WriteMapleString(account.Password);
            pw.WriteMapleString(auth);
            pw.WriteZero(6);
            pw.WriteInt(account.Hwid1);
            pw.WriteZero(4);
            pw.WriteInt(account.Hwid2);
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

        public static byte[] SelectCharacter(Account account, int uid) {
            var pw = new PacketWriter(SendOps.CHAR_SELECT);
            pw.WriteMapleString(account.Pic);
            pw.WriteInt(uid);
            pw.WriteByte();
            pw.WriteMapleString(account.MacAddress);
            pw.WriteMapleString(account.Hwid);
            //pw.WriteMapleString(Tools.HexEncoding.RandomHexString(6, '-')); //Blank works too! (MAC address)
            //pw.WriteMapleString(Tools.HexEncoding.RandomHexString(6) + "_" + Tools.HexEncoding.RandomHexString(4)); //Blank works too! (HWID)

            return pw.ToArray();
        }

        public static byte[] EnterServer(Account account, int uid, long sessionId) {
            var pw = new PacketWriter(SendOps.PLAYER_LOGGEDIN);
            pw.WriteInt(account.World);
            pw.WriteInt(uid);
            pw.WriteZero(6);
            //pw.WriteHexString("68 05 CA 18 48 CF");
            pw.WriteInt(account.Hwid1);
            pw.WriteZero(4);
            pw.WriteInt(account.Hwid2);
            pw.WriteLong(sessionId);

            return pw.ToArray();
        }
    }
}
