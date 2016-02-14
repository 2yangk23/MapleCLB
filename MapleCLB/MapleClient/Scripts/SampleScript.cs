using System.Collections.Generic;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;

namespace MapleCLB.MapleClient.Scripts {
    class SampleScript : Script {
        private readonly Dictionary<int, string> UidMap = new Dictionary<int, string>(); //uid -> ign

        internal SampleScript(Client client) : base(client) {
            RegisterRecv(RecvOps.REMOVE_PLAYER, RemovePlayer);
            RegisterRecv(RecvOps.SPAWN_PLAYER, SpawnPlayer);
        }

        protected override void Execute() {
            WriteLog("[SCRIPT] Script started");
            WaitRecv(RecvOps.ALL_CHAT);
            WriteLog("[SCRIPT] Someone Talked 1...");
            WaitRecv(RecvOps.ALL_CHAT);
            WriteLog("[SCRIPT] Someone Talked 2...");
            WaitRecv(RecvOps.ALL_CHAT);
            WriteLog("[SCRIPT] Someone Talked 3...");
            WriteLog("[SCRIPT] Complete");
        }

        private void RemovePlayer(PacketReader r) {
            int uid = r.ReadInt();

            UidMap.Remove(uid);
            WriteLog("[SCRIPT] Removed " + uid);
        }

        private void SpawnPlayer(PacketReader r) {
            int uid = r.ReadInt();
            r.ReadByte();
            string ign = r.ReadMapleString();

            UidMap[uid] = ign;
            WriteLog("[SCRIPT] Spawned " + uid + " (" + ign + ")");
        }
    }
}
