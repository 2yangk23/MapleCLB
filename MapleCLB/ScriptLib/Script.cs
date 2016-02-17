using System;
using System.Threading;
using System.Threading.Tasks;
using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.ScriptLib {
    internal abstract class Script {
        private readonly AutoResetEvent Waiter = new AutoResetEvent(false);
        private readonly ScriptManager Manager;

        protected Client Client;
        protected bool Running;

        protected Script(Client client) {
            Client = client;
            Manager = client.ScriptManager;
        }

        internal bool Start() {
            return Start(Run);
        }

        // Wakes up script that is waiting on any header
        internal void Wake() {
            Waiter.Set();
        }

        /* Shared Implementations */
        protected bool Start(Action run) {
            if (Running) return false;
            Running = true;
            WriteLog($"[SCRIPT] Started {GetType().Name}.");
            Task.Run(run);
            return true;
        }

        protected T Requires<T>() where T : Script {
            var script = Manager.Get<T>();
            var complex = script as ComplexScript;

            if (complex != null) {
                complex.Start();
            } else {
                script.Start(); // Make sure script is started
            }

            return script;
        }

        /* Script Run */
        private void Run() {
            Execute();
            Running = false;
        }

        /* Scripting Functions */
        protected void WaitRecv(ushort header) {
            Client.WaitScriptRecv(header, Waiter);
            Waiter.WaitOne();
        }

        protected void WriteLog(string value) {
            Client.WriteLog.Report(value);
        }

        protected void SendPacket(byte[] packet) {
            Client.SendPacket(packet);
        }

        protected void SendPacket(PacketWriter w) {
            Client.SendPacket(w.ToArray());
        }

        protected abstract void Execute();
    }
}
