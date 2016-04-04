using MapleLib.Packet;

namespace MapleCLB.Types.Map {
    internal class Reactor : MapObject {
        internal byte Hits { get; set; }

        internal Reactor(int id) : base(id) { }
    }

    internal static class ReactorPacketExtensions {
        // SPAWN  - [ObjectId (4)] [ReactorId (4)] [Hits (1)] [X (2)] [Y (2)] 00 00 00
        // UPDATE - [ObjectId (4)] [Hits (1)] [X (2)] [Y (2)] [Animation? (4)] [Attacker Uid (4)]
        internal static Reactor ReadReactor(this PacketReader pr) {
            var r = new Reactor(pr.ReadInt());
            if (pr.Available == 12) {
                pr.ReadInt();
            }
            r.Hits = pr.ReadByte();
            r.Position = pr.ReadPosition();

            return r;
        }
    }
}
