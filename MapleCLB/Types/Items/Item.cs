using System;
using MapleLib.Packet;

namespace MapleCLB.Types.Items {
    public enum ItemType : byte {
        UNKNOWN = 0,
        EQUIP = 1,
        OTHER = 2,
        PET = 3
    }

    public abstract class Item {
        public readonly ItemType ItemType;
        public readonly int Id;
        public readonly short Slot;

        protected Item(ItemType type, int id, short slot) {
            ItemType = type;
            Id = id;
            Slot = slot;
        }

        public static T Parse<T>(PacketReader pr) where T : Item {
            short slot = typeof(T) == typeof(Equip) ? pr.ReadShort() : pr.ReadByte();
            if (slot == 0) {
                return (T) Activator.CreateInstance(typeof(T), ItemType.UNKNOWN, 0, slot);
            }

            // [Type (1)] [Id (4)] [Flag (1) ? UniqueId (8)] [Timestamp (8)] FF FF FF FF
            var type = (ItemType) pr.ReadByte();
            int id = pr.ReadInt();
            if (pr.ReadBool()) {
                pr.ReadLong();
            }
            pr.Skip(12); // Used to check for expiration?

            var item = (T) Activator.CreateInstance(typeof(T), type, id, slot);
            item.Parse(pr); // Parses remaining item info, and stores data

            return item;
        }

        protected abstract void Parse(PacketReader pr);
    }
}
