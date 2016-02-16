using System;
using System.Threading;
using System.Threading.Tasks;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.MapleClient.Scripts.ScriptLib {
    internal abstract class Script {
        private readonly AutoResetEvent Waiter = new AutoResetEvent(false);

        protected Task ScriptTask;
        protected Client Client;

        protected Script(Client client) {
            Client = client;
        }

        internal void Start() {
            ScriptTask = Task.Run(() => Run());
        }

        internal bool IsRunning() {
            return TaskStatus.Running.Equals(ScriptTask.Status);
        }

        protected void Run() {
            Execute();
        }

        /* Scripting Functions */
        protected void WaitRecv(short header) {
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
