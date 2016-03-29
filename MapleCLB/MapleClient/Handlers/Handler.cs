namespace MapleCLB.MapleClient.Handlers {
    internal abstract class Handler<T> {
        protected Client client;

        protected Handler(Client client) {
            this.client = client;
        }

        protected void SendPacket(params byte[] packet) {
            client.SendPacket(packet);
        }

        internal abstract void Handle(object o1, T o2);
    }
}
