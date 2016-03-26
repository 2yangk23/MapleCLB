using System;
using MapleLib.Packet;

namespace MapleCLB.Types {
    public sealed class Mapler {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public byte Level { get; private set; }
        public short Job { get; private set; }

        public short Str { get; private set; }
        public short Dex { get; private set; }
        public short Int { get; private set; }
        public short Luk { get; private set; }
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }
        public int Mp { get; private set; }
        public int MaxMp { get; private set; }

        public int Ap { get; private set; }
        public long Exp { get; private set; }
        public int Fame { get; private set; }
        public int Map { get; set; }

        public long Meso { get; set; }

        public bool IsDemon => Job == 3001 || Job / 100 == 31;
        public bool IsXenon => Job == 3002 || Job / 100 == 36;
        public bool IsBeastTamer => Job == 11000 || Job / 100 == 112;

        public static Mapler Parse(PacketReader pr) {
            var m = new Mapler();

            // [uid (4)] [uid (4)] [02 00 00 00] [Name (13)]
            m.Id = pr.ReadInt();
            pr.Skip(8);
            m.Name = pr.ReadString(13).TrimEnd('\0');
            //[Gender (1)] [Skin (1)] [Face (4)] [Hair (4)] [FF 00 00] [Level (1)] [Job (2)]
            pr.Skip(13);
            m.Level = pr.ReadByte();
            m.Job = pr.ReadShort();
            //[str (2)] [dex (2)] [int (2)] [luk (2)] [hp (4)] [maxhp (4)] [mp (4)] [maxmp (4)] [Unused AP (2)]
            m.Str = pr.ReadShort();
            m.Dex = pr.ReadShort();
            m.Int = pr.ReadShort();
            m.Luk = pr.ReadShort();

            m.Hp = pr.ReadInt();
            m.MaxHp = pr.ReadInt();
            m.Mp = pr.ReadInt();
            m.MaxMp = pr.ReadInt();

            m.Ap = pr.ReadShort();

            byte temp = pr.ReadByte(); // Separated SP
            if (temp > 4) {
                temp = pr.ReadByte();
            }
            for (int j = 0; j < temp; ++j) {
                pr.Skip(5);
            }

            /* Correct way to do separated sp
            pw.WriteShort(chr.AP);
            if (!chr.IsSeparatedSpJob)
                pw.WriteShort((short)chr.SpTable[0]);
            else
                AddSeparatedSP(chr, pw);
             */

            // [Exp (8)] [Fame (4)] [GachExp (4)] [?? (4)] [MapId (4)]
            m.Exp = pr.ReadLong();
            m.Fame = pr.ReadInt();
            pr.Skip(8);
            m.Map = pr.ReadInt();

            pr.Skip(7); // [SpawnPoint (1)] 00 00 00 00 [SubJob (2)] [(Demon, Xenon, Beast Tamer) ? FaceMark (4)]
            if (m.IsDemon || m.IsXenon || m.IsBeastTamer) { // Demon/Xenon/Beast Tamer
                pr.Skip(4);
            }

            /* [Fatigue (1)] [Date (4)]
             * [Ambition (4)] [Insight (4)] [Willpower (4)] [Dilligence (4)] [Empathy (4)] [Charm (4)]
             * [Zeros (13)] [00 40 E0 FD] [3B 37 4F 01]
             * [PvP Exp (4)] [PvP Rank (1)] [Battle Pts (4)] [Byte (1)] [Byte (1)] [Int (4)]
             * part time job action of resting = 1, herbalism= 2, Mining = 3, general store = 4, Weapon and armor store = 5
             * [3B 37 4F 01] [00 40 E0 FD] [00 00 00 00] [00]
             * Character Cards 9 bytes each
             * [Last Login (8)] 00
             */
            pr.Skip(5 + 24 + 21 + 15 + 1 + 13 + 81 + 9);

            return m;
        }

        public static void SkipAppearance(PacketReader pr, short job) {
            var m = new Mapler { Job = job };
            pr.Skip(15); // [Gender (1)] [Skin (1)] [Face (4)] [Job (2)] [SubJob (2)] [Mega (1)] [Hair (4)]
            for (int j = 0; j < 3; ++j) { // Skips the Equipment
                pr.Next(0xFF);
            }
            pr.Skip(4); // [00 00 00 00]

            pr.Skip(21); // [Weapon (4)] [Shield (4)] [Mercedes Ears (1)] [Zeros (12)]
            if (m.IsDemon || m.IsXenon) { // Demon/Xenon
                pr.Skip(4); // [FaceMark (4)]
            } else if (m.IsBeastTamer) { // Beast Tamer
                pr.Skip(14); // [FaceMark (4)] [Ears (1)] [EarType (4)] [Tail (1)] [TailType (4)]
            }
            pr.Skip(3); // ?? ?? ??
        }

        public void Print() {
            Console.WriteLine("Id: {0}, Name: {1}, Job: {2}, Level: {3}", Id, Name, Job, Level);
            Console.WriteLine("Str[{0}] Dex[{1}] Int[{2}] Luk[{3}], {4} / {5} Hp, {6} / {7} Mp", 
                Str, Dex, Int, Luk, Hp, MaxHp, Mp, MaxMp);
            Console.WriteLine("Ap: {0}, Exp: {1}, Fame: {2}, Map: {3}", Ap, Exp, Fame, Map);
        }
    }
}
