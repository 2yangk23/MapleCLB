using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;

namespace MapleCLB.MapleClient.Scripts {
    internal class ChatBot : ComplexScript {
        private readonly PlayerLoader PlayerLoader;

        public ChatBot(Client client) : base(client) {
            /* ChatBot requires PlayerLoader for Uid -> Name Map */
            PlayerLoader = Requires<PlayerLoader>();
        }

        protected override void Init() {
            RegisterRecv(RecvOps.ALL_CHAT, Respond);
        }

        protected override void Execute() {
            WaitRecv(RecvOps.CHAR_INFO);
            SendPacket(Chat.All("I have arrived"));
            WaitRecv(-1); // Wait forever
        }

        private void Respond(PacketReader r) {
            int uid = r.ReadInt();
            r.ReadByte();
            string msg = r.ReadMapleString();
            r.Skip(2);
            byte type = r.ReadByte();

            string ign;
            if (type == 0xFF && PlayerLoader.UidMap.TryGetValue(uid, out ign)) {
                WriteLog(ign + ": " + msg);
                SendPacket(Chat.All(msg));
            }
        }
    }
}
