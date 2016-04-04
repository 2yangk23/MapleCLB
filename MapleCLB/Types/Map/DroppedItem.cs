using System.Diagnostics;
using MapleLib.Packet;

namespace MapleCLB.Types.Map {
    internal enum DropType : byte {
        DROPPING = 0,
        VISIBLE = 1,
        SPAWNED = 2,
        DISAPPEARING = 3
    }

    internal class DroppedItem : MapObject {
        internal DropType Type { get; set; }
        internal int Crc { get; set; }
        internal long Timestamp { get; set; }

        internal DroppedItem(int id) : base(id) { }
    }

    internal static class MapItemPacketExtensions {
        internal static DroppedItem ReadMapItem(this PacketReader pr) {
            pr.Skip(1);
            byte type = pr.ReadByte();
            var i = new DroppedItem(pr.ReadInt());
            i.Type = (DropType) type; // [Type (1)]
            if (pr.ReadBool()) { // IsMeso
                pr.Skip(21); // [Zero (12)] [MesoAmount/ItemId (4)] 00 00 00 00 [DropType (1)]
                i.Crc = 0;
            } else {
                pr.Skip(12);
                i.Crc = pr.ReadInt(); // Need to convert to actual crc later
                pr.Skip(5);
            }
            i.Position = pr.ReadPosition(); // [X (2)] [Y (2)]
            i.Timestamp = Stopwatch.GetTimestamp() / Stopwatch.Frequency;

            return i;
        }
    }
}
