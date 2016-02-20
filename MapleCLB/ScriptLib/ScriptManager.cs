﻿using System;
using System.Collections.Concurrent;
using MapleCLB.MapleClient;

namespace MapleCLB.ScriptLib {
    internal class ScriptManager {
        private readonly ConcurrentDictionary<Type, Lazy<Script>> Scripts = new ConcurrentDictionary<Type, Lazy<Script>>();
        private readonly Client Client;

        internal ScriptManager(Client client) {
            Client = client;
        }

        internal T Get<T>() where T : Script {
            var k = typeof (T);

            return (T) Scripts.GetOrAdd(k, 
                t => new Lazy<Script>(() => (T) Activator.CreateInstance(t, Client))).Value;
        } 
    }
}