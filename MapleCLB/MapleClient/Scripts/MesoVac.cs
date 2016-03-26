using System.Diagnostics;
using System.Threading;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Tools;
using MapleLib.Packet;

namespace MapleCLB.MapleClient.Scripts {
    internal class MesoVac : ComplexScript {
        private readonly BlockingLinkedList<Item> lootQueue = new BlockingLinkedList<Item>();

        public MesoVac(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.SPAWN_ITEM, LootMeso);
        }

        protected override void Execute() {
            while (true) { // TODO: Some condition to terminate script
                var item = lootQueue.GetFirst();
                Thread.Sleep(30);
                while (item.Timestamp > Stopwatch.GetTimestamp() / Stopwatch.Frequency) {
                    Thread.Sleep(300);
                }
                SendPacket(Map.LootItem(item.X, item.Y, item.Id, item.Crc));
            }
        }

        private void LootMeso(PacketReader r) {
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
            short x = r.ReadShort(); // [x (2)]
            short y = r.ReadShort(); // [y (2)]

            var item = new Item {
                X = x,
                Y = y,
                Id = objectId,
                Crc = 0, // Crc for mesos is 0
                Timestamp = Stopwatch.GetTimestamp() / Stopwatch.Frequency
            };

            switch (type) {
                case 0: // Item dropping
                    item.Timestamp++; // Delay 1 second
                    lootQueue.AddLast(item);
                    break;
                case 2: // Item already on ground
                    lootQueue.AddFirst(item);
                    break;
            }
        }

        private class Item {
            public uint Id, Crc;
            public long Timestamp;
            public short X, Y;
        }
    }
}
