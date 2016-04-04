using System;
using System.Threading;
using MapleCLB.MapleClient;
using MapleCLB.Types;
using MapleLib.Packet;
using SharedTools;

// TODO: The Thread.Sleep here will cause UI to freeze
namespace MapleCLB.Packets.Recv.Connection {
    internal static class Login {
        internal static void LoginStatus(Client c, PacketReader r) {
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

        internal static void LoginSecond(Client c, PacketReader r) {
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

        internal static void LoadCharlist(Client c, PacketReader r) {
            r.Skip(1);
            r.ReadMapleString(); // v170?
            r.Skip(18);

            int temp = r.ReadInt(); // Weird loopy shit (uids?) v167
            for (int i = 0; i < temp; i++) {
                r.ReadInt();
            }
            byte count = r.ReadByte();

            MultiKeyDictionary<byte, string, int> charMap = new MultiKeyDictionary<byte, string, int>(); // slot/ign -> uid
            for (byte i = 0; i < count; ++i) {
                /* Character Stats */
                var m = r.ReadMapler();

                /* Mapler Appearance */
                r.SkipAppearance(m);

                bool hasRank = r.ReadBool(); // [HasRanking (1)]
                if (hasRank) {
                    r.Skip(16); // [Rank (4)] [Rank Move (4)] [JobRank (4)] [JobRank Move (4)]
                }

                if (m.IsZero) { // Zero
                    for (int j = 0; j < 6; ++j) { // I guess Zero has 2 extra appearance?
                        r.Next(0xFF);
                    }
                }
                // System.Diagnostics.Debug.WriteLine("" + chr.Id + " : " + chr.Job + " : " + chr.Name + Environment.NewLine);
                charMap.Add(i, m.Name.ToLower(), m.Id);
            }

            c.Log.Report("Selecting Character...");
            try {
                switch (c.Account.SelectMode) {
                    case SelectMode.SLOT:
                        byte n;
                        byte.TryParse(c.Account.Select, out n);
                        c.UserId = charMap[--n];
                        break;
                    case SelectMode.NAME:
                        c.UserId = charMap[c.Account.Select.ToLower()];
                        break;
                    default:
                        throw new InvalidOperationException("Selection mode " + c.Account.SelectMode + " is not valid.");
                }
                c.SendPacket(Send.Login.SelectCharacter(c.Account, c.UserId));
            } catch {
                c.Log.Report("Error selecting character. Restart in 1 min...");
                Thread.Sleep(60000);
                c.Disconnect();
            }
        }
    }
}
