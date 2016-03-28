using System.Diagnostics;
using System.Threading;
using MapleCLB.Packets.Send;
using MapleLib;

namespace MapleCLB.MapleClient.Handlers {
    internal class Handshake : Handler<ServerInfo> {
        internal Handshake(Client client) : base(client) { }

        internal override void Handle(object session, ServerInfo info) {
            Debug.WriteLine("HANDSHAKEEEEE");
            switch (Client.State) {
                case ClientState.CONNECTED:
                    Debug.WriteLine("Validating login for MapleStory v" + info.Version + "." + info.Subversion);
                    SendPacket(Login.Validate(info.Locale, info.Version, short.Parse(info.Subversion)));
                    string authCode = Auth.GetAuth(Client.Account);
                    Client.State = ClientState.LOGIN;
                    Debug.WriteLine(authCode);
                    Thread.Sleep(1000);
                    SendPacket(Login.ClientLogin(Client.Account, authCode));
                    break;

                case ClientState.LOGIN:
                    Debug.WriteLine("Logged in!");
                    Client.dcst.Enabled = true;
                    SendPacket(Login.EnterServer(Client.Account, Client.UserId, Client.SessionId));
                    Client.State = ClientState.GAME;
                    break;

                case ClientState.GAME:
                    SendPacket(Login.EnterServer(Client.Account, Client.UserId, Client.SessionId));
                    break;

                case ClientState.CASHSHOP:
                    Thread.Sleep(2000);
                    SendPacket(Login.EnterServer(Client.Account, Client.UserId, Client.SessionId));
                    Thread.Sleep(2000);
                    SendPacket(General.ExitCS());
                    Client.State = ClientState.GAME;
                    Client.Log.Report("Left CS!");
                    break;
            }
        }
    }
}
