using System.Collections.Concurrent;
using System.Collections.Generic;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.ScriptLib;
using MapleCLB.Packets.Send;
using MapleCLB.Types;

namespace MapleCLB.MapleClient.Scripts {
    internal class PlayerLoader : ComplexScript {
        // All public fields should be thread-safe
        public readonly IDictionary<int, string> UidMap = new ConcurrentDictionary<int, string>(); //Player-UID's -> ign
        public readonly IDictionary<int, byte[]> UidMovementPacket = new Dictionary<int, byte[]>(); //Player-UID's -> MovementPacket
        public readonly IDictionary<int, string> UidMushMap = new ConcurrentDictionary<int, string>(); //Mushroom UID's -> ign
        public readonly IDictionary<int, byte[]> UidMushMovementPacket = new Dictionary<int, byte[]>(); //Mushroom-UID's -> MovementPacket

        public PlayerLoader(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.REMOVE_PLAYER, RemovePlayer);
            RegisterRecv(RecvOps.SPAWN_PLAYER, SpawnPlayer);
            RegisterRecv(RecvOps.LOAD_MUSHY, SpawnMushy);
        }

        protected override void Execute() {
            WaitRecv(0xFFFF); // Wait forever
        }

        /* Handlers */
        private void RemovePlayer(PacketReader r) {
            int uid = r.ReadInt();

            Client.totalItemCount = Client.totalPeopleCount--;
            Client.UpdatePeople.Report(Client.totalPeopleCount);

            UidMap.Remove(uid);
            WriteLog($"[{uid:X8}] removed.");
        }

        private void SpawnMushy(PacketReader r) {
            int uid = r.ReadInt();
            r.Skip(4);
            short x = r.ReadShort();
            short y = r.ReadShort();
            short fh = r.ReadShort();
            string ign = r.ReadMapleString();
            byte type = r.ReadByte(); 
            int tradeId = r.ReadInt(); // Used to enter shops

            UidMushMap[uid] = ign;
            UidMushMovementPacket[uid] = Movement.Teleport(Client.PortalCount, SendOps.FM1_CRC, x, y, fh);

            WriteLog("Added Mushroom : " + ign + " to UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
        }

        private void SpawnPlayer(PacketReader r)
        {
            int uid = r.ReadInt();
            r.ReadByte(); // [Level (1)]
            string ign = r.ReadMapleString(); // Name
            r.ReadMapleString(); // Ultimate Explorer's Parent
            r.ReadMapleString(); // Guild
            r.Skip(6); // [LogoBG (2)] [ColoUr (1)] [Logo (2)] [ColoUr (1)]

            r.Skip(13); // 00 [40 00 00 00] [01 00 00 00] [00 00 00 00]

            // Sub
            r.Skip(64); //Mostly Zeros 
            r.Skip(4); //-1
            r.Skip(64); //TODO: Figure this part out later but until then cheat
            r.Next(01); //Time Encoding
            r.Skip(4);
            r.Skip(8); // Zero(8)
            r.Skip(30); // [TimeEncode (5)] [00 00] [00 00 00 00] [00 00 00 00] [TimeEncode (5)] [00 00] [00 00 00 00] [00 00 00 00]
            r.Skip(13); // [TimeEncode (5)] [00 00 00 00] [00 00 00 00]
            r.Skip(20); // [TimeEncode (5)] 00 [DE AC 77 DA] [00 00 00 00] [00 00 00 00] [00 00]
            r.Skip(28); // [TimeEncode (5)] [Zero (16)] [TimeEncode (5)] 00 00

            short job = r.ReadShort();// JOB 
            r.Skip(6); // [SubJob (2)] [? (4)]

            Mapler.SkipAppearance(r, job);
            
            r.Skip(4 * 14); // [00 00 00 00] * 14
            r.Skip(14); // [FF FF] [00 00 00 00 00 00 00 00 00 00 00 00]

            short x = r.ReadShort();
            short y = r.ReadShort();
            r.Skip(1);//Type or stance?
            short fh = r.ReadShort();
            r.Skip(18); //Unknown shit
            int tradeId = r.ReadInt(); //tradeId used to enter shops
            UidMap[uid] = ign;

            Client.totalItemCount = Client.totalPeopleCount++;
            Client.UpdatePeople.Report(Client.totalPeopleCount);

            UidMovementPacket[uid] = Movement.Teleport(Client.PortalCount, SendOps.FM1_CRC, x, y, fh);
            WriteLog("Added : " + ign + " to UID : " + uid + "@ " + x + ", " + y + ", fh: " + fh);
        }
    } 
}
