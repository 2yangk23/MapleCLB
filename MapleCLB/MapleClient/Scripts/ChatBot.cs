using MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using ScriptLib;

namespace MapleCLB.MapleClient.Scripts {
    internal class ChatBot : ComplexScript<Client> {
        //private readonly PlayerLoader playerLoader;

        public ChatBot(Client client) : base(client) {
            /* ChatBot requires PlayerLoader for Uid -> Name Map */
            //playerLoader = Requires<PlayerLoader>();
        }

        protected override void Init() {
            RegisterRecv(RecvOps.ALL_CHAT, Respond);
        }

        protected override void Execute() {
            WaitRecv(RecvOps.CHAR_INFO);
            SendPacket(Chat.All("Kelvin is gh3y"));
            WaitRecv(0xFFFF); // Wait forever
        }

        private void Respond(PacketReader r) {
            int uid = r.ReadInt();
            r.ReadByte();
            string msg = r.ReadMapleString();
            r.Skip(2);
            byte type = r.ReadByte();

            string ign;
            if (type != 0xFF || !client.UidMap.TryGetValue(uid, out ign)) {
                return;
            }
            // Don't respond to self
            if (ign.Equals(client.Mapler.Name)) {
                return;
            }

            client.WriteLog(ign + ": " + msg);
            SendPacket(Chat.All(msg));
        }
    }
}
