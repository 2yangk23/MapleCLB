using System;
using System.Threading;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.MapleClient.Scripts {
    abstract class Script {
        private readonly EventWaitHandle Handler = new AutoResetEvent(false);
        protected Client Client;

        protected Script(Client client) {
            Client = client;
        }

        internal void Run() {
            new Thread(Execute) {
                IsBackground = true
            }.Start();
        }

        protected void WaitRecv(short header) {
            Client.WaitRecv.Report(new Tuple<short, EventWaitHandle>(header, Handler));
            Handler.WaitOne();
        }

        protected void RegisterRecv(short header, Action<PacketReader> handler) {
            Progress<PacketReader> progress = new Progress<PacketReader>(handler);
            Client.WaitHandler.Report(new Tuple<short, IProgress<PacketReader>>(header, progress));
        }

        protected void WriteLog(string value) {
            Client.WriteLog.Report(value);
        }

        protected abstract void Execute();
    }
}
