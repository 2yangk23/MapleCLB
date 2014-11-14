using MapleCLB.MaplePacketLib;
using MapleCLB.User;
using MaplePacketLib;

namespace MapleCLB.Packets.Function
{
    class ServerIP : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            r.ReadShort();
            byte[] serverIP = r.ReadBytes(4);
            short Port = r.ReadShort();
            string IP = serverIP[0] + "." + serverIP[1] + "." + serverIP[2] + "." + serverIP[3];
            c.Reconnect(IP, Port);
        }
    }

    class ChannelIP : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            r.ReadByte();
            byte[] channelIP = r.ReadBytes(4);
            short Port = r.ReadShort();
            string IP = channelIP[0] + "." + channelIP[1] + "." + channelIP[2] + "." + channelIP[3];
            c.Reconnect(IP, Port);
        }
    }
}
