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

                // pr.Skip(24); // [Exp (8)] [Fame (4)] [GachExp (4)] [?? (4)] [MapId (4)]
                chr.Exp = pr.ReadLong();
                chr.Fame = pr.ReadInt();
                pr.Skip(8);
                chr.Map = pr.ReadInt();

                pr.Skip(12); // [SpawnPoint (1)] 00 00 00 00 [SubJob (2)] [(Demon, Xenon, Beast Tamer) ? FaceMark (4)] [Fatigue (1)] [Date (4)]
                if ((chr.Job >= 3100 && chr.Job <= 3122) || (chr.Job >= 3600 && chr.Job <= 3612) || chr.Job == 3002 || chr.Job == 3001) { // Demon/Xenon
                    pr.Skip(4);
                } else if (chr.Job >= 11200 && chr.Job <= 11212) { // Beast Tamer
                    pr.Skip(4);
                }

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
            pr.Skip(4); //UID Repeated
            pr.Skip(5); //[00 02 00 00 00]
            //string IGN = pr.ReadHexString(12);
            string IGN = pr.ReadString(12).TrimEnd('\0');
            c.WriteLog.Report("Ign is "+IGN);
               // pr.Skip(12); //IGN + 00 for fillers up to 12
            pr.Skip(1); //[00]
            pr.Skip(13); //[Byte Gender][Byte Skin][Int Face][Int Hair][FF 00 00]
            int LEVEL = pr.ReadByte();
            int JOB = pr.ReadShort();
            pr.Skip(8); //[Short Str][Short Dex][Short Int][Short Luk]
            int HP = pr.ReadInt();// Current HP
            pr.Skip(4);//Max HP
            int MP = pr.ReadInt(); //Current MP
            pr.Skip(4);//Max MP
            int UnUsedAP = pr.ReadShort();
            pr.Skip(6);//Unknown Something about skills [01 01 07 00 00 00] //If Something breaks blame this
            long EXP = pr.ReadLong();
            pr.Skip(4); //Fame
            pr.Skip(8); //[Int Gach EXP][00 00 00 00]
            long MAP = pr.ReadInt();
            pr.Skip(1); //Character Spawn Point [01]
            pr.Skip(6);// [00 00 00 00][Short SubClass]
            if ((JOB >= 3100 && JOB <= 3122) || (JOB >= 3600 && JOB<= 3612) || JOB== 3002 || JOB == 3001)
            { // Demon/Xenon
                pr.Skip(4);
            }
            else if (JOB>= 11200 && JOB <= 11212)
            { // Beast Tamer
                pr.Skip(4);
            }
            pr.Skip(1); //Fatigue 
            pr.Skip(4);// Current Date
            pr.Skip(24); //Charisma, Insight, Will, Craft, Sense, Charm
            pr.Skip(13);// [00 00 00 00 00 00 00 00 00 00 00 00 00]
            pr.Skip(8); // [00 40 E0 FD 3B 37 4F 01] Constant
            pr.Skip(4);//[00 00 00 00]
            pr.Skip(1); //PVP Rank 0A
            pr.Skip(6);// [00 00 00 00 05 06] Constant 
            pr.Skip(5); //[00 00 00 00 00]
            pr.Skip(8); //[3B 37 4F 01 00 40 E0 FD] Constant
            pr.Skip(86); // 86 Bytes of Zeros wtf is this shit
            pr.Skip(8); //Last logged in day reveresed or something
            pr.Skip(1); // [00]
            pr.Skip(1);//BL Size
            if (pr.ReadByte() == 1) //Skips FairyBlessing
            {
                int temp = pr.ReadShort();
                pr.Skip(temp);
            }
            else
                pr.Skip(1);
            if (pr.ReadByte() == 1) //Skips EmpressBlessing
            {
                int temp = pr.ReadShort();
                pr.Skip(temp);
            }
            else
                pr.Skip(1);
            pr.Skip(1); //[00]

            long MESOS = pr.ReadLong();
            pr.Skip(12); //12 Zeros
            pr.Skip(4); // UID again
            pr.Skip(31); // 31 Zeros 
            pr.Skip(5); //[#Equip Slots][#Use Slots][#Set Up Slots][#ETC Slots][#Cash Slots]
            pr.Skip(8); //Some Weird Long time stamp
            pr.Skip(1);// [00]
            //Equips Start loading here

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

        public static void Mushrooms(Client c, PacketReader pr){
            int uid = pr.ReadInt();
            pr.Skip(4);
            short x = pr.ReadShort();
            short y = pr.ReadShort();
            short pid = pr.ReadShort();
            string ign = pr.ReadMapleString();

            int FM1CRC = 0x28C27A2A;

            //IGN -> UID 
            try
            {
                //c.IgnUid.Add(ign,uid);
                c.IgnUid[ign] = uid;
            } catch (Exception) {
                c.WriteLog.Report("Error loading mushrooms");
            }
            c.WriteLog.Report("Added : "+ ign +" to UID : "+uid +" @ "+x +" " +y);
            try
            {
            c.UidMovementPacket[uid] = HexEncoding.ToHexString(Movement.Teleport(FM1CRC, x, y, pid));
            // c.UidMovementPacket.Add(uid, HexEncoding.ToHexString(Movement.Teleport(FM1CRC, x, y, pid)));
            } catch (Exception) {
                c.WriteLog.Report("Error adding UID to Movement Packet");
            }
                    
        }
    }
}
