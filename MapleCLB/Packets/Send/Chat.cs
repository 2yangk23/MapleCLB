using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    class Chat {
        public static byte[] All(string msg) {
            var pw = new PacketWriter();
            pw.WriteShort();//SendOps.GENERAL_CHAT);
            pw.Timestamp();
            pw.WriteMapleString(msg);
            pw.WriteByte();

            return pw.ToArray();
        }

        // whisper header is used for find?? function = 5 no message
        // [Func 05 (1)] [Timestamp (4)] [IGN]
        public static byte[] Whisper(string ign, string msg) {
            var pw = new PacketWriter();
            pw.WriteShort();//SendOps.WHISPER);
            pw.WriteByte(6);
            pw.Timestamp();
            pw.WriteMapleString(ign);
            pw.WriteMapleString(msg);

            return pw.ToArray();
        }

        /* Types
         * 00 = Buddy
         * 01 = Party
         * 02 = Guild
         * 03 = Alliance
         * 06 = Expedition
         */
        public static byte[] Send(byte type, int[] uid, string msg) {
            var pw = new PacketWriter();
            pw.WriteShort();//SendOps.SEND_CHAT);
            pw.WriteByte(type);
            pw.WriteByte((byte)uid.Length);
            for (int i = 0; i < uid.Length; ++i) {
                pw.WriteInt(uid[i]);
            }
            pw.WriteMapleString(msg);

            return pw.ToArray();
        }
    }
}
