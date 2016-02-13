using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.Packets.Recv.Connection {
    class PortIp {
        public static void ServerIp(object o, PacketReader r) {
            var c = o as Client;
            r.ReadShort();
            byte[] serverIp = r.ReadBytes(4);
            short port = r.ReadShort();
            string ip = serverIp[0] + "." + serverIp[1] + "." + serverIp[2] + "." + serverIp[3];
            c.Reconnect(ip, port);
        }

        public static void ChannelIp(object o, PacketReader r) {
            var c = o as Client;
            r.ReadByte();
            byte[] channelIp = r.ReadBytes(4);
            short port = r.ReadShort();
            string ip = channelIp[0] + "." + channelIp[1] + "." + channelIp[2] + "." + channelIp[3];
            c.Reconnect(ip, port);
        }
    }
}
