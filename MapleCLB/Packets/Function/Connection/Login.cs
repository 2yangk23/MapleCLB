using System.Threading;
using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Recv;

namespace MapleCLB.Packets.Function.Connection {
    class Login {
        public static void LoginSecond(object o, PacketReader r) {
            var c = o as Client;
            switch (r.ReadByte()) {
                case 0x01:
                    c.WriteLog.Report("Incorrect password");
                    return;
                case 0x07:
                    c.WriteLog.Report("Already logged in. Restart in 1 min...");
                    Thread.Sleep(60000);
                    c.Session.Disconnect();
                    return;
            }
            r.Skip(15);
            r.ReadMapleString();
            r.Skip(10);
            r.ReadMapleString();
            r.Skip(12);
            
            c.SessionId = r.ReadLong(); //Get session ID from login recieve
        }

        public static void LoginStatus(object o, PacketReader r) {
            var c = o as Client;
            switch (r.ReadByte()) {
                case 0x01:
                    c.WriteLog.Report("Incorrect Password");
                    return; // Don't try to login again
                case 0x02:
                    c.WriteLog.Report("Banned R.I.P");
                    break;
                case 0x07:
                    c.WriteLog.Report("Already logged in. Restart in 2 mins...");
                    Thread.Sleep(120000);
                    break;
                case 0x09:
                    // End of file crash on real client
                    break;
                default:
                    c.SendPacket(Send.Login.acceptWorld());
                    c.SendPacket(Send.Login.SelectServer(c.World, c.Channel));
                    return;
            }
            c.Session.Disconnect();
        }

        public static void SelectCharacter(object o, PacketReader r) {
            var c = o as Client;
            c.WriteLog.Report("Selecting Character...");
            //no new thread here, MUST finish loading chars
            Load.Character(c, r);

            try {
                switch (c.Select) {
                    case 0:
                        byte n;
                        byte.TryParse(c.Name, out n);
                        c.UserId = c.CharMap[--n];
                        break;
                    case 1:
                        c.UserId = c.CharMap[c.Name.ToLower()];
                        break;
                }
            } catch {
                c.WriteLog.Report("Error selecting character. Restart in 1 min...");
                Thread.Sleep(60000);
                c.Session.Disconnect();
                return;
            }
            c.SendPacket(Send.Login.SelectCharacter(c.UserId, c.Pic));
        }
    }
}
