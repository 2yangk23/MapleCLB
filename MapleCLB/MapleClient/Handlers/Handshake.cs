using System.Diagnostics;
using MapleCLB.MapleLib;
using MapleCLB.Packets.Send;

namespace MapleCLB.MapleClient.Handlers {
    internal class Handshake : Handler<ServerInfo> {
        internal Handshake(Client client) : base(client) { }

        internal override void Handle(object session, ServerInfo info) {
            Debug.WriteLine("HANDSHAKEEEEE");
            switch (Client.Mode) {
                case ClientMode.CONNECTED:
                    Debug.WriteLine(("Validating login for MapleStory v" + info.Version + "." + info.Subversion));
                    SendPacket(Login.Validate(info.Locale, info.Version, short.Parse(info.Subversion)));
                    //SendPacket(0x66, 0x00, 0x08);
                    string authCode = Auth.GetAuth(Client.User, Client.Pass);
                    Client.Mode = ClientMode.LOGIN;
                    Debug.WriteLine(authCode);
                    System.Threading.Thread.Sleep(1500);
                    SendPacket(Login.ClientLogin(Client.Pass, authCode));
                    break;
                case ClientMode.LOGIN:
                    Debug.WriteLine("Logged in!");
                    // Client.cst.Enabled = true;
                    Client.ccst.Enabled = true;
                    SendPacket(Login.EnterServer(Client.World, Client.UserId, Client.SessionId));
                    Client.Mode = ClientMode.GAME;
                    break;
                case ClientMode.GAME:
                    SendPacket(Login.EnterServer(Client.World, Client.UserId, Client.SessionId));
                    
                    break;
                case ClientMode.CASHSHOP:
                    System.Threading.Thread.Sleep(2000);
                    SendPacket(Login.EnterServer(Client.World, Client.UserId, Client.SessionId));
                    System.Threading.Thread.Sleep(2000);
                    SendPacket(General.ExitCS());
                    Client.Mode = ClientMode.GAME;
                    Client.WriteLog.Report(("Left CS!"));
                    break;
            }
        }
    }
}
