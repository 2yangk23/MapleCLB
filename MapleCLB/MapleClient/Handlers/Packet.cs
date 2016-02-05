using System;
using System.Collections.Generic;
using System.Diagnostics;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Function.Connection;
using MapleCLB.Packets.Function.MapInfo;

namespace MapleCLB.MapleClient.Handlers {
    internal class Packet : Handler<byte[]> {
        private readonly Dictionary<short, EventHandler<PacketReader>> HeaderMap;

        internal Packet(Client client) : base(client) {
            HeaderMap = new Dictionary<short, EventHandler<PacketReader>>();

            //Register(RecvOps.LOGIN_STATUS, Login.LoginSecond);
            Register(RecvOps.CHARLIST, Login.SelectCharacter);
            Register(RecvOps.SERVER_IP, PortIp.ServerIp);
            Register(RecvOps.CHANNEL_IP, PortIp.ChannelIp);
            Register(RecvOps.PING, Request.PingPong);

            Register(RecvOps.LOGIN_STATUS, Login.LoginStatus);
            Register(RecvOps.LOGIN_SECOND, Login.LoginSecond);

            Register(RecvOps.SPAWN_PLAYER, Player.SpawnPlayer);
            Register(RecvOps.REMOVE_PLAYER, Player.RemovePlayer);

            Register(RecvOps.LOAD_MUSHY,  Mushrooms.loadMushrooms);
            Register(RecvOps.MAP_LOAD, MapCheck.mapCheck);
            Register(RecvOps.FINISH_LOAD, FMMovement.moveFM1);

        }

        internal override void Handle(object session, byte[] packet)
        {
            Debug.WriteLine(HexEncoding.ToHexString(packet));

            var pr = new PacketReader(packet);
            short header = pr.ReadShort();
            if (HeaderMap.ContainsKey(header))
            {
                HeaderMap[header](Client, pr);
            }
        }

        internal void Register(short header, EventHandler<PacketReader> handler) {
            HeaderMap.Add(header, handler);
        }
    }
}
