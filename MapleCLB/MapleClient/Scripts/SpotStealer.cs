using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.Types;
using MapleLib.Packet;
using ScriptLib;

namespace MapleCLB.MapleClient.Scripts {
    public enum StealMode { SNIPER, GREEDY, SERVER_CHECK }

    //TODO: Allow support of all shop ids & slots?
    internal sealed class SpotStealer : ComplexScript<Client> {
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

        protected override void Execute() {
            if (Client.State != ClientState.GAME) {
                WaitRecv(RecvOps.FINISH_LOAD);
            }
            if (Mode == StealMode.SERVER_CHECK) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(Movement.Teleport(Client.PortalCount, SendOps.FM1_CRC, X, Y, Fh));
                OpenShop();
            }
            WaitRecv(RecvOps.UPDATE_SHOP);
            SendPacket(Trade.PutItem(2, (byte)(Type == ShopType.PERMIT ? 0x41 : 0x21), 1, 1, 1, 999999999));
            WaitRecv(RecvOps.UPDATE_SHOP);
            SendPacket(Trade.OpenShop2());
            SendPacket(Trade.OpenShop());

            Client.hasFMShop = true;
            Client.WriteLog("Shop Open For Business!");
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
        private void OpenMushy(PacketReader r) {
            if (r.ReadByte() == 7) {
                SendPacket(Trade.CreateShop(ShopType.MUSHY, ShopName, 1, 5030000));
            }
        }

        private void PermitClosed(PacketReader r) {
            int uid = r.ReadInt();
            if (r.ReadByte() == 5) { // Permit closed
                StealSpot(uid << 4);
            }
        }

        private void MushyClosed(PacketReader r) {
            int uid = r.ReadInt();
            StealSpot(uid);
        }

        private void SpawnPermit(PacketReader r) {
            int uid = r.ReadInt() << 4; // Shift uid so that it won't conflict with uid for mushys
            r.ReadByte(); // [Level (1)]
            string ign = r.ReadMapleString(); // Name
            r.ReadMapleString(); // Ultimate Explorer's Parent
            r.ReadMapleString(); // Guild
            r.Skip(6); // [LogoBG (2)] [ColoUr (1)] [Logo (2)] [ColoUr (1)]

            r.Skip(13); // 00 [40 00 00 00] [01 00 00 00] [00 00 00 00]

            // Sub
            r.Skip(64); // Mostly Zeros 
            r.Skip(4); // -1
            r.Skip(64); // TODO: Figure this part out later but until then cheat
            r.Next(01); // Time Encoding
            r.Skip(12); // ?? ?? ?? ?? [Zero (8)]
            r.Skip(30); // [TimeEncode (5)] [00 00] [00 00 00 00] [00 00 00 00] [TimeEncode (5)] [00 00] [00 00 00 00] [00 00 00 00]
            r.Skip(13); // [TimeEncode (5)] [00 00 00 00] [00 00 00 00]
            r.Skip(20); // [TimeEncode (5)] 00 [DE AC 77 DA] [00 00 00 00] [00 00 00 00] [00 00]
            r.Skip(28); // [TimeEncode (5)] [Zero (16)] [TimeEncode (5)] 00 00

            short job = r.ReadShort(); // JOB 
            r.Skip(6); // [SubJob (2)] [? (4)]

            Mapler.SkipAppearance(r, job);

            r.Skip(4 * 14 + 14); // [00 00 00 00] * 14 [FF FF] [00 00 00 00 00 00 00 00 00 00 00 00]

            short x = r.ReadShort();
            short y = r.ReadShort();
            r.Skip(1); // Type or stance?
            short fh = r.ReadShort();
            r.Skip(17); // Unknown shit
            if (r.ReadByte() != 0) {
                UidMap[uid] = ign;
                UidMovementPacket[uid] = Movement.Teleport(Client.PortalCount, SendOps.FM1_CRC, x, y, fh);

                Client.WriteLog("Added Permit : " + ign + " to shifted UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
            }
        }

        private void SpawnMushy(PacketReader r) {
            int uid = r.ReadInt();
            r.Skip(4);
            short x = r.ReadShort();
            short y = r.ReadShort();
            short fh = r.ReadShort();
            string ign = r.ReadMapleString();

            UidMap[uid] = ign;
            UidMovementPacket[uid] = Movement.Teleport(Client.PortalCount, SendOps.FM1_CRC, x, y, fh);

            Client.WriteLog("Added Mushy : " + ign + " to UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
        }
    }
}
