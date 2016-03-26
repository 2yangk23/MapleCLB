using MapleCLB.MapleClient;
using MapleCLB.Resources;
using MapleCLB.Types;
using MapleCLB.Types.Items;
using MapleLib.Packet;

namespace MapleCLB.Packets.Recv {
    internal class Load {
        private const int MAGIC_NUM = -1770422;
        //[Header (2)] 00 [Char count (1)] [UID (4)] [IGN (13)] ...
        public static void Charlist(Client c, PacketReader pr) {
            pr.Skip(1);
            pr.ReadMapleString(); // v170?
            pr.Skip(18);

            int temp = pr.ReadInt(); // Weird loopy shit (uids?) v167
            for (int i = 0; i < temp; i++) {
                pr.ReadInt();
            }
            byte count = pr.ReadByte();

            for (byte i = 0; i < count; ++i) {
                /* Character Stats */
                var m = Mapler.Parse(pr);

                /* AddPlayer Appearance */
                Mapler.SkipAppearance(pr, m.Job);

                bool hasRank = pr.ReadBool(); // [HasRanking (1)]
                if (hasRank) {
                    pr.Skip(16); // [Rank (4)] [Rank Move (4)] [JobRank (4)] [JobRank Move (4)]
                }

                if (m.Job >= 10100 && m.Job <= 10112) { // Zero
                    for (int j = 0; j < 6; ++j) { // I guess Zero has 2 extra appearance?
                        pr.Next(0xFF);
                    }
                }
                // System.Diagnostics.Debug.WriteLine("" + chr.Id + " : " + chr.Job + " : " + chr.Name + Environment.NewLine);
                c.CharMap.Add(i, m.Name.ToLower(), m.Id);
            }
        }

        public static void CharInfo(Client c, PacketReader pr) {
            //TODO: Better fix for this? Although this should always work so maybe it's good enough
            if (pr.Available < 100) {
                pr.Skip(44);
                c.Mapler.Map = pr.ReadInt();
                c.UpdateMapler.Report(c.Mapler);
                return;
            }

            pr.Skip(18); // [02 00 01 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00]
            int channel = pr.ReadInt(); //CH Connected To
            /* [00 00 00 00 00 01 00 00 00 00] Unknown 8 Bytes that change
             * [01 00 00] Unknown 12 bytes something to do with connection
             * [FF FF FF FF FF FF FF FF] [00 FX FF FF FF FX FF FF FF FX FF FF FF]
             * [00 00 00 00 00 00 00]
             */
            pr.Skip(18 + 15 + 21 + 7);

            /* Character Stats */
            var m = Mapler.Parse(pr);

            /* Char Info */
            pr.Skip(1); // BL Size
            if (pr.ReadBool()) { // Skips Fairy Blessing
                pr.ReadMapleString();
            }
            if (pr.ReadBool()) { // Skips Empress Blessing
                pr.ReadMapleString();
            }
            if (pr.ReadBool()) { // Skips Ultimate Explorer's Parent
                pr.ReadMapleString();
            }

            /* Inventory Info */
            m.Meso = pr.ReadLong();
            /* [Zero (12)] [uid (4)] [Zero (28)] 00 00 00
             * [Equip Slots (1)] [Use Slots (1)] [Set-up Slots (1)] [Etc Slots (1)] [Cash Slots (1)]
             * [Timestamp (8)] 00
             */
            pr.Skip(47 + 5 + 9);

            /* Equipped Items */
            short slot;
            while ((slot = pr.ReadShort()) != 0) {
                byte type = pr.ReadByte();
                var itemTest = Equip.Parse(pr, type);
                itemTest.Slot = slot;
                c.totalItemCount = c.totalItemCount + 1;
                //c.currentEquipInventory[itemTest.Id] = 1; ToDo : Equipped Inventory
                c.WriteLog.Report("Equipped: " + itemTest.Id + " Item Type: " + itemTest.Type +
                                  " Potential: " + itemTest.Potential);
            }
            /* Equipped CS Items */
            //TODO : Equipped Inventory 
            while ((slot = pr.ReadShort()) != 0) {
                byte type = pr.ReadByte();
                var itemTest = Equip.Parse(pr, type);
                itemTest.Slot = slot;
                c.totalItemCount = c.totalItemCount + 1;
                //c.currentEquipInventory[itemTest.Id] = 1; 
                //c.WriteLog.Report("Cash Equip: " + itemTest.Id + " Item Type: " + itemTest.Type + " Potential: " + itemTest.Potential);
            }

            /* Equip Inventory */
            while ((slot = pr.ReadShort()) != 0) {
                byte type = pr.ReadByte();
                var itemTest = Equip.Parse(pr, type);
                itemTest.Slot = slot;
                c.currentEquipInventory[ItemData.Equip[itemTest.Id]] = 1;
                c.totalItemCount = c.totalItemCount + 1;
                //c.WriteLog.Report("Equip: " + itemTest.Id + " Item Type: " + itemTest.Type + " Potential: " + itemTest.Potential);
            }
            // [Zero (24)]
            pr.Skip(24);
            /* Use Inventory */
            while ((slot = pr.ReadByte()) != 0) {
                byte type = pr.ReadByte();
                var itemTest = Other.Parse(pr, type);
                itemTest.Slot = slot;
                c.currentUseInventory[ItemData.Use[itemTest.Id]] = itemTest.Quantity;
                c.totalItemCount = c.totalItemCount + itemTest.Quantity;
                //c.WriteLog.Report("Use: " + itemTest.Id + " Item Type: " + itemTest.Type +" Quantity: " + itemTest.Quantity);
            }
            /* Set-up Inventory */
            while ((slot = pr.ReadByte()) != 0) {
                byte type = pr.ReadByte();
                var itemTest = Other.Parse(pr, type);
                itemTest.Slot = slot;
                c.currentSetUpInventory[ItemData.Setup[itemTest.Id]] = itemTest.Quantity;
                c.totalItemCount = c.totalItemCount + itemTest.Quantity;
                //c.WriteLog.Report("Setup: " + itemTest.Id + " Item Type: " + itemTest.Type + " Quantity: " + itemTest.Quantity);
            }
            /* Etc Inventory */
            while ((slot = pr.ReadByte()) != 0) {
                byte type = pr.ReadByte();
                var itemTest = Other.Parse(pr, type);
                itemTest.Slot = slot;
                c.currentEtcInventory[ItemData.Etc[itemTest.Id]] = itemTest.Quantity;
                c.totalItemCount = c.totalItemCount + itemTest.Quantity;
                //c.WriteLog.Report("Etc: " + itemTest.Id + " Item Type: " + itemTest.Type + " Quantity: " + itemTest.Quantity);
            }
            /* Cash Inventory */
            //TODO : Create Cash Inventory NOT USING Equips
            while ((slot = pr.ReadByte()) != 0) {
                byte type = pr.ReadByte();
                if (type == 3) {
                    var itemTest = Pet.Parse(pr, type);
                    itemTest.Slot = slot;
                    c.currentEquipInventory[ItemData.Cash[itemTest.Id]] = 1;
                    c.totalItemCount = c.totalItemCount + 1;
                } else {
                    var itemTest = Other.Parse(pr, type);
                    itemTest.Slot = slot;
                    c.currentEquipInventory[ItemData.Cash[itemTest.Id]] = itemTest.Quantity;
                    c.totalItemCount = c.totalItemCount + itemTest.Quantity;
                }
            }

            c.Mapler = m;
            c.Channel = (byte) (channel + 1);

            c.UpdateItems.Report(c.totalItemCount);
            c.UpdateMapler.Report(m);
            c.UpdateChannel.Report(c.Channel);
        }

        public static void Seed(object o, PacketReader pr) {
            var c = o as Client;
            int seed = pr.ReadInt();
            c.PortalCrc = c.Mapler.Id ^ seed ^ MAGIC_NUM;
            c.PortalCount = 1;
        }
    }
}
