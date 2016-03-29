using System.Diagnostics;
using System.Threading;
using MapleCLB.Packets.Send;
using MapleLib;

namespace MapleCLB.MapleClient.Handlers {
    internal class Handshake : Handler<ServerInfo> {
        internal Handshake(Client client) : base(client) { }

        internal override void Handle(object session, ServerInfo info) {
            Debug.WriteLine("HANDSHAKEEEEE");
            switch (client.State) {
                case ClientState.DISCONNECTED:
                    Debug.WriteLine("Validating login for MapleStory v" + info.Version + "." + info.Subversion);
                    SendPacket(Login.Validate(info.Locale, info.Version, short.Parse(info.Subversion)));
                    string authCode = Auth.GetAuth(client.Account);
                    Debug.WriteLine(authCode);
                    Thread.Sleep(1000);
                    SendPacket(Login.ClientLogin(client.Account, authCode));
                    client.State = ClientState.LOGIN;
                    break;

                case ClientState.LOGIN:
                    client.Log.Report("Logged in!");
                    client.dcst.Enabled = true;
                    SendPacket(Login.EnterServer(client.Account, client.UserId, client.SessionId));
                    client.State = ClientState.GAME;
                    break;

                case ClientState.GAME:
                    SendPacket(Login.EnterServer(client.Account, client.UserId, client.SessionId));
                    break;

                case ClientState.CASHSHOP:
                    Thread.Sleep(2000);
                    SendPacket(Login.EnterServer(client.Account, client.UserId, client.SessionId));
                    Thread.Sleep(2000);
                    SendPacket(General.ExitCS());
                    Debug.WriteLine("Left Cash Shop!");
                    client.State = ClientState.GAME;
                    break;
            }
        }
    }
}
