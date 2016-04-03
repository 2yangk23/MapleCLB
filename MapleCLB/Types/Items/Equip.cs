using MapleLib.Packet;

namespace MapleCLB.Types.Items {
    public enum Potential : byte {
        NONE = 0,
        HIDDEN_RARE = 1,
        HIDDEN_EPIC = 2,
        HIDDEN_UNIQUE = 3,
        HIDDEN_LEGEND = 4,
        RARE = 17,
        EPIC = 18,
        UNIQUE = 19,
        LEGEND = 20
    }

    public sealed class Equip : Item {
        public Potential Potential { get; private set; }
        public byte Enhancements { get; private set; }
        // TODO: Add all the stats :D

        public Equip(ItemType type, int id, short slot) : base(type, id, slot) { }

        internal override void Parse(PacketReader pr) {
            pr.Next(0xFF); // Skip item stats
            // [00 11 00 00] Unknown
            if (pr.ReadShort() != 0) {
                pr.Skip(2);
            } else {
                pr.Skip(-2);
            }
            // [Other Creator] [Potential (1)] [Enhancements (1)]
            pr.ReadMapleString();
            Potential = (Potential) pr.ReadByte();
            Enhancements = pr.ReadByte();
            // [Potential (2 * 3)] [Bonus Potential (2 * 3)] 00 00 [Socket State (2)] [Socket (2 * 3)]
            // [Database Id (8)] [Timestamp (8)] FF FF FF FF [Zero (8)] [Timestamp (8)] [Zero (20)] 00 00
            pr.Skip(22 + 58);
        }
    }
}
