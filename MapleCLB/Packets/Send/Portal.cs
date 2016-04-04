using MapleCLB.Types;
using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Portal {
        public static byte[] Enter(byte count, int crc, PortalInfo data) {
            var pw = new PacketWriter(SendOps.ENTER_PORTAL);
            pw.WriteByte(count);
            pw.WriteInt(-1); // FF FF FF FF
            pw.WriteInt(crc);
            pw.WriteMapleString(data.Name);
            pw.WritePosition(data.Position);
            pw.WriteZero(3); //what is this

            return pw.ToArray();
        }

        public static byte[] EnterSpecial(byte count, PortalInfo data) {
            var pw = new PacketWriter(SendOps.SPECIAL_PORTAL);
            pw.WriteByte(count);
            pw.WriteMapleString(data.Name);
            pw.WritePosition(data.Position);

            return pw.ToArray();
        }
    }
}
