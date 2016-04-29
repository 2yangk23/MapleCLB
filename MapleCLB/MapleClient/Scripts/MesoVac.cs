using System.Diagnostics;
using System.Threading;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Types.Map;
using MapleLib.Packet;
using SharedTools;

namespace MapleCLB.MapleClient.Scripts {
    internal class MesoVac : UserScript {
        private readonly BlockingLinkedList<DroppedItem> lootQueue = new BlockingLinkedList<DroppedItem>();

        public MesoVac(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.SPAWN_ITEM, SpawnItem);
            RegisterRecv(RecvOps.REMOVE_ITEM, RemoveItem);
        }

        protected override void Execute(CancellationToken token) {
            while (!token.IsCancellationRequested) {
                DroppedItem item;
                if (!lootQueue.TryGetFirst(out item, 1000)) {
                    continue;
                }
                Thread.Sleep(30);
                while (item.Timestamp > Stopwatch.GetTimestamp() / Stopwatch.Frequency) {
                    Thread.Sleep(300);
                }
                SendPacket(Map.LootItem(item));
            }
        }

        private void SpawnItem(Client c, PacketReader r) {
            var item = r.ReadMapItem();
            if (item.Type == DropType.VISIBLE || item.Type == DropType.DISAPPEARING) {
                return;
            }

            //TODO: This delay is still not perfect, maybe react on confirmation of loot instead?
            switch (item.Type) {
                case DropType.DROPPING:
                    item.Timestamp += 2; // Delay 2 seconds
                    lootQueue.AddLast(item);
                    break;
                case DropType.SPAWNED:
                    lootQueue.AddFirst(item);
                    break;
            }
        }
        //Why even waste time on catching the recv for removing items?
        private void RemoveItem(Client c, PacketReader r) {
            if (System.Convert.ToBoolean(r.ReadByte())) // Animation?
            {
                r.ReadInt(); // Object Id
                r.ReadInt(); // Looter Uid
            }
        }
    }
}
