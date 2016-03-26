using System;
using System.Collections.Concurrent;
using MapleCLB.MapleClient;

namespace MapleCLB.ScriptLib {
    internal class ScriptManager {
        private readonly Client client;
        private readonly ConcurrentDictionary<Type, Lazy<Script>> scripts;

        internal ScriptManager(Client client) {
            scripts = new ConcurrentDictionary<Type, Lazy<Script>>();
            this.client = client;
        }

        internal T Get<T>() where T : Script {
            var k = typeof (T);

            return (T) scripts.GetOrAdd(k,
                t => new Lazy<Script>(() => (T) Activator.CreateInstance(t, client))).Value;
        }

        // This will have to do until i figure out way to use generics
        internal void Release(Type t) {
            Lazy<Script> trash;
            scripts.TryRemove(t, out trash);
        }
    }
}
