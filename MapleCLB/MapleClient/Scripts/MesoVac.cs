using System.Diagnostics;
using System.Threading;
using MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Tools;

namespace MapleCLB.MapleClient.Scripts {
    internal class MesoVac : ComplexScript {
        private readonly BlockingLinkedList<Item> LootScheduler = new BlockingLinkedList<Item>();

        public MesoVac(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.SPAWN_ITEM, LootMeso);
        }

        protected override void Execute() {
            while (true) { // TODO: Some condition to terminate script
                var item = LootScheduler.GetFirst();
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
            if (type == 1 || type == 3) return; // Don't loot item if visible/disappearing
            uint objectId = r.ReadUInt(); // [ObjectId (4)]
            if (!r.ReadBool()) return; // If not meso, currently cannot loot
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
                    LootScheduler.AddLast(item);
                    break;
                case 2: // Item already on ground
                    LootScheduler.AddFirst(item);
                    break;
            }
        }

        private class Item {
            public short X, Y;
            public uint Id, Crc;
            public long Timestamp;
        }
    }
}
