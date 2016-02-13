using System;
using System.Collections.Generic;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Recv.Connection;
using MapleCLB.Packets.Recv.Map;

namespace MapleCLB.MapleClient.Handlers {
    internal class Packet : Handler<byte[]> {
        private readonly Dictionary<short, EventHandler<PacketReader>> HeaderMap;

        internal Packet(Client client) : base(client) {
            HeaderMap = new Dictionary<short, EventHandler<PacketReader>>();

            Register(RecvOps.CHARLIST, Login.SelectCharacter);
            Register(RecvOps.SERVER_IP, PortIp.ServerIp);
            Register(RecvOps.CHANNEL_IP, PortIp.ChannelIp);
            Register(RecvOps.PING, Request.PingPong);

            Register(RecvOps.LOGIN_STATUS, Login.LoginStatus);
            Register(RecvOps.LOGIN_SECOND, Login.LoginSecond);

            Register(RecvOps.SPAWN_PLAYER, Player.SpawnPlayer);
            Register(RecvOps.REMOVE_PLAYER, Player.RemovePlayer);

            Register(RecvOps.LOAD_MUSHY,  Shop.SpawnMushy);
            Register(RecvOps.CHAR_INFO, MapCheck.Check);
            Register(RecvOps.FINISH_LOAD, FMMovement.moveFM1);
        }

        internal override void Handle(object session, byte[] packet) {
            Client.WriteRecv.Report(packet);

            var pr = new PacketReader(packet);
            short header = pr.ReadShort();
            if (HeaderMap.ContainsKey(header)) {
                HeaderMap[header](Client, pr);
            }
        }

        internal void Register(short header, EventHandler<PacketReader> handler) {
            HeaderMap.Add(header, handler);
        }
    }
}
