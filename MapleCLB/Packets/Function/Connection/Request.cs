using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Send;

namespace MapleCLB.Packets.Function.Connection {
    class Request {
        public static void PingPong(object o, PacketReader r) {
            var c = o as Client;
            c.SendPacket(General.Pong());
        }



    }
}
