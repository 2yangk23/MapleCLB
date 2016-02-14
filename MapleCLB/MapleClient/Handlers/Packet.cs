using System;
using System.Collections.Generic;
using System.Threading;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Recv.Connection;
using MapleCLB.Packets.Recv.Map;

namespace MapleCLB.MapleClient.Handlers {
    internal class Packet : Handler<byte[]> {
        /* Client Headers */
        private readonly Dictionary<short, EventHandler<PacketReader>> HeaderMap;
        /* Script Headers */
        private readonly Dictionary<short, List<IProgress<PacketReader>>> ScriptHandler;
        private readonly Dictionary<short, List<EventWaitHandle>> ScriptWait;

        internal Packet(Client client) : base(client) {
            HeaderMap = new Dictionary<short, EventHandler<PacketReader>>();
            ScriptWait = new Dictionary<short, List<EventWaitHandle>>();
            ScriptHandler = new Dictionary<short, List<IProgress<PacketReader>>>();

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

            short header = (short) (packet[1] << 8 | packet[0]);
            if (HeaderMap.ContainsKey(header)) {
                HeaderMap[header](Client, new PacketReader(packet, 2));
            }
            if (ScriptHandler.ContainsKey(header)) {
                ScriptHandler[header].ForEach(p => p.Report(new PacketReader(packet, 2)));
            }
            if (ScriptWait.ContainsKey(header)) {
                ScriptWait[header].ForEach(e => e.Set());
                ScriptWait.Remove(header);
            }
        }

        internal void Register(short header, EventHandler<PacketReader> handler) {
            HeaderMap[header] = handler;
        }

        internal void Unregister(short header) {
            HeaderMap.Remove(header);
        }

        internal void RegisterHandler(short header, IProgress<PacketReader> progress) {
            if (!ScriptHandler.ContainsKey(header)) {
                ScriptHandler[header] = new List<IProgress<PacketReader>>(2);
            }
            ScriptHandler[header].Add(progress);
        }

        // Not working
        internal void UnregisterHandler(short header) {
            //TODO: How do you know which script's handler to remove?
            throw new NotImplementedException();
        }

        internal void RegisterWait(short header, EventWaitHandle handle) {
            if (!ScriptWait.ContainsKey(header)) {
                ScriptWait[header] = new List<EventWaitHandle>(2);
            }
            ScriptWait[header].Add(handle);
        }
    }
}
