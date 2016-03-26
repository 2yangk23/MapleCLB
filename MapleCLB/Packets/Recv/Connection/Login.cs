using System;
using System.Threading;
using MapleCLB.MapleClient;
using MapleCLB.Types;
using MapleLib.Packet;

// TODO: The Thread.Sleep here will cause UI to freeze
namespace MapleCLB.Packets.Recv.Connection {
    internal class Login {
        public static void LoginSecond(object o, PacketReader r) {
            var c = o as Client;
            switch (r.ReadByte()) {
                case 0x01:
                    c.WriteLog.Report("Incorrect password");
                    return;
                case 0x07:
                    c.WriteLog.Report("Already logged in. Restart in 1 min...");
                    Thread.Sleep(60000);
                    c.Disconnect();
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
                    c.SendPacket(Send.Login.GetServers());
                    c.SendPacket(Send.Login.SelectServer(c.Account.World, c.Account.Channel));
                    return;
            }
            c.Disconnect();
        }

        public static void SelectCharacter(object o, PacketReader r) {
            var c = o as Client;
            c.WriteLog.Report("Selecting Character...");
            //no new thread here, MUST finish loading chars
            Load.Charlist(c, r);

            try {
                switch (c.Account.Mode) {
                    case SelectMode.SLOT:
                        byte n;
                        byte.TryParse(c.Account.Select, out n);
                        c.UserId = c.CharMap[--n];
                        break;
                    case SelectMode.NAME:
                        c.UserId = c.CharMap[c.Account.Select.ToLower()];
                        break;
                    default:
                        throw new InvalidOperationException("Selection mode " + c.Account.Mode + " is not valid.");
                }
                c.SendPacket(Send.Login.SelectCharacter(c.Account, c.UserId));
            } catch {
                c.WriteLog.Report("Error selecting character. Restart in 1 min...");
                Thread.Sleep(60000);
                c.Disconnect();
            }
        }
    }
}
