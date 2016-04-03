using System.Diagnostics;
using System.Threading;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Types;
using MapleCLB.Types.Map;
using MapleLib.Packet;
using SharedTools;

namespace MapleCLB.MapleClient.Scripts {
    internal class MesoVac : UserScript {
        private readonly BlockingLinkedList<Item> lootQueue = new BlockingLinkedList<Item>();

        public MesoVac(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.SPAWN_ITEM, LootMeso);
        }

        protected override void Execute(CancellationToken token) {
            while (!token.IsCancellationRequested) {
                Item item;
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

        private void LootMeso(object o, PacketReader r) {
            r.Skip(1); // 00
            byte type = r.ReadByte(); // [Type (1)] 0 = drop animation, 1 = visible, 2 = spawned, 3 = dissapearing
            if (type == 1 || type == 3) { // Don't loot item if visible/disappearing
                return;
            }
            uint objectId = r.ReadUInt(); // [ObjectId (4)]
            if (!r.ReadBool()) { // If not meso, currently cannot loot
                return;
            }
            r.Skip(21); // [Zero (12)] [MesoAmount/ItemId (4)] [DropType (1)]
            var pos = r.ReadPosition(); // [x (2)] [y (2)]

            var item = new Item(objectId) {
                Position = pos,
                Crc = 0, // Crc for mesos is 0
                Timestamp = Stopwatch.GetTimestamp() / Stopwatch.Frequency
            };

            //TODO: This delay is still not perfect, maybe react on confirmation of loot instead?
            switch (type) {
                case 0: // Item dropping
                    item.Timestamp += 2; // Delay 2 seconds
                    lootQueue.AddLast(item);
                    break;
                case 2: // Item already on ground
                    lootQueue.AddFirst(item);
                    break;
            }
        }
    }
}
