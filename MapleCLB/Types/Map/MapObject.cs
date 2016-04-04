namespace MapleCLB.Types.Map {
    internal abstract class MapObject {
        internal readonly int Id;
        internal Position Position { get; set; }

        protected MapObject(int id) {
            Id = id;
        }

        public override int GetHashCode() {
            return Id;
        }
    }
}
