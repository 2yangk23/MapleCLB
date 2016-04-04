using MapleCLB.MapleClient;
using MapleCLB.Packets.Send;
using MapleLib.Packet;

namespace MapleCLB.Packets.Recv.Connection {
    internal class Request {
        public static void PingPong(Client c, PacketReader r) {
            c?.SendPacket(General.Pong());
        }
    }
}
