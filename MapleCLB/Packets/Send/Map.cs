using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Map {
        public static byte[] LootItem(short x, short y, uint id, uint crc) {
            var pw = new PacketWriter(SendOps.LOOT_ITEM);
            pw.WriteByte(1);
            pw.Timestamp();
            pw.WriteShort(x);
            pw.WriteShort(y);
            pw.WriteUInt(id);
            pw.WriteUInt(crc);
            pw.WriteBytes(0x01, 0xE0, 0x0B, 0xBC, 0x00, 0xE0, 0x0B, 0xBC, 0x00); // I don't even

            return pw.ToArray();
        }
    }
}
