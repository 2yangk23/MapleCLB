using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using MapleCLB.MaplePacketLib;
using MapleCLB.User;
using MaplePacketLib;

namespace MapleCLB.Packets.Function
{
    class ResetAuth : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            Program.writeLog(("Login auth failed.  Clearing auth..."));
            c.authCode = ""; //causes client to fetch new auth
            c.session.Disconnect();
        }
    }

    class LoginSecond : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            if (r.ReadByte() == 0x07)
            {
                Program.writeLog(("Already logged in. Restarting in 1 minute..."));
                Thread.Sleep(60000);
                c.session.Disconnect();
                return;
            }
            r.Skip(15);
            r.ReadMapleString();
            r.Skip(10);
            r.ReadMapleString();
            r.Skip(12);
            c.sessionID = r.ReadLong(); //Get session ID from login recieve
        }
    }

    class SelectCharacter : PacketFunction
    {
        public void Handle(Client c, PacketReader r)
        {
            Program.writeLog(("Selecting Character..."));
            //no new thread here, MUST finish loading chars
            Load.Character(c, r);

            int select = 0;
            Program.gui.Invoke((MethodInvoker)delegate { select = Program.gui.selType.SelectedIndex; }); //invoke so it waits

            switch (select)
            {
                case 0:
                    try
                    {
                        byte n;
                        Byte.TryParse(c.ign, out n);
                        c.uid = c.charMap[--n];
                    }
                    catch
                    {
                        Program.writeLog(("Error selecting character. Restarting in 1 minute..."));
                        Thread.Sleep(60000);
                        c.session.Disconnect();
                        return;
                    }
                    break;

                case 1:
                    try
                    {
                        c.uid = c.charMap[c.ign.ToLower()];
                    }
                    catch (KeyNotFoundException)
                    {
                        Program.writeLog(("Error selecting character. Restarting in 1 minute..."));
                        Thread.Sleep(60000);
                        c.session.Disconnect();
                        return;
                    }
                    break;
            }
            c.SendPacket(Login.SelectCharacter(c.uid, c.pic));
        }
    }
}
