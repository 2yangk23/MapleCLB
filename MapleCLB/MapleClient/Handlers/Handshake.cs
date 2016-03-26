using System.Diagnostics;
using System.Threading;
using MapleCLB.Packets.Send;
using MapleLib;

namespace MapleCLB.MapleClient.Handlers {
    internal class Handshake : Handler<ServerInfo> {
        internal Handshake(Client client) : base(client) { }

        internal override void Handle(object session, ServerInfo info) {
            Debug.WriteLine("HANDSHAKEEEEE");
            switch (Client.Mode) {
                case ClientMode.CONNECTED:
                    Debug.WriteLine("Validating login for MapleStory v" + info.Version + "." + info.Subversion);
                    SendPacket(Login.Validate(info.Locale, info.Version, short.Parse(info.Subversion)));
                    string authCode = Auth.GetAuth(Client.Account);
                    Client.Mode = ClientMode.LOGIN;
                    Debug.WriteLine(authCode);
                    Thread.Sleep(1000);
                    SendPacket(Login.ClientLogin(Client.Account, authCode));
                    break;

                case ClientMode.LOGIN:
                    Debug.WriteLine("Logged in!");
                    Client.StartWatch();
                    Client.displayTimer.Enabled = true;
                    Client.dcst.Enabled = true;
                    SendPacket(Login.EnterServer(Client.Account, Client.UserId, Client.SessionId));
                    Client.Mode = ClientMode.GAME;
                    break;

                case ClientMode.GAME:
                    SendPacket(Login.EnterServer(Client.Account, Client.UserId, Client.SessionId));
                    break;

                case ClientMode.CASHSHOP:
                    Thread.Sleep(2000);
                    SendPacket(Login.EnterServer(Client.Account, Client.UserId, Client.SessionId));
                    Thread.Sleep(2000);
                    SendPacket(General.ExitCS());
                    Client.Mode = ClientMode.GAME;
                    Client.WriteLog.Report("Left CS!");
                    break;
            }
        }
    }
}
