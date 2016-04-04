using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Types;
using MapleCLB.Types.Map;
using MapleLib.Packet;

namespace MapleCLB.MapleClient.Scripts {
    public enum StealMode { SNIPER, GREEDY, SERVER_CHECK }

    //TODO: Allow support of all shop ids & slots?
    internal sealed class SpotStealer : UserScript {
        /* Info */
        public StealMode Mode = StealMode.GREEDY;
        public ShopType Type = ShopType.PERMIT;

        public string Target = "Hommos";
        public string ShopName = "Cheap :]";
        public short X, Y, Fh;

        public readonly IDictionary<int, string> UidMap = new ConcurrentDictionary<int, string>();
        public readonly IDictionary<int, byte[]> UidMovementPacket = new ConcurrentDictionary<int, byte[]>();

        public SpotStealer(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.CLOSE_PERMIT, PermitClosed);
            RegisterRecv(RecvOps.CLOSE_MUSHY, MushyClosed);
            RegisterRecv(RecvOps.SPAWN_PLAYER, SpawnPermit);
            RegisterRecv(RecvOps.LOAD_MUSHY, SpawnMushy);
            RegisterRecv(RecvOps.BLUE_POP, OpenMushy);
        }

        protected override void Execute(CancellationToken token) {
            if (client.State != ClientState.GAME) {
                WaitRecv(RecvOps.FINISH_LOAD);
            }
            if (Mode == StealMode.SERVER_CHECK) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(Movement.Teleport(client.PortalCount, GameConsts.FM1_CRC, new Position(X, Y), Fh));
                OpenShop();
            }
            WaitRecv(RecvOps.UPDATE_SHOP);
            SendPacket(Trade.PutItem(2, (byte)(Type == ShopType.PERMIT ? 0x41 : 0x21), 1, 1, 1, 999999999));
            WaitRecv(RecvOps.UPDATE_SHOP);
            SendPacket(Trade.OpenShop2());
            SendPacket(Trade.OpenShop());

            client.hasFMShop = true;
            client.WriteLog("Shop Open For Business!");
        }

        private void StealSpot(int id) {
            switch (Mode) {
                case StealMode.SNIPER:
                    if (Target.Equals(UidMap[id], StringComparison.OrdinalIgnoreCase)) { // If target dropping spot
                        goto case StealMode.GREEDY;
                    }
                    break;
                case StealMode.GREEDY:
                    // Teleport to spot and open shop
                    SendPacket(Movement.beforeTeleport());
                    SendPacket(UidMovementPacket[id]);
                    OpenShop();
                    break;
            }

            UidMap.Remove(id);
            UidMovementPacket.Remove(id);
        }

        private void OpenShop() {
            switch (Type) {
                case ShopType.PERMIT:
                    SendPacket(Trade.CreateShop(ShopType.PERMIT, ShopName, 1, 5140000));
                    break;
                case ShopType.MUSHY:
                    SendPacket(Trade.UseMushy(1));
                    break;
            }
        }

        // Handlers
        private void OpenMushy(Client c, PacketReader r) {
            if (r.ReadByte() == 7) {
                SendPacket(Trade.CreateShop(ShopType.MUSHY, ShopName, 1, 5030000));
            }
        }

        private void PermitClosed(Client c, PacketReader r) {
            int uid = r.ReadInt();
            if (r.ReadByte() == 5) { // Permit closed
                StealSpot(uid << 4);
            }
        }

        private void MushyClosed(Client c, PacketReader r) {
            int uid = r.ReadInt();
            StealSpot(uid);
        }

        private void SpawnPermit(Client c, PacketReader r) {
            var p = r.ReadPlayer();
            int uid = p.Id << 4;
            if (p.HasPermit) {
                UidMap[uid] = p.Ign;
                UidMovementPacket[uid] = Movement.Teleport(client.PortalCount, GameConsts.FM1_CRC, p.Position, p.Fh);

                client.WriteLog("Added Permit : " + p.Ign + " to shifted UID : " + uid + "@ " + p.Position + ", fh: " + p.Fh);
            }
        }

        private void SpawnMushy(Client c, PacketReader r) {
            int uid = r.ReadInt();
            r.Skip(4);
            var pos = r.ReadPosition();
            short fh = r.ReadShort();
            string ign = r.ReadMapleString();

            UidMap[uid] = ign;
            UidMovementPacket[uid] = Movement.Teleport(client.PortalCount, GameConsts.FM1_CRC, pos, fh);

            client.WriteLog("Added Mushy : " + ign + " to UID : " + uid + "@ " + pos + ", fh: " + fh);
        }
    }
}
