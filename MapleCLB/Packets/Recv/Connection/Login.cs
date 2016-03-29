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
                    c.Log.Report("Incorrect password");
                    return;
                case 0x07:
                    c.Log.Report("Already logged in. Restart in 1 min...");
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
                    c.Log.Report("Incorrect Password");
                    return; // Don't try to login again
                case 0x02:
                    c.Log.Report("Banned R.I.P");
                    break;
                case 0x07:
                    c.Log.Report("Already logged in. Restart in 2 mins...");
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
            c.Log.Report("Selecting Character...");
            //no new thread here, MUST finish loading chars
            LoadCharlist(c, r);

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
                c.Log.Report("Error selecting character. Restart in 1 min...");
                Thread.Sleep(60000);
                c.Disconnect();
            }
        }

        private static void LoadCharlist(Client c, PacketReader pr) {
            pr.Skip(1);
            pr.ReadMapleString(); // v170?
            pr.Skip(18);

            int temp = pr.ReadInt(); // Weird loopy shit (uids?) v167
            for (int i = 0; i < temp; i++) {
                pr.ReadInt();
            }
            byte count = pr.ReadByte();

            for (byte i = 0; i < count; ++i) {
                /* Character Stats */
                var m = Mapler.Parse(pr);

                /* AddPlayer Appearance */
                Mapler.SkipAppearance(pr, m.Job);

                bool hasRank = pr.ReadBool(); // [HasRanking (1)]
                if (hasRank) {
                    pr.Skip(16); // [Rank (4)] [Rank Move (4)] [JobRank (4)] [JobRank Move (4)]
                }

                if (m.IsZero) { // Zero
                    for (int j = 0; j < 6; ++j) { // I guess Zero has 2 extra appearance?
                        pr.Next(0xFF);
                    }
                }
                // System.Diagnostics.Debug.WriteLine("" + chr.Id + " : " + chr.Job + " : " + chr.Name + Environment.NewLine);
                c.CharMap.Add(i, m.Name.ToLower(), m.Id);
            }
        }
    }
}
