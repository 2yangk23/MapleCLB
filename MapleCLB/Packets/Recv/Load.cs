using System;
using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Types;
using MapleCLB.Packets.Send;

namespace MapleCLB.Packets.Recv {
    class Load {
        //[Header (2)] 00 [Char count (1)] [UID (4)] [IGN (13)] ...
        public static void Character(Client c, PacketReader pr) {
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
                var chr = Types.Character.Parse(pr, 8);

                /* Character Appearance */
                pr.Skip(15); // [Gender (1)] [Skin (1)] [Face (4)] [Job (2)] [SubJob (2)] [Mega (1)] [Hair (4)]
                for (int j = 0; j < 3; ++j) { // Skips the Equipment
                    pr.Next(0xFF);
                }
                pr.Skip(4); // [00 00 00 00]

                pr.Skip(24); // [Weapon (4)] [Shield (4)] [Mercedes Ears (1)] [Zeros (15)]
                if ((chr.Job >= 3100 && chr.Job <= 3122) || (chr.Job >= 3600 && chr.Job <= 3612) || chr.Job == 3002 || chr.Job == 3001) { // Demon/Xenon
                    pr.Skip(4); // [FaceMark (4)]
                } else if (chr.Job >= 11200 && chr.Job <= 11212) { // Beast Tamer
                    pr.Skip(14); // [FaceMark (4)] [Ears (1)] [EarType (4)] [Tail (1)] [TailType (4)]
                }

                bool hasRank = pr.ReadBool(); // [HasRanking (1)]
                if (hasRank) {
                    pr.Skip(16); // [Rank (4)] [Rank Move (4)] [JobRank (4)] [JobRank Move (4)]
                }

                if (chr.Job >= 10100 && chr.Job <= 10112) { // Zero
                    for (int j = 0; j < 6; ++j) { // I guess Zero has 2 extra appearance?
                        pr.Next(0xFF);
                    }
                }
                // System.Diagnostics.Debug.WriteLine("" + chr.Id + " : " + chr.Job + " : " + chr.Name + Environment.NewLine);
                c.CharMap.Add(i, chr.Name.ToLower(), chr.Id);
            }
        }

        public static void Player(Client c, PacketReader pr) {
            int uid = pr.ReadInt();
            pr.ReadByte();
            string ign = pr.ReadMapleString();

            c.UidMap[uid] = ign;
            c.WriteLog.Report("Added " + uid + "(" + ign + ")");
        }

        public static void RemovePlayer(Client c, PacketReader pr) {
            int uid = pr.ReadInt();

            c.UidMap.Remove(uid);
            c.WriteLog.Report("Removed " + uid);
        }

        public static void MapLoad(Client c, PacketReader pr){
            pr.Skip(18); //[02 00 01 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00]
            int channel = pr.ReadInt(); //CH Connected To
            pr.Skip(10); // [00 00 00 00 00 01 00 00 00 00]
            pr.Skip(8); //Unknown 8 Bytes that change
            pr.Skip(3); // [01 00 00]
            pr.Skip(12);// Unknown 12 bytes something to do with connection
            pr.Skip(8); // [FF FF FF FF FF FF FF FF]
            pr.Skip(13); // [00 F1 FF FF FF F1 FF FF FF F1 FF FF FF] Where F1 Changes to random Fx Value
            pr.Skip(6); // [00 00 00 00 00 00]
            pr.ReadInt(); // [uid (4)]

            /* Character Stats */
            var chr = Types.Character.Parse(pr, 5);

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
            long MESOS = pr.ReadLong();
            pr.Skip(12);    // [Zero (12)]
            pr.Skip(4);     // [uid (4)]
            pr.Skip(31);    // [Zero (28)] 00 00 00
            pr.Skip(5);     // [Equip Slots (1)] [Use Slots (1)] [Set-up Slots (1)] [Etc Slots (1)] [Cash Slots (1)]
            pr.Skip(8);     // [Timestamp (8)]
            pr.Skip(1);     // [00]

            /* Equipped Items */
            while (pr.ReadByte() != 0) //Skip First Extra Zero 
            {
                pr.Skip(1); //Skip the extra byte until the extra zeros between next items
                var itemTest = Types.Items.Parse(pr);
                c.WriteLog.Report("Item: " + itemTest.Id + " Item Type: " + itemTest.itemType + " Quantity: " + itemTest.Quantity + " Potential: " + itemTest.PotentialLevel);
            }
            pr.Skip(1); //Skip Extra Zero
            /* Equipped CS Items */
            while (pr.ReadByte() != 0) //Skip First Extra Zero 
            {
                pr.Skip(1); //Skip the extra byte until the extra zeros between next items
                var itemTest = Types.Items.Parse(pr);
                c.WriteLog.Report("Item: " + itemTest.Id + " Item Type: " + itemTest.itemType + " Quantity: " + itemTest.Quantity + " Potential: " + itemTest.PotentialLevel);
            }
            pr.Skip(1);
            /* Equip Inventory */
            while (pr.ReadByte() != 0) //Skip First Extra Zero 
            {
                pr.Skip(1); //Skip the extra byte until the extra zeros between next items
                var itemTest = Types.Items.Parse(pr);
                c.WriteLog.Report("Item: " + itemTest.Id + " Item Type: " + itemTest.itemType + " Quantity: " + itemTest.Quantity + " Potential: " + itemTest.PotentialLevel);
            }
            pr.Skip(25);
            /* Use Inventory */
            while (pr.ReadByte() != 0) //Skip First Extra Zero 
            {
                var itemTest = Types.Items.Parse(pr);
                c.WriteLog.Report("Item: " + itemTest.Id + " Item Type: " + itemTest.itemType +" Quantity: " + itemTest.Quantity);
            }
            /* Set-up Inventory */
            while (pr.ReadByte() != 0) //Skip First Extra Zero 
            {
                var itemTest = Types.Items.Parse(pr);
                c.WriteLog.Report("Item: " + itemTest.Id + " Item Type: " + itemTest.itemType + " Quantity: " + itemTest.Quantity);
            }
            /* Etc Inventory */
            while (pr.ReadByte() != 0) //Skip First Extra Zero 
            {
                var itemTest = Types.Items.Parse(pr);
                c.WriteLog.Report("Item: " + itemTest.Id + " Item Type: " + itemTest.itemType + " Quantity: " + itemTest.Quantity);
            }
            /* Cash Inventory */
            while (pr.ReadByte() != 0) //Skip First Extra Zero 
            {
                var itemTest = Types.Items.Parse(pr);
                c.WriteLog.Report("Item: " + itemTest.Id + " Item Type: " + itemTest.itemType + " Quantity: " + itemTest.Quantity);
            }

            c.MapId = chr.Map;
            c.Level = chr.Level;
            c.Mesos = MESOS;
            c.ch = channel + 1;
            c.Name = chr.Name;

            c.UpdateName.Report(c.Name);
            c.UpdateMap.Report(c.MapId);
            c.UpdateLevel.Report(c.Level);
            c.UpdateChannel.Report(c.ch);
            c.UpdateMesos.Report(c.Mesos);
        }

        public static void Mushrooms(Client c, PacketReader pr) {
            int uid = pr.ReadInt();
            pr.Skip(4);
            short x = pr.ReadShort();
            short y = pr.ReadShort();
            short pid = pr.ReadShort();
            string ign = pr.ReadMapleString();

            int FM1CRC = 0x28C27A2A;

            //IGN -> UID 
            try {
                //c.IgnUid.Add(ign,uid);
                c.IgnUid[ign] = uid;
            } catch (Exception) {
                c.WriteLog.Report("Error loading mushrooms");
            }
            c.WriteLog.Report("Added : "+ ign +" to UID : "+uid +" @ "+x +" " +y);
            try {
                c.UidMovementPacket[uid] = HexEncoding.ToHexString(Movement.Teleport(FM1CRC, x, y, pid));
                // c.UidMovementPacket.Add(uid, HexEncoding.ToHexString(Movement.Teleport(FM1CRC, x, y, pid)));
            } catch (Exception) {
                c.WriteLog.Report("Error adding UID to Movement Packet");
            }
                    
        }
    }
}
