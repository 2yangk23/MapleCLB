using System;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Types {
    public sealed class Character {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public short Job { get; set; }

        public short Str { get; set; }
        public short Dex { get; set; }
        public short Int { get; set; }
        public short Luk { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }

        public int Ap { get; set; }
        public long Exp { get; set; }
        public int Fame { get; set; }
        public int Map { get; set; }

        public static Character Parse(PacketReader pr, int skip) {
            var c = new Character();

            // [uid (4)] [?? (5 or 8)] [Name (13)]
            c.Id = pr.ReadInt();
            pr.Skip(skip); // 8 in CHARLIST [?? ?? ?? ?? ?? ?? ?? ??], 5 in CHAR_INFO [00 02 00 00 00]
            c.Name = pr.ReadString(13).TrimEnd('\0');
            //[Gender (1)] [Skin (1)] [Face (4)] [Hair (4)] [FF 00 00] [Level (1)] [Job (2)]
            pr.Skip(13);
            c.Level = pr.ReadByte();
            c.Job = pr.ReadShort();
            //[str (2)] [dex (2)] [int (2)] [luk (2)] [hp (4)] [maxhp (4)] [mp (4)] [maxmp (4)] [Unused AP (2)]
            c.Str = pr.ReadShort();
            c.Dex = pr.ReadShort();
            c.Int = pr.ReadShort();
            c.Luk = pr.ReadShort();

            c.Hp = pr.ReadInt();
            c.MaxHp = pr.ReadInt();
            c.Mp = pr.ReadInt();
            c.MaxMp = pr.ReadInt();

            c.Ap = pr.ReadShort();

            byte temp = pr.ReadByte(); // Separated SP
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

            // [Exp (8)] [Fame (4)] [GachExp (4)] [?? (4)] [MapId (4)]
            c.Exp = pr.ReadLong();
            c.Fame = pr.ReadInt();
            pr.Skip(8);
            c.Map = pr.ReadInt();

            pr.Skip(7); // [SpawnPoint (1)] 00 00 00 00 [SubJob (2)] [(Demon, Xenon, Beast Tamer) ? FaceMark (4)]
            if ((c.Job >= 3100 && c.Job <= 3122) || (c.Job >= 3600 && c.Job <= 3612) || c.Job == 3002 || c.Job == 3001) { // Demon/Xenon
                pr.Skip(4);
            } else if (c.Job >= 11200 && c.Job <= 11212) { // Beast Tamer
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

            return c;
        }

        public void Print() {
            Console.WriteLine("Id: {0}, Name: {1}, Job: {2}, Level: {3}", Id, Name, Job, Level);
            Console.WriteLine("Str[{0}] Dex[{1}] Int[{2}] Luk[{3}], {4} / {5} Hp, {6} / {7} Mp", Str, Dex, Int, Luk, Hp, MaxHp, Mp, MaxMp);
            Console.WriteLine("Ap: {0}, Exp: {1}, Fame: {2}, Map: {3}", Ap, Exp, Fame, Map);
        }
    }
}
