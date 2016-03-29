using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MapleLib.Packet;
using SharedTools;

namespace ScriptLib {
    internal abstract class ComplexScript<TClient> : Script<TClient> where TClient : IScriptClient {
        private readonly List<ushort> headers;
        private BlockingCollection<Action> scheduler;

        internal ComplexScript(TClient client) : base(client) {
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
                client.WriteLog("Error running script. Terminated.");
                Console.WriteLine(ex.ToString());
            }
            // Clean-up script
            Release(source);
            Complete();
            running = false;
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
            headers.ForEach(d => client.RemoveScriptRecv(d));
            headers.Clear();
            // Stops handler
            source?.Cancel();
        }
        #endregion

        #region Scripting Functions
        protected void RegisterRecv(ushort header, Action<PacketReader> handler) {
            Progress<PacketReader> progress = new Progress<PacketReader>(r => { scheduler.Add(() => handler(r)); });
            Precondition.Check<InvalidOperationException>(client.AddScriptRecv(header, progress), 
                $"Failed to register header {header:X4}.");
            headers.Add(header);
        }

        protected void UnregisterRecv(ushort header) {
            headers.Remove(header);
            client.RemoveScriptRecv(header);
        }

        protected abstract void Init();
        #endregion
    }
}
