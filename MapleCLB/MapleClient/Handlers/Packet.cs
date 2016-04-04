using System;
using System.Collections.Generic;
using System.Linq;
using MapleCLB.Packets;
using MapleCLB.Packets.Recv;
using MapleCLB.Packets.Recv.Connection;
using MapleCLB.Packets.Recv.Maps;
using MapleLib.Packet;
using SharedTools;

namespace MapleCLB.MapleClient.Handlers {
    internal class Packet : Handler<byte[]> {
        private readonly IProgress<byte[]> writeRecv;
        private readonly int maxRecv;
        /* Client Headers */
        private readonly Action<Client, PacketReader>[] headerMap;
        /* Script Headers */
        private readonly Action<Client, PacketReader>[] scriptHandler;
        private readonly List<Tuple<bool, Blocking<PacketReader>>>[] scriptWait;

        internal Packet(Client client, IProgress<byte[]> log) : base(client) {
            writeRecv = log;
            maxRecv = typeof(RecvOps).GetFields().Max(field =>
                ushort.Parse(field.GetRawConstantValue().ToString())) + 1;

            headerMap = new Action<Client, PacketReader>[maxRecv];
            scriptHandler = new Action<Client, PacketReader>[maxRecv];
            scriptWait = new List<Tuple<bool, Blocking<PacketReader>>>[maxRecv];

            Register(RecvOps.CHARLIST, Login.LoadCharlist);
            Register(RecvOps.SERVER_IP, PortIp.ServerIp);
            Register(RecvOps.CHANNEL_IP, PortIp.ChannelIp);
            Register(RecvOps.PING, Request.PingPong);

            Register(RecvOps.LOGIN_STATUS, Login.LoginStatus);
            Register(RecvOps.LOGIN_SECOND, Login.LoginSecond);

            Register(RecvOps.REMOVE_PLAYER, Map.RemovePlayer);
            Register(RecvOps.SPAWN_PLAYER, Map.SpawnPlayer);
            Register(RecvOps.CHAR_INFO, Load.CharInfo);
            Register(RecvOps.SEED, Load.Seed);

            Register(RecvOps.UPDATE_INVENTORY, Update.Inventory);
            Register(RecvOps.UPDATE_STATUS, Update.Status);

            //Temp (Is this even working?)
            Register(RecvOps.FINISH_LOAD, FMMovement.moveFM1);
        }

        internal override void Handle(object session, byte[] packet) {
            ushort header = (ushort) (packet[1] << 8 | packet[0]);
            writeRecv.Report(packet);

            if (header > maxRecv) { // Packet is not handled
                return;
            }
            if (headerMap[header] != null) {
                headerMap[header](client, new PacketReader(packet, 2));
            }
            if (scriptHandler[header] != null) {
                scriptHandler[header](client, new PacketReader(packet, 2));
            }
            if (scriptWait[header] != null) {
                // Ordering is necessary to prevent race condition
                List<Tuple<bool, Blocking<PacketReader>>> waitList = scriptWait[header];
                scriptWait[header] = null;
                waitList.ForEach(r => { r.Item2.Set(r.Item1 ? new PacketReader(packet, 2) : null); });
            }
        }

        internal void Register(ushort header, Action<Client, PacketReader> handler) {
            headerMap[header] = handler;
        }

        internal void Unregister(ushort header) {
            headerMap[header] = null;
        }

        internal bool RegisterHandler(ushort header, Action<Client, PacketReader> handler) {
            if (scriptHandler[header] != null) {
                return false;
            }
            scriptHandler[header] = handler;
            return true;
        }

        internal void UnregisterHandler(ushort header) {
            scriptHandler[header] = null;
        }

        internal void RegisterWait(ushort header, Blocking<PacketReader> reader, bool returnPacket) {
            if (scriptWait[header] == null) {
                scriptWait[header] = new List<Tuple<bool, Blocking<PacketReader>>>(2);
            }
            scriptWait[header].Add(new Tuple<bool, Blocking<PacketReader>>(returnPacket, reader));
        }
    }
}
