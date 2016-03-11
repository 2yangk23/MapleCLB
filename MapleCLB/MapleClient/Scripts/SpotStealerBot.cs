using System.Collections.Generic;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using System;


namespace MapleCLB.MapleClient.Scripts {
    internal sealed class SpotStealerBot : ComplexScript {
        private readonly PlayerLoader PlayerLoader;
        private const int FM1_CRC = 0x2A7AC228;

        public string IGN { get; set; }
        public string shopName { get; set; }
        public string FH { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public bool PermitCB { get; set; }
        public bool SCMode { get; set; }
        public bool takeAnyCB { get; set; }    

        public SpotStealerBot(Client client) : base(client) {
            PlayerLoader = Requires<PlayerLoader>();
        }

        protected override void Init() {
            RegisterRecv(RecvOps.CLOSE_PERMIT, StealSpot);
            RegisterRecv(RecvOps.CLOSE_MUSHY, StealSpotMush); 
            RegisterRecv(RecvOps.BLUE_POP, OpenMushy);
        }

        //To Do: add custom items to permit
        protected override void Execute() {
            WaitRecv(RecvOps.FINISH_LOAD);
            if (!PermitCB)
                WaitRecv(RecvOps.TEMP); //This shit is guessed... You must wait if its a mushroom.
            if (SCMode){
                ServerCheckSteal();
                if(!PermitCB)
                    WaitRecv(RecvOps.BLUE_POP);
                WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
                if(PermitCB)
                    SendPacket(Trade.PutItem(2, 0x41, 1, 1, 1, 999999999));
                else
                    SendPacket(Trade.PutItem(2, 0x21, 1, 1, 1, 999999999));
                WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
                if(!PermitCB)
                    SendPacket(Trade.OpenShop2());
                SendPacket(Trade.OpenShop());
                Client.hasFMShop = true;
                WriteLog("Shop Open For Business!");
            }
            else {
                if (!PermitCB)
                    WaitRecv(RecvOps.BLUE_POP);
                WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
                if(PermitCB)
                    SendPacket(Trade.PutItem(2,0x41,1, 1, 1, 999999999));
                else
                    SendPacket(Trade.PutItem(2, 0x21, 1, 1, 1, 999999999));
                WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
                if (!PermitCB)
                    SendPacket(Trade.OpenShop2());
                SendPacket(Trade.OpenShop());
                Client.hasFMShop = true;
                WriteLog("Shop Open For Business!");
            }
        }

        private void OpenMushy(PacketReader r){
            if(r.ReadByte() == 07){
                SendPacket(Trade.CreateShop(6, shopName, 1, 5030000));
            }
        }

        private void ServerCheckSteal() { 
            SendPacket(Movement.Teleport(FM1_CRC, (short)Int32.Parse(X), (short)Int32.Parse(Y), (short)Int32.Parse(FH)));
            if (PermitCB)
                SendPacket(Trade.CreateShop(5, shopName, 1, 5140000));
            else {
                SendPacket(Trade.UseMushy(1));
            }
        }

        private void StealSpot(PacketReader r) {
            WriteLog("Permit Dropped!");
            int uid = r.ReadInt();
            if (takeAnyCB) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(PlayerLoader.UidMovementPacket[uid]);
                if (PermitCB)
                    SendPacket(Trade.CreateShop(5, shopName, 1, 5140000));
                else 
                    SendPacket(Trade.UseMushy(1));
            }
            else if (IGN.Equals(PlayerLoader.UidMap[uid])) {
                SendPacket(Movement.beforeTeleport());
                SendPacket(PlayerLoader.UidMovementPacket[uid]);
                if (PermitCB)
                    SendPacket(Trade.CreateShop(5, shopName, 1, 5140000));
                else
                    SendPacket(Trade.UseMushy(1));
            }
            PlayerLoader.UidMovementPacket.Remove(uid);
        }


        private void StealSpotMush(PacketReader r)
        {
            WriteLog("Mush Dropped!");
            int uid = r.ReadInt();
            if (takeAnyCB)
            {
                SendPacket(Movement.beforeTeleport());
                SendPacket(PlayerLoader.UidMushMovementPacket[uid]);
                if (PermitCB)
                    SendPacket(Trade.CreateShop(5, shopName, 1, 5140000));
                else
                    SendPacket(Trade.UseMushy(1));
            }
            else if (IGN.Equals(PlayerLoader.UidMushMap[uid]))
            {
                SendPacket(Movement.beforeTeleport());
                SendPacket(PlayerLoader.UidMushMovementPacket[uid]);
                if (PermitCB)
                    SendPacket(Trade.CreateShop(5, shopName, 1, 5140000));
                else
                    SendPacket(Trade.UseMushy(1));
            }
            PlayerLoader.UidMushMovementPacket.Remove(uid);
            PlayerLoader.UidMushMap.Remove(uid);
        }


    }
}
