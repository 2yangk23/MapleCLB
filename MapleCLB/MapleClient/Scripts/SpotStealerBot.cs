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

        protected override void Execute() {
            WaitRecv(0xFFFF); // Wait forever
        }

        private void StealSpot(PacketReader r) {
            int temp = r.ReadInt();
            if(Client.UidMap[temp] == "Rawche")
            {
                Client.SendBytePacket(Tools.HexEncoding.ToByteArray("75 02 A7 E6 7A 0E 00 00"));
                Client.SendBytePacket(Tools.HexEncoding.ToByteArray("75 02 A7 E6 7A 0E 00 00"));
                Client.SendBytePacket(Client.UidMovementPacket[temp]);
                WriteLog("Movement Sent!");
                SendPacket(Trade.CreateShop(5, "???? Thanks", 1, 5140000));
                WriteLog("Create Shop Sent!");
                SendPacket(Trade.PutItem(02, 01, 1, 1, 9999999));
                WriteLog("Put Item Sent!");
                SendPacket(Trade.OpenShop());
                WriteLog("Opened Sent!");
            }
        }
    }
}
