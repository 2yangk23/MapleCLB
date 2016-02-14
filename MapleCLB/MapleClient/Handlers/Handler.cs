namespace MapleCLB.MapleClient.Handlers {
    abstract class Handler<T> {
        protected Client Client;

        protected Handler(Client client) {
            Client = client;
        }

        protected void SendPacket(params byte[] packet) {
            Client.SendPacket(packet);
        }

        abstract internal void Handle(object o1, T o2);
    }
}
