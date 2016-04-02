using System;
using System.Collections.Generic;
using MapleCLB.Packets.Send;
using MapleLib.Packet;

namespace MapleCLB.Types.Items {
    public enum InventoryTab : byte {
        EQUIP = 1,
        USE = 2,
        SETUP = 3,
        ETC = 4,
        CASH = 5
    }

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

        public void Add(InventoryTab tab, Item item) {
            switch (tab) {
                case InventoryTab.EQUIP:
                    var equip = item as Equip;
                    if (equip != null) {
                        EquipInventory[item.Slot] = equip;
                    }
                    return;
                default:
                    var other = item as Other;
                    if (other != null) {
                        GetInventory(tab)[item.Slot] = other;
                    }
                    break;
            }
        }

        public void Update(InventoryTab tab, short slot, short quantity) {
            switch (tab) {
                case InventoryTab.EQUIP:
                    if (quantity == 0) {
                        EquipInventory.Remove(slot);
                    }
                    break;
                default:
                    Dictionary<short, Other> inventory = GetInventory(tab);
                    if (quantity == 0) {
                        inventory.Remove(slot);
                    } else {
                        if (inventory.ContainsKey(slot)) {
                            inventory[slot].Quantity = quantity;
                        }
                    }
                    break;
            }
        }

        public void Move(InventoryTab tab, short src, short dst) {
            switch (tab) {
                case InventoryTab.EQUIP:
                    if (EquipInventory.ContainsKey(src) && EquipInventory.ContainsKey(dst)) {
                        var equip = EquipInventory[src];
                        EquipInventory[src] = EquipInventory[dst];
                        EquipInventory[dst] = equip;
                    }
                    break;
                default:
                    Dictionary<short, Other> inventory = GetInventory(tab);
                    if (inventory.ContainsKey(src) && inventory.ContainsKey(dst)) {
                        var other = inventory[src];
                        inventory[src] = inventory[dst];
                        inventory[dst] = other;
                    }
                    break;
            }
        }

        // Helper method to simplify code, this doesnt handle Equip inventory
        private Dictionary<short, Other> GetInventory(InventoryTab tab) {
            switch (tab) {
                case InventoryTab.USE:
                    return UseInventory;
                case InventoryTab.SETUP:
                    return SetupInventory;
                case InventoryTab.ETC:
                    return EtcInventory;
                case InventoryTab.CASH:
                    return CashInventory;
                default:
                    throw new ArgumentException($"{tab} is not a supported InventoryTab");
            }
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
