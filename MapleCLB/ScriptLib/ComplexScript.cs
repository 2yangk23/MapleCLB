using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MapleCLB.MapleClient;
using MapleLib.Packet;

namespace MapleCLB.ScriptLib {
    internal abstract class ComplexScript : Script {
        private readonly List<ushort> headers;
        private BlockingCollection<Action> scheduler;

        internal ComplexScript(Client client) : base(client) {
            headers = new List<ushort>();
        }

        internal new bool Start() {
            return Start(Run);
        }

        #region Script Managing Functions
        private void Run() {
            scheduler = new BlockingCollection<Action>();
            CancellationTokenSource source = null;
            try {
                // Initialize script
                Init();
                source = new CancellationTokenSource();
                Task.Run(() => StartHandler(source.Token), source.Token);
                // Execute script body
                Execute();
            } catch (InvalidOperationException ex) {
                WriteLog("Error running script. Terminated.");
                Console.WriteLine(ex.ToString());
            }
            // Clean-up script
            Release(source);
            Complete();
            Running = false;
        }

        // TODO: Find better way to clear Scheduler
        // Currently, does nothing with retrieved actions since canceled == true
        private void StartHandler(CancellationToken token) {
            while (!token.IsCancellationRequested) {
                Action handle;
                if (!scheduler.TryTake(out handle, 10000)) {
                    continue;
                }
                if (!token.IsCancellationRequested) {
                    handle();
                }
            }
        }

        private void Release(CancellationTokenSource source) {
            // Unregisters all headers
            headers.ForEach(d => Client.RemoveScriptRecv(d));
            headers.Clear();
            // Stops handler
            source?.Cancel();
        }
        #endregion

        #region Scripting Functions
        protected void RegisterRecv(ushort header, Action<PacketReader> handler) {
            Progress<PacketReader> progress = new Progress<PacketReader>(r => { scheduler.Add(() => handler(r)); });
            if (Client.AddScriptRecv(header, progress)) {
                headers.Add(header);
            } else {
                throw new InvalidOperationException($"Failed to register header {header:X4}.");
            }
        }

        protected void UnregisterRecv(ushort header) {
            headers.Remove(header);
            Client.RemoveScriptRecv(header);
        }

        protected abstract void Init();
        #endregion
    }
}
