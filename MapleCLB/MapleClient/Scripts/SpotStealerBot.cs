using MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using ScriptLib;

namespace MapleCLB.MapleClient.Scripts {
    internal sealed class SpotStealerBot : ComplexScript<Client> {
        private readonly PlayerLoader playerLoader;
        private const int FM1_CRC = 0x2A7AC228;

        public string Ign { get; set; }
        public string ShopName { get; set; }
        public string Fh { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public bool PermitCb { get; set; }
        public bool ScMode { get; set; }
        public bool TakeAnyCb { get; set; }    

        public SpotStealerBot(Client client) : base(client) {
            playerLoader = Requires<PlayerLoader>();
        }

        protected override void Init() {
            RegisterRecv(RecvOps.CLOSE_PERMIT, StealSpot);
            RegisterRecv(RecvOps.CLOSE_MUSHY, StealSpotMush); 
            RegisterRecv(RecvOps.BLUE_POP, OpenMushy);
        }

        //To Do: add custom items to permit
        protected override void Execute() {
            if (Client.State != ClientState.GAME) {
                WaitRecv(RecvOps.FINISH_LOAD);
            }
            if (!PermitCb)
                WaitRecv(RecvOps.TEMP); //This shit is guessed... You must wait if its a mushroom.
            if (ScMode){
                ServerCheckSteal();
                if(!PermitCb)
                    WaitRecv(RecvOps.BLUE_POP);
                WaitRecv(RecvOps.UPDATE_SHOP);
                SendPacket(Trade.PutItem(2, (byte) (PermitCb ? 0x41 : 0x21), 1, 1, 1, 999999999));
                WaitRecv(RecvOps.UPDATE_SHOP);
                if(!PermitCb)
                    SendPacket(Trade.OpenShop2());
                SendPacket(Trade.OpenShop());
                Client.hasFMShop = true;
                Client.WriteLog("Shop Open For Business!");
            } else {
                if (!PermitCb)
                    WaitRecv(RecvOps.BLUE_POP);
                WaitRecv(RecvOps.UPDATE_SHOP);
                SendPacket(Trade.PutItem(2, (byte)(PermitCb ? 0x41 : 0x21), 1, 1, 1, 999999999));
                WaitRecv(RecvOps.UPDATE_SHOP);
                if (!PermitCb)
                    SendPacket(Trade.OpenShop2());
                SendPacket(Trade.OpenShop());
                Client.hasFMShop = true;
                Client.WriteLog("Shop Open For Business!");
            }
        }

        private void OpenMushy(PacketReader r){
            if(r.ReadByte() == 07){
                SendPacket(Trade.CreateShop(ShopType.MUSHY, ShopName, 1, 5030000));
            }
        }

        private void ServerCheckSteal() { 
            SendPacket(Movement.Teleport(Client.PortalCount, SendOps.FM1_CRC, short.Parse(X), short.Parse(Y), short.Parse(Fh)));
            if (PermitCb)
                SendPacket(Trade.CreateShop(ShopType.PERMIT, ShopName, 1, 5140000));
            else {
                SendPacket(Trade.UseMushy(1));
            }
        }

        private void StealSpot(PacketReader r) {
            Client.WriteLog("Permit Dropped!");
            int uid = r.ReadInt();
            if (TakeAnyCb) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(playerLoader.UidMovementPacket[uid]);
                if (PermitCb)
                    SendPacket(Trade.CreateShop(ShopType.PERMIT, ShopName, 1, 5140000));
                else 
                    SendPacket(Trade.UseMushy(1));
            }
            else if (Ign.Equals(playerLoader.UidMap[uid])) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(playerLoader.UidMovementPacket[uid]);
                if (PermitCb)
                    SendPacket(Trade.CreateShop(ShopType.PERMIT, ShopName, 1, 5140000));
                else
                    SendPacket(Trade.UseMushy(1));
            }
            playerLoader.UidMovementPacket.Remove(uid);
        }


        private void StealSpotMush(PacketReader r) {
            Client.WriteLog("Mush Dropped!");
            int uid = r.ReadInt();
            if (TakeAnyCb) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(playerLoader.UidMushMovementPacket[uid]);
                if (PermitCb)
                    SendPacket(Trade.CreateShop(ShopType.PERMIT, ShopName, 1, 5140000));
                else
                    SendPacket(Trade.UseMushy(1));
            } else if (Ign.Equals(playerLoader.UidMushMap[uid])) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(playerLoader.UidMushMovementPacket[uid]);
                if (PermitCb)
                    SendPacket(Trade.CreateShop(ShopType.PERMIT, ShopName, 1, 5140000));
                else
                    SendPacket(Trade.UseMushy(1));
            }
            playerLoader.UidMushMovementPacket.Remove(uid);
            playerLoader.UidMushMap.Remove(uid);
        }


    }
}
