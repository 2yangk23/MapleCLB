using MapleLib.Packet;
using SharedTools;

namespace MapleCLB.Types.Items {
    public sealed class Pet : Item {
        public new static Pet Parse(PacketReader pr, byte temp) {
            var p = Item.Parse(pr, temp) as Pet;
            Precondition.NotNull(p, "Error casting item-type Pet.");

            pr.Skip(17);
            pr.Skip(8);
            pr.Skip(15);
            pr.Skip(4);
            pr.Skip(2);

            return p;
        }
    }
}
