using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using MapleCLB.MapleClient;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Packets.Recv;

namespace MapleCLB.Packets.Function.Connection {
    class Login {
        public static void LoginSecond(object o, PacketReader r) {
            var c = o as Client;
            switch (r.ReadByte())
            {
                case 0x01:
                    Program.WriteLog(("Incorrect password"));
                    return;
                case 0x07:
                    Program.WriteLog(("Already logged in. Restarting in 1 minute..."));
                    Thread.Sleep(60000);
                    c.Session.Disconnect();
                    return;
            }
            r.Skip(15);
            r.ReadMapleStringv2();
            r.Skip(11);
            r.ReadMapleStringv2();
            
            r.Skip(13);
            
            c.SessionId = r.ReadLong(); //Get session ID from login recieve
        }



        public static void LoginStatus(object o, PacketReader r)
        {
            var c = o as Client;
            switch (r.ReadByte())
            {
                case 0x01:
                    Program.WriteLog(("Incorrect Password"));
                    c.Session.Disconnect();
                    return;
                case 0x02:
                    Program.WriteLog(("Banned R.I.P "));
                    c.Session.Disconnect();
                    return;
                case 0x07:
                    Program.WriteLog(("Already logged in. Restart in 1 Minute"));
                    Thread.Sleep(120000);
                    c.Session.Disconnect();
                    return;
                default:
                c.SendPacket(Send.Login.acceptWorld());
                c.SendPacket(Send.Login.SelectServer(c.World, c.Channel));
                return;
            }

        }



        public static void SelectCharacter(object o, PacketReader r) {
            var c = o as Client;
            Program.WriteLog(("Selecting Character..."));
            //no new thread here, MUST finish loading chars
            Load.Character(c, r);

            int select = 0;
            Program.Gui.Invoke((MethodInvoker)delegate { select = Program.Gui.selType.SelectedIndex; }); //invoke so it waits

            switch (select) {
                case 0:
                    try {
                        byte n;
                        Byte.TryParse(c.Ign, out n);
                        c.UserId = c.CharMap[--n];
                    } catch {
                        Program.WriteLog(("Error selecting character. Restarting in 1 minute..."));
                        Thread.Sleep(60000);
                        c.Session.Disconnect();
                        return;
                    }
                    break;

                case 1:
                    try {
                        c.UserId = c.CharMap[c.Ign.ToLower()];
                    } catch (KeyNotFoundException) {
                        Program.WriteLog(("Error selecting character. Restarting in 1 minute..."));
                        Thread.Sleep(60000);
                        c.Session.Disconnect();
                        return;
                    }
                    break;
            }
            c.SendPacket(Send.Login.SelectCharacter(c.UserId, c.Pic));
        }
    }
}
