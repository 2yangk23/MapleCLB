using MaplePacketLib;
using MapleCLB.User;
using System;

namespace MapleCLB.Packets {
    class Load {
        //[Header (2)] 00 [Char count (1)] [UID (4)] [IGN (13)] ...
        public static void Character(Client c, PacketReader pr) {
            int uid;
            short job;
            byte temp;
            string ign;
            pr.Skip(13); //+12 bytes (v156)
            byte count = pr.ReadByte();
            for (byte i = 0; i < count; ++i) {
                uid = pr.ReadInt();
                ign = pr.ReadString(13);
                pr.Skip(11); //[Gender (1)] [Skin (1)] [Face (4)] [Hair (4)] [level (1)]
                pr.Skip(3); //Who the fuck knows (v157)
                job = pr.ReadShort(); //[Job (2)]
                pr.Skip(26); //[str (2)] [dex (2)] [int (2)] [luk (2)] [hp (4)] [maxhp (4)] [mp (4)] [maxmp (4)] [Unused AP (2)]

                temp = pr.ReadByte(); //some separated SP shit
                if (temp > 4)
                    temp = pr.ReadByte();
                for (int j = 0; j < temp; ++j)
                    pr.Skip(5);

                /*pr.Skip(24); //[exp (8)] [fame (4)] [?? (4)] [?? (4)] [map (4)]
                pr.Skip(90); //who knows
                pr.Skip(81); //Character Cards 9 bytes each
                pr.Skip(19); //[?? (4)] [Gender (1)] [Skin (1)] [Face (4)] [Job (2)] 00 00 00 [Hair (4)]*/
                pr.Skip(214);

                for (int j = 0; j < 3; ++j) //skips all the appearance stuff
                    pr.Next(0xFF);

                if (job >= 10100 && job <= 10112)//zero -guess-
                {
                    for (int j = 0; j < 6; ++j) {
                        pr.Next(0xFF); //I guess zero has 3 appearances?
                    }
                    pr.Skip(1); //extra 00 at the end
                } else {
                    temp = 24; //[wep (4)] [wep (4)] [wep (4)] [pet (12)]
                    temp += 2; //Idk what im doing (v157)
                    if (job != 0) {//Beginners dont have these 16 bytes, not sure why
                        temp += 16;
                    }
                    if ((job >= 3100 && job <= 3122) || (job >= 3600 && job <= 3612) || job == 3002 || job == 3001) {//demon/demon avenger/xenon char
                        temp += 4;
                    } else if (job >= 11200 && job <= 11212) {//beast tamer
                        temp += 14;
                    }

                    pr.Skip(temp + 4);
                }
                //System.Diagnostics.Debug.WriteLine("" + uid + " : " + job + " : " + ign + System.Environment.NewLine);
                c.charMap.Add(i, ign.TrimEnd(new char[] { '\0' }).ToLower(), uid);
            }
        }

        public static void Player(Client c, PacketReader pr) {
            int uid = pr.ReadInt();
            pr.ReadByte();
            string ign = pr.ReadMapleString();

            try {
                c.uidMap.Add(uid, ign);
                //Program.WriteLog("Added " + uid + "(" + ign + ")");
            } catch (Exception) {
                Program.WriteLog("Error adding uid to player list.");
            }
        }

        public static void RemovePlayer(Client c, PacketReader pr) {
            int uid = pr.ReadInt();

            try {
                c.uidMap.Remove(uid);
                //Program.WriteLog("Removed " + uid);
            } catch (Exception) {
                Program.WriteLog("Error removing uid from player list.");
            }
        }
    }
}
