namespace MapleCLB.Types.Map {
    internal class Player : MapObject {
        internal string Ign { get; set; }
        internal short Fh { get; set; }

        internal Player(uint id) : base(id) { }
    }
}
