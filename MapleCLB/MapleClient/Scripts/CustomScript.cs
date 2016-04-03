using System.Threading;
using MapleCLB.MapleClient.Functions;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Types;
using MapleCLB.Types.Items;

namespace MapleCLB.MapleClient.Scripts {
    internal abstract class CustomScript : UserScript {
        protected readonly Mapler Mapler;
        protected readonly Inventory Inventory;

        protected CustomScript(Client client) : base(client) {
            Mapler = client.Mapler;
            Inventory = client.Inventory;
        }

        public void MoveMap(int mapId) {
            client.MapRush.Report(MapRusher.Pathfind(Mapler.Map, mapId));
            WaitMap(mapId);
        }

        public void WaitMap(int mapId) {
            SpinWait.SpinUntil(() => client.Mapler.Map == mapId);
        }

        public void EnterPortal(string name, short x, short y) {
            var portal = new PortalInfo {
                Name = name,
                Position = new Position(x, y)
            };
            SendPacket(Portal.Enter(client.PortalCount, client.PortalCrc, portal));
        }
    }
}
