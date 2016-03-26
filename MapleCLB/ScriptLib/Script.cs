using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using MapleCLB.MapleClient;
using MapleCLB.Tools;
using MapleLib.Packet;

namespace MapleCLB.ScriptLib {
    [Synchronization(true)]
    internal abstract class Script {
        private readonly ScriptManager manager;
        private readonly Blocking<PacketReader> reader = new Blocking<PacketReader>();
        private readonly AutoResetEvent waiter = new AutoResetEvent(false);

        protected Client Client;
        protected bool Running;

        protected Script(Client client) {
            Client = client;
            manager = client.ScriptManager;
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
            Running = false;
        }
        #endregion

        #region Shared Implementations
        protected bool Start(Action run) {
            if (Running) {
                return false;
            }
            Running = true;
            WriteLog($"[SCRIPT] Started {GetType().Name}.");
            Task.Run(run);
            return true;
        }

        protected void Complete() {
            manager.Release(GetType());
        }

        protected T Requires<T>() where T : Script {
            var script = manager.Get<T>();
            var complex = script as ComplexScript;

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
        protected void WaitRecv(ushort header) {
            Client.WaitScriptRecv(header, waiter);
            waiter.WaitOne();
        }

        protected PacketReader WaitRecv2(ushort header) {
            Client.WaitScriptRecv2(header, reader);
            return reader.Get();
        }

        protected void WriteLog(string value) {
            Client.WriteLog.Report(value);
        }

        protected void SendPacket(byte[] packet) {
            Client.SendPacket(packet);
        }

        protected void SendPacket(PacketWriter w) {
            Client.SendPacket(w);
        }

        protected abstract void Execute();
        #endregion
    }
}
