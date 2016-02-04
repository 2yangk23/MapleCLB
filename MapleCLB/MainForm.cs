using System;
using System.Windows.Forms;
using MapleCLB.Tools;
using System.Threading;
using System.ComponentModel;
using MapleCLB.MapleClient;
using MapleCLB.Packets.Send;

namespace MapleCLB {
    public partial class MainForm : Form {
        public Client C;
        private Thread Ct;
        private BackgroundWorker Bw;
        private int Delayms;

        public MainForm() {
            InitializeComponent();

            Win32.SendMessage(username.Handle, Win32.EM_SETCUEBANNER, 0, "Username");
            Win32.SendMessage(password.Handle, Win32.EM_SETCUEBANNER, 0, "Password");
            Win32.SendMessage(pic.Handle, Win32.EM_SETCUEBANNER, 0, "PIC");
            Win32.SendMessage(character.Handle, Win32.EM_SETCUEBANNER, 0, "Character");
            Win32.SendMessage(sendPacket.Handle, Win32.EM_SETCUEBANNER, 0, "Enter packet to send...");
            Win32.SendMessage(delay.Handle, Win32.EM_SETCUEBANNER, 0, "Delay");

            C = new Client();
            world.SelectedIndex = 0;
            channel.SelectedIndex = 0;
            selType.SelectedIndex = 0;
            loginMode.SelectedIndex = 0;

            #if DEBUG
            /*Use this for testing account 
            username.Text           = "";
            password.Text           = "";
            pic.Text                = "";
            character.Text          = "";
            world.SelectedIndex     = 0;
            channel.SelectedIndex   = 0;*/
            #endif
            username.Text = "BL8ldo2@outlook.com";
            password.Text = "teeworlds";
            pic.Text = "777000";
            character.Text = "1";
            world.SelectedIndex = 2;
            channel.SelectedIndex = 6;
        }

        private void connect_Click(object sender, EventArgs e) {
            C.User = username.Text;
            C.Pass = password.Text;
            C.Pic = pic.Text;
            C.Ign = character.Text;

            C.World = (byte)world.SelectedIndex;
            C.Channel = (byte)channel.SelectedIndex;
            C.doWhat = (byte)loginMode.SelectedIndex;

            Ct = new Thread(() => C.Connect()); //Connect to login servers
            Ct.IsBackground = true; //Puts thread in background so it closes with program
            Ct.Start();
        }

        private void disconnect_Click(object sender, EventArgs e) {
            C.Session.Disconnect();
        }

        private void CC_Click(object sender, EventArgs e)
        {
            Program.WriteLog("Changing to Ch 2");
            C.shouldCC = true;
            C.SendPacket(General.ChangeChannel(0x01));
        }


        private void button1_Click(object sender, EventArgs e)
        {

            //int CRC = 0x9FF5D003;
          //  int CRC = 0x03D0F59F;
         //   int X = 0xFE5C;
         //   short Y = 0x0113;
         //  short PID = 0x5A03;

            int X = 0xF574; 
            int Y = 0xFEB4;
            int CRC = 0x5104F9C5;
            int PID = 0xE005;


           // C.SendPacket(HexEncoding.GetBytes("7502EFDF6B060000"));
          //  C.SendPacket(HexEncoding.GetBytes("7502EFDF6B060000"));
            C.SendPacket(HexEncoding.GetBytes("75 0 23D DE 77 42 00 00"));
            C.SendPacket(HexEncoding.GetBytes("75 0 23D DE 77 42 00 00"));

           // System.Threading.Thread.Sleep(1000);
            C.SendPacket(HexEncoding.GetBytes("B9 00 01 28 C2 7A 2A 1D E0 77 42 00 00 00 00 00 75 01 15 FE 00 00 00 00 07 0C 01 00 75 01 16 FE 00 00 3C 00 00 00 00 00 00 00 06 1E 00 00 0C 02 00 75 01 5E FE 00 00 1C 02 00 00 00 00 00 00 06 F0 00 00 0C 03 00 75 01 60 FE 00 00 00 00 00 00 00 00 00 00 06 03 00 00 00 75 01 60 FE 00 00 00 00 0F 00 00 00 00 00 04 ED 00 00 11 00 00 00 00 00 00 00 00 00"));
             //For Adventure Map //C.SendPacket(HexEncoding.GetBytes("B900019FF5D003FD9D7206000000000025FFBD0000000000070C010C020025FFCB000000F000000000000000067800000C030025FF050100001C02000000000000069600000025FF130100000000000000000000061600000025FF1301000000000E000000000004DA000011000000000000000000"));
            //For BW first train map?
            //C.SendPacket(HexEncoding.GetBytes("B9 00 01 C5 F9 04 51 DD 57 1E 33 00 00 00 00 00 34 F4 0E FF 00 00 00 00 06 0C 01 00 34 F4 25 FF 00 00 2C 01 00 00 00 00 00 00 06 96 00 00 00 34 F4 2C FF 00 00 00 00 00 00 00 00 00 00 06 17 00 00 00 34 F4 2C FF 00 00 00 00 28 00 00 00 00 00 04 BB 00 00 0C 02 00 34 F4 2C FF 00 00 00 00 28 00 00 00 00 00 04 96 00 00 11 00 00 00 00 00 00 00 00 00"));
            //C.SendPacket(Movement.Teleport(CRC,(short)X,(short)Y,(short)PID));

        }
        private void connect_EnabledChanged(object sender, EventArgs e) {
            username.Enabled = connect.Enabled;
            password.Enabled = connect.Enabled;
            pic.Enabled = connect.Enabled;
            world.Enabled = connect.Enabled;
            channel.Enabled = connect.Enabled;
            selType.Enabled = connect.Enabled;
            character.Enabled = connect.Enabled;
            loginMode.Enabled = connect.Enabled;
            if (C.doWhat == 1)
            {
                aCS.Checked = !connect.Enabled;
                aCS.Enabled = connect.Enabled;
            }
        }

        private void sMenuSend_Click(object sender, EventArgs e) {
            //Stop spam if currently spamming!
            if (sendSpam.Text.Equals("Stop")) {
                Bw.CancelAsync();
                sendPacket.Enabled = true;
                delay.Enabled = true;
            }

            sendPacket.Width = 505;
            delay.Visible = false;
            sMenuSpam.Checked = false;
            sendSpam.Text = "Send";
            //Console.WriteLine("Send?");
        }

        private void sMenuSpam_Click(object sender, EventArgs e) {
            delay.Visible = true;
            sendPacket.Width = 455;
            sMenuSend.Checked = false;
            sendSpam.Text = "Spam";
        }


        private void sendSpam_Click(object sender, EventArgs e)
        {
            C.SendPacket(HexEncoding.GetBytes(sendPacket.Text));
        }

        private void EnterCS_Click(Object sender, EventArgs e)
        {
            C.SendPacket(General.EnterCS());
            C.SendPacket(HexEncoding.GetBytes("75 02 5A E5 58 5C 00 01"));
            C.SendPacket(HexEncoding.GetBytes("55 01"));
            C.Mode = ClientMode.CASHSHOP;
        }

    private void name_TextChanged(object sender, EventArgs e)
    {
        name.Text = String.Format("Whatever ", name.Text);
    }

        /*private void sendSpam_Click(object sender, EventArgs e) {
            if (!sMenuSpam.Checked) {
                C.SendPacket(sendPacket.Text);
            } else {
                if (sendSpam.Text.Equals("Spam")) {
                    try {
                        Delayms = Convert.ToInt32(delay.Text);
                    } catch (FormatException) //Not a number
                    {
                        MessageBox.Show("Invalid spam delay.", "Error!");
                        return;
                    }

                    if (Delayms > 0) {
                        sendSpam.Text = "Stop";
                        sendPacket.Enabled = false;
                        delay.Enabled = false;
                        //use bg worker
                        Bw = new BackgroundWorker();
                        Bw.WorkerSupportsCancellation = true;
                        Bw.DoWork += new DoWorkEventHandler(
                        delegate(object o, DoWorkEventArgs args) {
                            BackgroundWorker b = o as BackgroundWorker;

                            while (!Bw.CancellationPending) {
                                C.SendPacket(sendPacket.Text);
                                Thread.Sleep(Delayms);
                            }
                        });
                        Bw.RunWorkerAsync();
                    } else //Negative delay
                    {
                        MessageBox.Show("Invalid spam delay.", "Error!");
                    }
                } else if (sendSpam.Text.Equals("Stop")) {
                    Bw.CancelAsync();
                    sendSpam.Text = "Spam";
                    sendPacket.Enabled = true;
                    delay.Enabled = true;
                }
            }
        }*/
    }
}
