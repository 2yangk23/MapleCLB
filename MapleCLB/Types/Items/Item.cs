using System;
using System.Threading;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Types.Items {
    public enum Type : byte {
        EQUIP = 1,
        OTHER = 2,
        PET = 3
    }

    public abstract class Item {
        public Type Type { get; set; }
        public int Id { get; set; }
        public short Slot { get; set; }

        protected static Item Parse(PacketReader pr) {
            // [Type (1)] [Id (4)] [Flag (1) ? UniqueId (8)] [Timestamp (8)] FF FF FF FF
            var type = (Type) pr.ReadByte();
            int id = pr.ReadInt();
            if (pr.ReadBool()) {
                pr.ReadLong();
            }
            pr.Skip(8); // Used to check for expiration?
            pr.Skip(4);
            switch (type) {
                case Type.EQUIP:
                    return new Equip {
                        Type = type,
                        Id = id
                    };
                case Type.OTHER:
                    return new Other {
                        Type = type,
                        Id = id
                    };
                default:
                    throw new InvalidOperationException("Unsupported item type: " + type);
            }
        }
    }
}
