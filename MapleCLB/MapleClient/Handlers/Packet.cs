using System;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<short, IProgress<PacketReader>> ScriptHandler;
        private readonly ConcurrentDictionary<short, List<AutoResetEvent>> ScriptWait;

        internal Packet(Client client) : base(client) {
            HeaderMap = new Dictionary<short, EventHandler<PacketReader>>();
            ScriptWait = new ConcurrentDictionary<short, List<AutoResetEvent>>();
            ScriptHandler = new ConcurrentDictionary<short, IProgress<PacketReader>>();

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
            short header = (short)(packet[1] << 8 | packet[0]);
            Client.WriteRecv.Report(packet);

            if (HeaderMap.ContainsKey(header)) {
                HeaderMap[header](Client, new PacketReader(packet, 2));
            }
            if (ScriptHandler.ContainsKey(header)) {
                ScriptHandler[header].Report(new PacketReader(packet, 2));
            }
            if (ScriptWait.ContainsKey(header)) {
                // Ordering is necessary to prevent race condition
                List<AutoResetEvent> waitList;
                ScriptWait.TryRemove(header, out waitList);
                waitList.ForEach(e => e.Set());
            }
        }

        internal void Register(short header, EventHandler<PacketReader> handler) {
            HeaderMap[header] = handler;
        }

        internal void Unregister(short header) {
            HeaderMap.Remove(header);
        }

        internal bool RegisterHandler(short header, IProgress<PacketReader> progress) {
            if (ScriptHandler.ContainsKey(header)) {
                return false;
            }
            ScriptHandler[header] = progress;
            return true;
        }

        internal void UnregisterHandler(short header) {
            IProgress<PacketReader> trash;
            ScriptHandler.TryRemove(header, out trash);
        }

        internal void RegisterWait(short header, AutoResetEvent handle) {
            if (!ScriptWait.ContainsKey(header)) {
                ScriptWait[header] = new List<AutoResetEvent>(2);
            }
            ScriptWait[header].Add(handle);
        }
    }
}
