﻿using MapleCLB.Types;
using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Movement {
        public static byte[] Teleport(byte count, int crc, Position pos, short fh) {
            var pw = new PacketWriter(SendOps.MOVE_PLAYER);
            short temper = -8188;
            pw.WriteByte(count);
            pw.WriteInt(crc);
            pw.Timestamp();
            pw.WriteZero(5);
            pw.WritePosition(pos);
            pw.WriteZero(4);
            pw.WriteShort(1); //number of movement things  1 minimum
            pw.WritePosition(pos);
            pw.WriteZero(4);
            pw.WriteShort(fh);
            pw.WriteZero(4);
            pw.WriteShort(temper); //Animation
            pw.WriteZero(12);

            return pw.ToArray();
        }

        public static byte[] beforeTeleport() {
            var pw = new PacketWriter(SendOps.BEFORE_MOVE);
            pw.Timestamp();
            pw.WriteZero(2);

            return pw.ToArray();
        }
    }
}
