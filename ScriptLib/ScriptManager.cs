﻿using System;
using System.Collections.Concurrent;

namespace ScriptLib {
    public class ScriptManager<TClient> where TClient : IScriptClient {
        private readonly TClient client;
        private readonly ConcurrentDictionary<Type, Lazy<Script<TClient>>> scripts;

        internal ScriptManager(TClient client) {
            scripts = new ConcurrentDictionary<Type, Lazy<Script<TClient>>>();
            this.client = client;
        }

        internal TScript Get<TScript>() where TScript : Script<TClient> {
            var k = typeof (TScript);

            return (TScript) scripts.GetOrAdd(k,
                t => new Lazy<Script<TClient>>(() => (TScript) Activator.CreateInstance(t, client))).Value;
        }

        // This will have to do until i figure out way to use generics
        internal void Release(Type t) {
            Lazy<Script<TClient>> trash;
            scripts.TryRemove(t, out trash);
        }
    }
}