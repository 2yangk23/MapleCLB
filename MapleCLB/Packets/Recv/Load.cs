using System;
using System.Collections.Generic;
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
                // charList.Add(chr);
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
                Program.WriteLog("Added " + uid + "(" + ign + ")");
            } catch (Exception) {
                Program.WriteLog("Error adding uid to player list.");
            }
        }

        public static void RemovePlayer(Client c, PacketReader pr) {
            int uid = pr.ReadInt();

            try {
                c.UidMap.Remove(uid);
                Program.WriteLog("Removed " + uid);
            } catch (Exception) {
                Program.WriteLog("Error removing uid from player list.");
            }
        }

        public static void mapID(Client c, PacketReader pr){
            if(c.shouldCC == true && c.doWhat == 1)
            {
                pr.Skip(176);
                long mapID = pr.ReadInt();
                Program.WriteLog("In Map: " + mapID);
                if (mapID == 910000001)
                {
                    c.shouldCC = false;
                    c.SendPacket(General.ChangeChannel(0x00));
                }
                else
                {
                    Program.WriteLog("Not In FM Room 1");
                    c.shouldCC = false;
                    c.Session.Disconnect();
                }
            }
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
                Program.WriteLog("Error loading mushrooms");
            }
            Program.WriteLog("Added : "+ ign +" to UID : "+uid +" @ "+x +" " +y);
            try
            {
             c.UidMovementPacket.Add(uid, HexEncoding.ToHexString(Movement.Teleport(FM1CRC, x, y, pid)));
            } catch (Exception) {
                Program.WriteLog("Error adding UID to Movement Packet");
            }
                    
        }
    }
}
