using System.Collections.Concurrent;
using System.Collections.Generic;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.ScriptLib;

namespace MapleCLB.MapleClient.Scripts {
    internal class PlayerLoader : ComplexScript {
        // All public fields should be thread-safe
        public readonly IDictionary<int, string> UidMap = new ConcurrentDictionary<int, string>(); //uid -> ign

        public PlayerLoader(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.REMOVE_PLAYER, RemovePlayer);
            RegisterRecv(RecvOps.SPAWN_PLAYER, SpawnPlayer);
        }

        protected override void Execute() {
            WaitRecv(0xFFFF); // Wait forever
        }

        /* Handlers */
        private void RemovePlayer(PacketReader r) {
            int uid = r.ReadInt();

            UidMap.Remove(uid);
            WriteLog($"[{uid:X8}] removed.");
        }

        private void SpawnPlayer(PacketReader r) {
            int uid = r.ReadInt();
            r.ReadByte();
            string ign = r.ReadMapleString();
            
            UidMap[uid] = ign;
            WriteLog($"[{uid:X8}] {ign} spawned.");
        }
    }
}
