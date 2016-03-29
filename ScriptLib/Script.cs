using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using MapleCLB.Tools;
using MapleLib.Packet;
using SharedTools;

namespace ScriptLib {
    [Synchronization(true)]
    internal abstract class Script<TClient> where TClient : IScriptClient {
        private int refs = 1;
        private readonly ScriptManager<TClient> manager;
        private readonly Blocking<PacketReader> reader = new Blocking<PacketReader>();
        private readonly AutoResetEvent waiter = new AutoResetEvent(false);

        protected TClient client;
        protected bool running;

        protected Script(TClient client) {
            manager = client.GetScriptManager<TClient>();
            Precondition.NotNull(manager);
            this.client = client;
        }

        internal bool Start() {
            return Start(Run);
        }

        // Wakes up script that is waiting on any header
        internal void Wake() {
            waiter.Set();
        }

        #region Script Run
        private void Run() {
            Execute();
            Complete();
            running = false;
        }
        #endregion

        #region Shared Implementations
        protected bool Start(Action run) {
            Interlocked.Increment(ref refs);
            if (running) {
                return false;
            }
            running = true;
            client.WriteLog($"[SCRIPT] Started {GetType().Name}.");
            Task.Run(run);
            return true;
        }

        protected void Complete() {
            manager.Release(GetType());
        }

        protected TScript Requires<TScript>() where TScript : Script<TClient> {
            var script = manager.Get<TScript>();
            ComplexScript<TClient> complex = script as ComplexScript<TClient>;

            // Make sure script is started
            if (complex != null) {
                complex.Start();
            } else {
                script.Start();
            }

            return script;
        }
        #endregion

        #region Scripting Functions
        // Returns 'null' if returnPacket is FALSE, else returns received packet
        protected PacketReader WaitRecv(ushort header, bool returnPacket = false) {
            client.WaitScriptRecv(header, reader, returnPacket);
            return reader.Get();
        }

        protected void SendPacket(byte[] packet) {
            client.SendPacket(packet);
        }

        protected void SendPacket(PacketWriter w) {
            client.SendPacket(w);
        }

        protected abstract void Execute();
        #endregion
    }
}
