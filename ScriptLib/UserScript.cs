using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using MapleCLB.Tools;
using MapleLib.Packet;
using SharedTools;

namespace ScriptLib {
    [Synchronization(true)]
    public abstract class UserScript<TClient> : Script<TClient> where TClient : IScriptClient {
        private readonly ScriptManager<TClient> manager;
        private readonly HashSet<Script<TClient>> dependencies = new HashSet<Script<TClient>>(); 
        private readonly Blocking<PacketReader> reader = new Blocking<PacketReader>();
        private readonly AutoResetEvent waiter = new AutoResetEvent(false);
        private readonly CancellationTokenSource source = new CancellationTokenSource();

        protected UserScript(TClient client) : base(client) {
            manager = client.GetScriptManager<TClient>();
            Precondition.NotNull(manager);
        }

        #region Script Commands
        public new bool Start() {
            return Start(Run);
        }

        public new void Stop() {
            source?.Cancel();
            base.Stop();
            foreach (Script<TClient> script in dependencies) {
                script.Stop();
            }
        }

        // Wakes up script that is waiting on any header
        public void Wake() {
            waiter.Set();
        }

        private void Run() {
            try {
                Init();
                Task.Run(() => Execute(source.Token), source.Token);
            } catch (InvalidOperationException ex) {
                Debug.WriteLine($"Error running {GetType().Name}.  Terminated.");
                Debug.WriteLine(ex.ToString());
            }
            manager.Release(GetType());
            running = false;
        }
        #endregion

        #region Scripting Functions
        // Returns 'null' if returnPacket is FALSE, else returns received packet
        protected PacketReader WaitRecv(ushort header, bool returnPacket = false) {
            client.WaitScriptRecv(header, reader, returnPacket);
            return reader.Get();
        }

        protected TScript Requires<TScript>() where TScript : Script<TClient> {
            var script = manager.Get<TScript>();
            Precondition.Check<InvalidOperationException>(!(script is UserScript<TClient>),
                "You cannot require a UserScript.");
            Precondition.Check<InvalidOperationException>(dependencies.Contains(script),
                $"{script.GetType().Name} is already a dependency.");
            script.Start(); // Make sure script is started
            dependencies.Add(script);

            return script;
        }

        protected abstract void Execute(CancellationToken token);
        #endregion
    }
}
