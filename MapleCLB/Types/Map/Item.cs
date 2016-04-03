namespace MapleCLB.Types.Map {
    internal class Item : MapObject {
        internal uint Crc { get; set; }

        internal long Timestamp { get; set; }

        internal Item(uint id) : base(id) { }
    }
}
