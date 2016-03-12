using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Recv;
using MapleCLB.Packets.Recv.Connection;
using MapleCLB.Packets.Recv.Map;
using MapleCLB.Tools;

namespace MapleCLB.MapleClient.Handlers {
    internal class Packet : Handler<byte[]> {
        /* Client Headers */
        private readonly Dictionary<ushort, EventHandler<PacketReader>> HeaderMap;
        /* Script Headers */
        private readonly ConcurrentDictionary<ushort, IProgress<PacketReader>> ScriptHandler;
        private readonly ConcurrentDictionary<ushort, List<AutoResetEvent>> ScriptWait;
        private readonly ConcurrentDictionary<ushort, List<Blocking<PacketReader>>> ScriptWait2;

        internal Packet(Client client) : base(client) {
            HeaderMap = new Dictionary<ushort, EventHandler<PacketReader>>();
            ScriptWait = new ConcurrentDictionary<ushort, List<AutoResetEvent>>();
            ScriptHandler = new ConcurrentDictionary<ushort, IProgress<PacketReader>>();
            ScriptWait2 = new ConcurrentDictionary<ushort, List<Blocking<PacketReader>>>();

            Register(RecvOps.CHARLIST, Login.SelectCharacter);
            Register(RecvOps.SERVER_IP, PortIp.ServerIp);
            Register(RecvOps.CHANNEL_IP, PortIp.ChannelIp);
            Register(RecvOps.PING, Request.PingPong);
            Register(RecvOps.SEED, Load.Seed);

            Register(RecvOps.LOGIN_STATUS, Login.LoginStatus);
            Register(RecvOps.LOGIN_SECOND, Login.LoginSecond);

            //To Do : Rename this to something else
            Register(RecvOps.CHAR_INFO, MapCheck.Check);

            //Temp
            Register(RecvOps.FINISH_LOAD, FMMovement.moveFM1);
        }

        internal override void Handle(object session, byte[] packet) {
            ushort header = (ushort)(packet[1] << 8 | packet[0]);
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
            if (ScriptWait2.ContainsKey(header)) {
                // Ordering is necessary to prevent race condition
                List<Blocking<PacketReader>> waitList;
                ScriptWait2.TryRemove(header, out waitList);
                waitList.ForEach(r => r.Set(new PacketReader(packet, 2)));
            }
        }

        internal void Register(ushort header, EventHandler<PacketReader> handler) {
            HeaderMap[header] = handler;
        }

        internal void Unregister(ushort header) {
            HeaderMap.Remove(header);
        }

        internal bool RegisterHandler(ushort header, IProgress<PacketReader> progress) {
            if (ScriptHandler.ContainsKey(header)) {
                return false;
            }
            ScriptHandler[header] = progress;
            return true;
        }

        internal void UnregisterHandler(ushort header) {
            IProgress<PacketReader> trash;
            ScriptHandler.TryRemove(header, out trash);
        }

        internal void RegisterWait(ushort header, AutoResetEvent handle) {
            if (!ScriptWait.ContainsKey(header)) {
                ScriptWait[header] = new List<AutoResetEvent>(2);
            }
            ScriptWait[header].Add(handle);
        }

        internal void RegisterWait(ushort header, Blocking<PacketReader> reader) {
            if (!ScriptWait2.ContainsKey(header)) {
                ScriptWait2[header] = new List<Blocking<PacketReader>>(2);
            }
            ScriptWait2[header].Add(reader);
        }
    }
}
