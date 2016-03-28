using System.Collections.Generic;
using MapleLib.Packet;

namespace MapleCLB.Types.Items {
    public class Inventory {
        public long Mesos { get; set; }
        public readonly Dictionary<short, Equip> EquipInventory = new Dictionary<short, Equip>();
        public readonly Dictionary<short, Other> UseInventory = new Dictionary<short, Other>();
        public readonly Dictionary<short, Other> SetupInventory = new Dictionary<short, Other>();
        public readonly Dictionary<short, Other> EtcInventory = new Dictionary<short, Other>();
        public readonly Dictionary<short, Other> CashInventory = new Dictionary<short, Other>();

        public void Clear() {
            EquipInventory.Clear();
            UseInventory.Clear();
            SetupInventory.Clear();
            EtcInventory.Clear();
            CashInventory.Clear();
        }

        public static Inventory Parse(PacketReader pr) {
            var i = new Inventory();

            /* Inventory Info */
            i.Mesos = pr.ReadLong();
            /* [Zero (12)] [uid (4)] [Zero (28)] 00 00 00
             * [Equip Slots (1)] [Use Slots (1)] [Set-up Slots (1)] [Etc Slots (1)] [Cash Slots (1)]
             * [Timestamp (8)] 00
             */
            pr.Skip(47 + 5 + 9);

            //TODO : Equipped Inventory 
            /* Equipped Items */
            while (Item.Parse<Equip>(pr).Slot != 0) { }
            /* Equipped CS Items */
            while (Item.Parse<Equip>(pr).Slot != 0) { }

            /* Equip Inventory */
            Equip equipItem;
            while ((equipItem = Item.Parse<Equip>(pr)).Slot != 0) {
                i.EquipInventory[equipItem.Slot] = equipItem;
            }
            // [Zero (24)]
            pr.Skip(24);
            /* Use Inventory */
            Other otherItem;
            while ((otherItem = Item.Parse<Other>(pr)).Slot != 0) {
                i.UseInventory[otherItem.Slot] = otherItem;
            }
            /* Set-up Inventory */
            while ((otherItem = Item.Parse<Other>(pr)).Slot != 0) {
                i.SetupInventory[otherItem.Slot] = otherItem;
            }
            /* Etc Inventory */
            while ((otherItem = Item.Parse<Other>(pr)).Slot != 0) {
                i.EtcInventory[otherItem.Slot] = otherItem;
            }
            /* Cash Inventory */
            while ((otherItem = Item.Parse<Other>(pr)).Slot != 0) {
                i.CashInventory[otherItem.Slot] = otherItem;
            }

            return i;
        }
    }
}
