using MapleCLB.Types;
using MapleCLB.Types.Map;
using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal class Map {
        public static byte[] LootItem(DroppedItem item) {
            var pw = new PacketWriter(SendOps.LOOT_ITEM);
            pw.WriteByte(1);
            pw.Timestamp();
            pw.WritePosition(item.Position);
            pw.WriteInt(item.Id);
            pw.WriteInt(item.Crc);
            pw.WriteBytes(0x01, 0x72, 0x09, 0x91, 0x01, 0x72, 0x09, 0x91, 0x01); // I don't even

            return pw.ToArray();
        }
    }
}
