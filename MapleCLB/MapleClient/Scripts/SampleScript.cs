using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using MapleCLB.MapleClient.Scripts.ScriptLib;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;

namespace MapleCLB.MapleClient.Scripts {
    internal class SampleScript : ComplexScript {
        // All fields should be thread-safe
        public readonly IDictionary<int, string> UidMap = new ConcurrentDictionary<int, string>(); //uid -> ign
        public int PeopleCount; // Used Interlocked for ints

        public SampleScript(Client client) : base(client) { }

        protected override void Init() {
            RegisterRecv(RecvOps.REMOVE_PLAYER, RemovePlayer);
            RegisterRecv(RecvOps.SPAWN_PLAYER, SpawnPlayer);
        }

        protected override void Execute() {
            WriteLog(Thread.CurrentThread.ManagedThreadId + " " + "[SCRIPT] Script started");
            WaitRecv(RecvOps.ALL_CHAT);
            WriteLog("[SCRIPT] Someone Talked 1...");
            WaitRecv(RecvOps.ALL_CHAT);
            WriteLog("[SCRIPT] Someone Talked 2...");
            WaitRecv(RecvOps.ALL_CHAT);
            WriteLog("[SCRIPT] Someone Talked 3...");
            WriteLog(Thread.CurrentThread.ManagedThreadId + " " + "[SCRIPT] Complete");
        }

        /* Handlers */
        private void RemovePlayer(PacketReader r) {
            int uid = r.ReadInt();

            UidMap.Remove(uid);
            Interlocked.Exchange(ref PeopleCount, UidMap.Count);
            WriteLog(Thread.CurrentThread.ManagedThreadId + " " + "[SCRIPT] Removed " + uid);
        }

        private void SpawnPlayer(PacketReader r) {
            int uid = r.ReadInt();
            r.ReadByte();
            string ign = r.ReadMapleString();
            
            UidMap[uid] = ign;
            Interlocked.Exchange(ref PeopleCount, UidMap.Count);
            WriteLog(Thread.CurrentThread.ManagedThreadId + " " + "[SCRIPT] Spawned " + uid + " (" + ign + ")");
        }
    }
}
