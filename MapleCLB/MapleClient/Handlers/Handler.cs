﻿namespace MapleCLB.MapleClient.Handlers {
    internal abstract class Handler<T> {
        protected Client Client;

        protected Handler(Client client) {
            Client = client;
        }

        protected void SendPacket(params byte[] packet) {
            Client.SendPacket(packet);
        }

        internal abstract void Handle(object o1, T o2);
    }
}
