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

            // List<Character> charList = new List<Character>(count);

            for (byte i = 0; i < count; ++i) {
                var chr = new Character();
                /* Character Stats */
                chr.Id = pr.ReadInt();
                pr.Skip(8); // some stuff v167
                chr.Name = pr.ReadString(13).TrimEnd('\0');
                pr.Skip(13); //[Gender (1)] [Skin (1)] [Face (4)] [Hair (4)] [FF 00 00] [Level (1)] [Job (2)]
                chr.Level = pr.ReadByte();
                chr.Job = pr.ReadShort();
                //pr.Skip(26); //[str (2)] [dex (2)] [int (2)] [luk (2)] [hp (4)] [maxhp (4)] [mp (4)] [maxmp (4)] [Unused AP (2)]
                chr.Str = pr.ReadShort();
                chr.Dex = pr.ReadShort();
                chr.Int = pr.ReadShort();
                chr.Luk = pr.ReadShort();

                chr.Hp = pr.ReadInt();
                chr.MaxHp = pr.ReadInt();
                chr.Mp = pr.ReadInt();
                chr.MaxMp = pr.ReadInt();

                chr.Ap = pr.ReadShort();

                temp = pr.ReadByte(); // Separated SP
                if (temp > 4)
                    temp = pr.ReadByte();
                for (int j = 0; j < temp; ++j)
                    pr.Skip(5);

                /* Correct way to do separated sp
                pw.WriteShort(chr.AP);
                if (!chr.IsSeparatedSpJob)
                    pw.WriteShort((short)chr.SpTable[0]);
                else
                    AddSeparatedSP(chr, pw);
                 */

                // pr.Skip(24); // [Exp (8)] [Fame (4)] [GachExp (4)] [?? (4)] [MapId (4)]
                chr.Exp = pr.ReadLong();
                chr.Fame = pr.ReadInt();
                pr.Skip(8);
                chr.Map = pr.ReadInt();

                pr.Skip(7); // [SpawnPoint (1)] 00 00 00 00 [SubJob (2)] [(Demon, Xenon, Beast Tamer) ? FaceMark (4)]
                if ((chr.Job >= 3100 && chr.Job <= 3122) || (chr.Job >= 3600 && chr.Job <= 3612) || chr.Job == 3002 || chr.Job == 3001) { // Demon/Xenon
                    pr.Skip(4);
                } else if (chr.Job >= 11200 && chr.Job <= 11212) { // Beast Tamer
                    pr.Skip(4);
                }

                pr.Skip(5);     // [Fatigue (1)] [Date (4)]
                pr.Skip(24);    // [Ambition (4)] [Insight (4)] [Willpower (4)] [Dilligence (4)] [Empathy (4)] [Charm (4)]
                pr.Skip(21);    // [Zeros (13)] [00 40 E0 FD] [3B 37 4F 01]
                pr.Skip(15);    // [PvP Exp (4)] [PvP Rank (1)] [Battle Pts (4)] [Byte (1)] [Byte (1)] [Int (4)]
                pr.Skip(1);     // part time job action of resting = 1, herbalism= 2, Mining = 3, general store = 4, Weapon and armor store = 5
                pr.Skip(13);    // [3B 37 4F 01] [00 40 E0 FD] [00 00 00 00] [00]
                pr.Skip(81);    // Character Cards 9 bytes each
                pr.Skip(8);     // [Last Login (8)]
                pr.Skip(1);     // 00

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

            /*foreach (var chara in charList) {
                chara.Print();
                Console.WriteLine();
            }*/
        }

        public static void Player(Client c, PacketReader pr) {
            int uid = pr.ReadInt();
            pr.ReadByte();
            string ign = pr.ReadMapleString();

            try {
                c.UidMap[uid] = ign;
                //c.UidMap.Add(uid, ign);
                c.WriteLog.Report("Added " + uid + "(" + ign + ")");
            } catch (Exception) {
                c.WriteLog.Report("Error adding uid to player list.");
            }
        }

        public static void RemovePlayer(Client c, PacketReader pr) {
            int uid = pr.ReadInt();

            try {
                c.UidMap.Remove(uid);
                c.WriteLog.Report("Removed " + uid);
            } catch (Exception) {
                c.WriteLog.Report("Error removing uid from player list.");
            }
        }

        public static void MapLoad(Client c, PacketReader pr){
            pr.Skip(18); //[02 00 01 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00]
            int CH = pr.ReadInt(); //CH Connected To
            pr.Skip(10); // [00 00 00 00 00 01 00 00 00 00]
            pr.Skip(8); //Unknown 8 Bytes that change
            pr.Skip(3); // [01 00 00]
            pr.Skip(12);// Unknown 12 bytes something to do with connection
            pr.Skip(8); // [FF FF FF FF FF FF FF FF]
            pr.Skip(13); // [00 F1 FF FF FF F1 FF FF FF F1 FF FF FF] Where F1 Changes to random Fx Value
            pr.Skip(6); // [00 00 00 00 00 00]
            int UID = pr.ReadInt();

            /* Character Stats */ //EXACT SAME as charlist char stats but skip 5 instead of 8 after uid, why?
            pr.Skip(4); //UID Repeated
            pr.Skip(5); //[00 02 00 00 00]

            string IGN = pr.ReadString(13).TrimEnd('\0');
            c.WriteLog.Report("Ign is "+IGN);
            pr.Skip(13); //[Gender (1)] [Skin (1)] [Face (4)] [Hair (4)] [FF 00 00] [Level (1)] [Job (2)]
            int LEVEL = pr.ReadByte();
            int JOB = pr.ReadShort();
            //[str (2)] [dex (2)] [int (2)] [luk (2)] [hp (4)] [maxhp (4)] [mp (4)] [maxmp (4)] [Unused AP (2)]
            pr.Skip(8);
            int HP = pr.ReadInt();// Current HP
            pr.Skip(4);//Max HP
            int MP = pr.ReadInt(); //Current MP
            pr.Skip(4);//Max MP
            int UnUsedAP = pr.ReadShort();

            int temp = pr.ReadByte(); // Separated SP
            if (temp > 4)
                temp = pr.ReadByte();
            for (int j = 0; j < temp; ++j)
                pr.Skip(5);

            // pr.Skip(24); // [Exp (8)] [Fame (4)] [GachExp (4)] [?? (4)] [MapId (4)]
            long EXP = pr.ReadLong();
            int FAME = pr.ReadInt();
            pr.Skip(8);
            long MAP = pr.ReadInt();


            pr.Skip(7); // [SpawnPoint (1)] 00 00 00 00 [SubJob (2)] [(Demon, Xenon, Beast Tamer) ? FaceMark (4)] 
            if ((JOB >= 3100 && JOB <= 3122) || (JOB >= 3600 && JOB <= 3612) || JOB == 3002 || JOB == 3001) { // Demon/Xenon
                pr.Skip(4);
            } else if (JOB >= 11200 && JOB <= 11212) { // Beast Tamer
                pr.Skip(4);
            }

            pr.Skip(5);     // [Fatigue (1)] [Date (4)]
            pr.Skip(24);    // [Ambition (4)] [Insight (4)] [Willpower (4)] [Dilligence (4)] [Empathy (4)] [Charm (4)]
            pr.Skip(21);    // [Zeros (13)] [00 40 E0 FD] [3B 37 4F 01]
            pr.Skip(15);    // [PvP Exp (4)] [PvP Rank (1)] [Battle Pts (4)] [Byte (1)] [Byte (1)] [Int (4)]
            pr.Skip(1);     // part time job action of resting = 1, herbalism= 2, Mining = 3, general store = 4, Weapon and armor store = 5
            pr.Skip(13);    // [3B 37 4F 01] [00 40 E0 FD] [00 00 00 00] [00]
            pr.Skip(81);    // Character Cards 9 bytes each
            pr.Skip(8);     // [Last Login (8)]
            pr.Skip(1);     // 00

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

            /* Equipped Items & Equipped CS Items */
            /* Equip Inventory */

            /* So much random shit */

            /* Use Inventory */
            /* Set-up Inventory */
            /* Etc Inventory */
            /* Cash Inventory */

            c.MapId = MAP;
            c.Level = LEVEL;
            c.Mesos = MESOS;
            c.ch = CH +1;
            c.Name = IGN;

            c.UpdateName.Report(c.Name);
            c.UpdateMap.Report("" + c.MapId);
            c.UpdateLevel.Report("" + c.Level);
            c.UpdateChannel.Report("" + c.ch);
            c.UpdateMesos.Report("" + c.Mesos);
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
