using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Types;
using MapleCLB.Types.Items;

namespace MapleCLB.Packets.Recv {
    class Load {
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
                var m = Mapler.Parse(pr, 8);

                /* AddPlayer Appearance */
                pr.Skip(15); // [Gender (1)] [Skin (1)] [Face (4)] [Job (2)] [SubJob (2)] [Mega (1)] [Hair (4)]
                for (int j = 0; j < 3; ++j) { // Skips the Equipment
                    pr.Next(0xFF);
                }
                pr.Skip(4); // [00 00 00 00]

                pr.Skip(24); // [Weapon (4)] [Shield (4)] [Mercedes Ears (1)] [Zeros (15)]
                if ((m.Job >= 3100 && m.Job <= 3122) || (m.Job >= 3600 && m.Job <= 3612) || m.Job == 3002 || m.Job == 3001) { // Demon/Xenon
                    pr.Skip(4); // [FaceMark (4)]
                } else if (m.Job >= 11200 && m.Job <= 11212) { // Beast Tamer
                    pr.Skip(14); // [FaceMark (4)] [Ears (1)] [EarType (4)] [Tail (1)] [TailType (4)]
                }

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

        public static void CharInfo(Client c, PacketReader pr){
            pr.Skip(18);    //[02 00 01 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00]
            int channel = pr.ReadInt(); //CH Connected To
            pr.Skip(10);    // [00 00 00 00 00 01 00 00 00 00]
            pr.Skip(8);     // Unknown 8 Bytes that change
            pr.Skip(3);     // [01 00 00]
            pr.Skip(12);    // Unknown 12 bytes something to do with connection
            pr.Skip(8);     // [FF FF FF FF FF FF FF FF]
            pr.Skip(13);    // [00 F1 FF FF FF F1 FF FF FF F1 FF FF FF] Where F1 Changes to random Fx Value
            pr.Skip(6);     // [00 00 00 00 00 00]
            pr.ReadInt();   // [uid (4)]

            /* Character Stats */
            var m = Mapler.Parse(pr, 5);

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
            pr.Skip(12);    // [Zero (12)]
            pr.Skip(4);     // [uid (4)]
            pr.Skip(31);    // [Zero (28)] 00 00 00
            pr.Skip(5);     // [Equip Slots (1)] [Use Slots (1)] [Set-up Slots (1)] [Etc Slots (1)] [Cash Slots (1)]
            pr.Skip(8);     // [Timestamp (8)]
            pr.Skip(1);     // [00]

            /* Equipped Items */
            short slot;
            while ((slot = pr.ReadShort()) != 0) {
                var itemTest = Equip.Parse(pr);
                itemTest.Slot = slot;
                //c.currentEquipInventory[itemTest.Id] = 1; ToDo : Equipped Inventory
                c.WriteLog.Report("Other: " + itemTest.Id + " Other Type: " + itemTest.Type + " Potential: " + itemTest.Potential);
            }
            /* Equipped CS Items */
            while ((slot = pr.ReadShort()) != 0) {
                var itemTest = Equip.Parse(pr);
                itemTest.Slot = slot;
                //c.currentEquipInventory[itemTest.Id] = 1; ToDo : Equipped Inventory
                c.WriteLog.Report("Other: " + itemTest.Id + " Other Type: " + itemTest.Type + " Potential: " + itemTest.Potential);
            }
            /* Equip Inventory */
            while ((slot = pr.ReadShort()) != 0) {
                var itemTest = Equip.Parse(pr);
                itemTest.Slot = slot;
                c.currentEquipInventory[c.EquipToString[itemTest.Id]] = 1;
                c.WriteLog.Report("Other: " + itemTest.Id + " Other Type: " + itemTest.Type + " Potential: " + itemTest.Potential);
            }
            /* [Zero (24)] */
            pr.Skip(24);
            /* Use Inventory */
            while ((slot = pr.ReadByte()) != 0) {
                var itemTest = Other.Parse(pr);
                itemTest.Slot = slot;
                c.currentUseInventory[c.UseToString[itemTest.Id]] = itemTest.Quantity;
                c.WriteLog.Report("Other: " + itemTest.Id + " Other Type: " + itemTest.Type +" Quantity: " + itemTest.Quantity);
            }
            /* Set-up Inventory */
            while ((slot = pr.ReadByte()) != 0) {
                var itemTest = Other.Parse(pr);
                itemTest.Slot = slot;
                c.currentSetUpInventory[c.SetUpToString[itemTest.Id]] = itemTest.Quantity;
                c.WriteLog.Report("Other: " + itemTest.Id + " Other Type: " + itemTest.Type + " Quantity: " + itemTest.Quantity);
            }
            /* Etc Inventory */
            while ((slot = pr.ReadByte()) != 0) {
                var itemTest = Other.Parse(pr);
                itemTest.Slot = slot;
                c.currentEtcInventory[c.EtcToString[itemTest.Id]] = itemTest.Quantity;
                c.WriteLog.Report("Other: " + itemTest.Id + " Other Type: " + itemTest.Type + " Quantity: " + itemTest.Quantity);
            }
            /* Cash Inventory */
            while ((slot = pr.ReadByte()) != 0) {
                var itemTest = Other.Parse(pr);
                itemTest.Slot = slot;
                c.currentEquipInventory[c.EquipToString[itemTest.Id]] = itemTest.Quantity;
                c.WriteLog.Report("Other: " + itemTest.Id + " Other Type: " + itemTest.Type + " Quantity: " + itemTest.Quantity);
            }

            c.Mapler = m;
            c.Channel = (byte) (channel + 1);

            c.UpdateMapler.Report(m);
            c.UpdateChannel.Report(c.Channel);
        }
    }
}
