using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;


namespace MapleCLB.MapleClient.Scripts {
    internal class SpotStealerBot : ComplexScript {

        public SpotStealerBot(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.CLOSE_SHOP, StealSpot);
        }
        //To Do: add custom items to permit
        protected override void Execute() {
            WaitRecv(RecvOps.FINISH_LOAD);
            Client.SendBytePacket(Movement.beforeTeleport());
            Client.SendBytePacket(Movement.beforeTeleport());
            Client.SendBytePacket(Movement.Teleport(Client.FM1CRC, 80, 34, 52)); //Lands on the ground
            WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
            Client.SendBytePacket(Trade.PutItem(02, 01, 1, 1, 9999999));
            WaitRecv(RecvOps.FINISH_LOAD_PERMIT);
            Client.SendBytePacket(Trade.OpenShop());
            WriteLog("Shop Open For Business!");
            WaitRecv(0xFFFF); // Wait forever
        }

        private void StealSpot(PacketReader r) {
            int temp = r.ReadInt();
            if(Client.UidMap[temp].Equals("Rawche")){
                //Client.SendBytePacket(Movement.beforeTeleport()); I dont think you have to send it before your second movement have to test
                Client.SendBytePacket(Movement.beforeTeleport());
                Client.SendBytePacket(Client.UidMovementPacket[temp]);
                WriteLog("Movement Sent!");
                Client.SendBytePacket(Trade.CreateShop(5, "Thanks", 1, 5140000));
            }
        }


    }
}
