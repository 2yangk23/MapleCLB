using System;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Types.Items {
    public enum Flag : short {
        NONE = 0x0,                 // 0000 0000 0000 0000
        LOCK = 0x1,                 // 0000 0000 0000 0001
        NO_SLIP = 0x2,              // 0000 0000 0000 0010
        COLD_RESIST = 0x4,          // 0000 0000 0000 0100
        UNTRADEABLE = 0x8,          // 0000 0000 0000 1000
        KARMA = 0x10,               // 0000 0000 0001 0000
        CHARM = 0x20,               // 0000 0000 0010 0000
        ANDROID_ACTIVATED = 0x40,   // 0000 0000 0100 0000
        CRAFTED = 0x80,             // 0000 0000 1000 0000
        CURSE_PROTECTION = 0x100,   // 0000 0001 0000 0000
        LUCKY_DAY = 0x200,          // 0000 0010 0000 0000
        KARMA_ACC_USE = 0x400,      // 0000 0100 0000 0000
        KARMA_ACC = 0x1000,         // 0001 0000 0000 0000
        SHIELD = 0x2000,            // 0010 0000 0000 0000
        SCROLL_PROTECTION = 0x4000, // 0100 0000 0000 0000

        KARMA_USE = 0x2,
    }

    public class Other : Item {
        public short Quantity { get; set; }
        public Flag Flag { get; set; }

        public new static Other Parse(PacketReader pr, byte temp) {
            var o = Item.Parse(pr,temp) as Other;
            if (o == null) {
                throw new InvalidCastException("Error casting item-type Other.");
            }

            o.Quantity = pr.ReadShort();
            pr.ReadMapleString();
            o.Flag = (Flag) pr.ReadShort();
            if (o.IsThrowingStar || o.IsFamiliar || o.IsBullet) {
                pr.Skip(8);
            }

            return o;
        }

        public int IdBase => Id / 10000;

        public bool IsAmmo => IsThrowingStar || IsBullet;
        public bool IsBowArrow => Id >= 2060000 && Id < 2061000;
        public bool IsXbowArrow => Id >= 2061000 && Id < 2062000;
        public bool IsThrowingStar => IdBase == 207;
        public bool IsSummonSack => IdBase == 210;
        public bool IsBullet => IdBase == 233;
        public bool IsMonsterCard => IdBase == 238;
        public bool IsFamiliar => IdBase == 287;
    }
}
