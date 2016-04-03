using MapleCLB.Types;
using MapleCLB.Types.Map;
using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Map {
        public static byte[] LootItem(Item item) {
            var pw = new PacketWriter(SendOps.LOOT_ITEM);
            pw.WriteByte(1);
            pw.Timestamp();
            pw.WritePosition(item.Position);
            pw.WriteUInt(item.Id);
            pw.WriteUInt(item.Crc);
            pw.WriteBytes(0x01, 0xE0, 0x0B, 0xBC, 0x00, 0xE0, 0x0B, 0xBC, 0x00); // I don't even

            return pw.ToArray();
        }
    }
}
