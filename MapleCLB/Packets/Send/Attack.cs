using MapleCLB.Types.Map;
using MapleLib.Packet;

namespace MapleCLB.Packets.Send {
    internal static class Attack {
        internal static byte[] Reactor(Reactor reactor) {
            var pw = new PacketWriter(SendOps.HIT_REACTOR);
            pw.WriteInt(reactor.Id);
            pw.WriteByte(0x03); // Animation
            pw.WriteBytes(0x00, 0x00, 0x00, 0x06, 0x01, 0x00, 0x00, 0x00, 0x00);

            return pw.ToArray();
        }
    }
}
