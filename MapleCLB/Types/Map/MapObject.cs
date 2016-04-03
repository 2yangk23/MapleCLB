namespace MapleCLB.Types.Map {
    internal abstract class MapObject {
        internal readonly uint Id;
        internal Position Position { get; set; }

        protected MapObject(uint id) {
            Id = id;
            //X.
        }

        public override int GetHashCode() {
            return (int) Id;
        }
    }
}
