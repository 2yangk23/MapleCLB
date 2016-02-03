using System;

namespace MapleCLB.Types {
    public sealed class Character {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Job { get; set; }
        public byte Level { get; set; }
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

        public void Print() {
            Console.WriteLine("Id: {0}, Name: {1}, Job: {2}, Level: {3}", Id, Name, Job, Level);
            Console.WriteLine("Str[{0}] Dex[{1}] Int[{2}] Luk[{3}], {4} / {5} Hp, {6} / {7} Mp", Str, Dex, Int, Luk, Hp, MaxHp, Mp, MaxMp);
            Console.WriteLine("Ap: {0}, Exp: {1}, Fame: {2}, Map: {3}", Ap, Exp, Fame, Map);
        }
    }
}
