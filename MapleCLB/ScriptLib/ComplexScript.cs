using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;

namespace MapleCLB.ScriptLib {
    internal abstract class ComplexScript : Script {
        private readonly List<short> Headers;
        private BlockingCollection<Action> Scheduler;

        internal ComplexScript(Client client) : base(client) {
            Headers = new List<short>();
        }

        internal new bool Start() {
            return Start(Run);
        }

        /* Script Managing Functions */
        private void Run() {
            Scheduler = new BlockingCollection<Action>();
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
            Running = false;
        }

        // TODO: Find better way to clear Scheduler
        // Currently, does nothing with retrieved actions since canceled == true
        private void StartHandler(CancellationToken token) {
            while (!token.IsCancellationRequested) {
                Action handle;
                if (!Scheduler.TryTake(out handle, 10000)) {
                    continue;
                }
                if (!token.IsCancellationRequested) {
                    handle();
                }
            }
        }

        private void Release(CancellationTokenSource source) {
            // Unregisters all headers
            Headers.ForEach(d => Client.RemoveScriptRecv(d));
            Headers.Clear();
            // Stops handler
            source?.Cancel();
        }

        /* Scripting Functions */
        protected void RegisterRecv(short header, Action<PacketReader> handler) {
            Progress<PacketReader> progress = new Progress<PacketReader>(r => {
                Scheduler.Add(() => handler(r));
            });
            if (Client.AddScriptRecv(header, progress)) {
                Headers.Add(header);
            } else {
                throw new InvalidOperationException($"Failed to register header {header:X4}.");
            }
        }

        protected void UnregisterRecv(short header) {
            Headers.Remove(header);
            Client.RemoveScriptRecv(header);
        }

        protected abstract void Init();
    }
}
