using System;
using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Portal {
        public static byte[] Enter(byte count, int crc, Tuple<short[], string> data) {
            var pw = new PacketWriter(SendOps.CHANGE_MAP);
            pw.WriteByte(count);
            pw.WriteInt(-1); // FF FF FF FF
            pw.WriteInt(crc);
            pw.WriteMapleString(data.Item2);
            pw.WriteShort(data.Item1[0]);
            pw.WriteShort(data.Item1[1]);
            pw.WriteZero(3); //what is this

            return pw.ToArray();
        }
    }
}
