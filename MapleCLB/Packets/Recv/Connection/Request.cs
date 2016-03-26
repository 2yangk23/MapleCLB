using MapleCLB.MapleClient;
using MapleLib.Packet;
using MapleCLB.Packets.Send;

namespace MapleCLB.Packets.Recv.Connection {
    internal class Request {
        public static void PingPong(object o, PacketReader r) {
            var c = o as Client;
            c.SendPacket(General.Pong());
        }
    }
}
