using MapleCLB.Packets;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Types.Map;
using MapleLib.Packet;

namespace MapleCLB.MapleClient.Scripts {
    internal class ChatBot : Script {
        //private readonly PlayerLoader playerLoader;

        public ChatBot(Client client) : base(client) {
            /* ChatBot requires PlayerLoader for Uid -> Name Map */
            //playerLoader = Requires<PlayerLoader>();
        }

        protected override void Init() {
            RegisterRecv(RecvOps.ALL_CHAT, Respond);
        }

        private void Respond(Client c, PacketReader r) {
            int uid = r.ReadInt();
            r.ReadByte();
            string msg = r.ReadMapleString();
            r.Skip(2);
            byte type = r.ReadByte();

            Player player;
            if (type != 0xFF || !client.UidMap.TryGetValue(uid, out player)) {
                return;
            }
            // Don't respond to self
            if (player.Ign.Equals(client.Mapler.Name)) {
                return;
            }

            client.WriteLog(player.Ign + ": " + msg);
            SendPacket(Chat.All(msg));
        }
    }
}
