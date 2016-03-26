using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using MapleCLB.Packets;
using MapleCLB.Packets.Recv;
using MapleCLB.Packets.Recv.Connection;
using MapleCLB.Packets.Recv.Map;
using MapleCLB.Tools;
using MapleLib.Packet;

namespace MapleCLB.MapleClient.Handlers {
    internal class Packet : Handler<byte[]> {
        /* Client Headers */
        private readonly Dictionary<ushort, EventHandler<PacketReader>> headerMap;
        /* Script Headers */
        private readonly ConcurrentDictionary<ushort, IProgress<PacketReader>> scriptHandler;
        private readonly ConcurrentDictionary<ushort, List<AutoResetEvent>> scriptWait;
        private readonly ConcurrentDictionary<ushort, List<Blocking<PacketReader>>> scriptWait2;

        internal Packet(Client client) : base(client) {
            headerMap = new Dictionary<ushort, EventHandler<PacketReader>>();
            scriptWait = new ConcurrentDictionary<ushort, List<AutoResetEvent>>();
            scriptHandler = new ConcurrentDictionary<ushort, IProgress<PacketReader>>();
            scriptWait2 = new ConcurrentDictionary<ushort, List<Blocking<PacketReader>>>();

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
            ushort header = (ushort) (packet[1] << 8 | packet[0]);
            Client.WriteRecv.Report(packet);

            if (headerMap.ContainsKey(header)) {
                headerMap[header](Client, new PacketReader(packet, 2));
            }
            if (scriptHandler.ContainsKey(header)) {
                scriptHandler[header].Report(new PacketReader(packet, 2));
            }
            if (scriptWait.ContainsKey(header)) {
                // Ordering is necessary to prevent race condition
                List<AutoResetEvent> waitList;
                scriptWait.TryRemove(header, out waitList);
                waitList.ForEach(e => e.Set());
            }
            if (scriptWait2.ContainsKey(header)) {
                // Ordering is necessary to prevent race condition
                List<Blocking<PacketReader>> waitList;
                scriptWait2.TryRemove(header, out waitList);
                waitList.ForEach(r => r.Set(new PacketReader(packet, 2)));
            }
        }

        internal void Register(ushort header, EventHandler<PacketReader> handler) {
            headerMap[header] = handler;
        }

        internal void Unregister(ushort header) {
            headerMap.Remove(header);
        }

        internal bool RegisterHandler(ushort header, IProgress<PacketReader> progress) {
            if (scriptHandler.ContainsKey(header)) {
                return false;
            }
            scriptHandler[header] = progress;
            return true;
        }

        internal void UnregisterHandler(ushort header) {
            IProgress<PacketReader> trash;
            scriptHandler.TryRemove(header, out trash);
        }

        internal void RegisterWait(ushort header, AutoResetEvent handle) {
            if (!scriptWait.ContainsKey(header)) {
                scriptWait[header] = new List<AutoResetEvent>(2);
            }
            scriptWait[header].Add(handle);
        }

        internal void RegisterWait(ushort header, Blocking<PacketReader> reader) {
            if (!scriptWait2.ContainsKey(header)) {
                scriptWait2[header] = new List<Blocking<PacketReader>>(2);
            }
            scriptWait2[header].Add(reader);
        }
    }
}
