using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapleCLB.MapleClient;
using MapleCLB.Packets.Send;
using MapleCLB.Tools;
using MapleCLB.Types;

namespace MapleCLB.Forms {
    public partial class ClientForm : UserControl {
        private readonly Client Client;
        public Progress<bool> ConnectToggle;
        public Progress<string> WriteLog;
        public Progress<byte[]> WriteSend, WriteRecv;
        public Progress<Mapler> UpdateMapler; 
        public Progress<byte> UpdateCh;

        public ClientForm() {
            InitializeComponent();
            InitializeProgress();
            PacketView.SetInput(PacketInput);
            Win32.SendMessage(UserInput.Handle, Win32.EM_SETCUEBANNER, 0, "Username");
            Win32.SendMessage(PassInput.Handle, Win32.EM_SETCUEBANNER, 0, "Password");
            Win32.SendMessage(PicInput.Handle, Win32.EM_SETCUEBANNER, 0, "PIC");
            Win32.SendMessage(CharInput.Handle, Win32.EM_SETCUEBANNER, 0, "Character");
            Win32.SendMessage(PacketInput.Handle, Win32.EM_SETCUEBANNER, 0, "Enter packet to send...");
            Win32.SendMessage(DelayInput.Handle, Win32.EM_SETCUEBANNER, 0, "Delay");

            Client = new Client(this);

            #if DEBUG
            /*Use this for testing account 
            username.Text           = "";
            password.Text           = "";
            pic.Text                = "";
            character.Text          = "";
            world.SelectedIndex     = 0;
            channel.SelectedIndex   = 0;*/
            #endif

            UserInput.Text = "bonybonbon1993@gmail.com";
            PassInput.Text = "maplestory";
            PicInput.Text = "777000";
            CharInput.Text = "1";
            SelectList.SelectedIndex = 0;
            WorldList.SelectedIndex = 2;
            ChannelList.SelectedIndex = 6;
            ModeList.SelectedIndex = 0;
        }

        private void InitializeProgress() {
            ConnectToggle = new Progress<bool>(b => {
                ConnectBtn.Text = b ? "Connect" : "Disconnect";
                UserInput.Enabled = b;
                PassInput.Enabled = b;
                PicInput.Enabled = b;
                CharInput.Enabled = b;
                SelectList.Enabled = b;
                WorldList.Enabled = b;
                ChannelList.Enabled = b;
                ModeList.Enabled = b;
            });

            /* Writers */
            WriteLog = new Progress<string>(s => LogText.AppendText(s + Environment.NewLine));
            WriteSend = PacketView.WriteSend;
            WriteRecv = PacketView.WriteRecv;

            /* Stats */
            UpdateMapler = new Progress<Mapler>(m => {
                if (m != null) {
                    NameStat.Text = m.Name;
                    MapStat.Text = m.Map.ToString();
                    LevelStat.Text = m.Level.ToString();
                    MesoStatus.Text = m.Meso.ToString();
                } else {
                    NameStat.Text = "Unknown";
                    MapStat.Text = "-1";
                    LevelStat.Text = "0";
                    MesoStatus.Text = "-1";
                }
            });
            UpdateCh    = new Progress<byte>(d => ChannelStat.Text = d.ToString());
        }

        public bool IsLogSend() {
            return PacketView.LogSend;
        }

        /* Temporary stuff*/
        private void ConnectBtn_Click(object sender, EventArgs e) {
            if (ConnectBtn.Text.Equals("Connect")) {
                Client.User = UserInput.Text;
                Client.Pass = PassInput.Text;
                Client.Pic = PicInput.Text;
                Client.Selection = CharInput.Text;

                Client.Select = (byte) SelectList.SelectedIndex;
                Client.Channel = (byte) ChannelList.SelectedIndex;
                Client.World = (byte) WorldList.SelectedIndex;
                Client.doWhat = (byte) ModeList.SelectedIndex;

                new Thread(Client.Connect) {
                    IsBackground = true
                }.Start();
            } else {
                Client.Disconnect();
            }
        }

        private void CcBtn_Click(object sender, EventArgs e) {
            Client.WriteLog.Report("Changing to Ch 2");
            Client.shouldCC = true;
            Client.SendPacket(General.ChangeChannel(0x01));
        }

        private void MoveBtn_Click(object sender, EventArgs e) {
            //int CRC = 0x9FF5D003;
            //  int CRC = 0x03D0F59F;
            //   int X = 0xFE5C;
            //   short Y = 0x0113;
            //  short PID = 0x5A03;

            //int X = 0xF574;
            //int Y = 0xFEB4;
            //int CRC = 0x5104F9C5;
            //int PID = 0xE005;

            // C.SendPacket(HexEncoding.GetBytes("7502EFDF6B060000"));
            //  C.SendPacket(HexEncoding.GetBytes("7502EFDF6B060000"));
            Client.SendPacket(HexEncoding.GetBytes("75 0 23D DE 77 42 00 00"));
            Client.SendPacket(HexEncoding.GetBytes("75 0 23D DE 77 42 00 00"));

            // System.Threading.Thread.Sleep(1000);
            Client.SendPacket(HexEncoding.GetBytes("B9 00 01 28 C2 7A 2A 1D E0 77 42 00 00 00 00 00 75 01 15 FE 00 00 00 00 07 0C 01 00 75 01 16 FE 00 00 3C 00 00 00 00 00 00 00 06 1E 00 00 0C 02 00 75 01 5E FE 00 00 1C 02 00 00 00 00 00 00 06 F0 00 00 0C 03 00 75 01 60 FE 00 00 00 00 00 00 00 00 00 00 06 03 00 00 00 75 01 60 FE 00 00 00 00 0F 00 00 00 00 00 04 ED 00 00 11 00 00 00 00 00 00 00 00 00"));
            //For Adventure Map //C.SendPacket(HexEncoding.GetBytes("B900019FF5D003FD9D7206000000000025FFBD0000000000070C010C020025FFCB000000F000000000000000067800000C030025FF050100001C02000000000000069600000025FF130100000000000000000000061600000025FF1301000000000E000000000004DA000011000000000000000000"));
            //For BW first train map?
            //C.SendPacket(HexEncoding.GetBytes("B9 00 01 C5 F9 04 51 DD 57 1E 33 00 00 00 00 00 34 F4 0E FF 00 00 00 00 06 0C 01 00 34 F4 25 FF 00 00 2C 01 00 00 00 00 00 00 06 96 00 00 00 34 F4 2C FF 00 00 00 00 00 00 00 00 00 00 06 17 00 00 00 34 F4 2C FF 00 00 00 00 28 00 00 00 00 00 04 BB 00 00 0C 02 00 34 F4 2C FF 00 00 00 00 28 00 00 00 00 00 04 96 00 00 11 00 00 00 00 00 00 00 00 00"));
            //C.SendPacket(Movement.Teleport(CRC,(short)X,(short)Y,(short)PID));
        }

        private void CsBtn_Click(object sender, EventArgs e) {
            Console.WriteLine("Sup not working");
            //Client.SendPacket(General.EnterCS());
            //Client.SendPacket(HexEncoding.GetBytes("75 02 5A E5 58 5C 00 01"));
            //Client.SendPacket(HexEncoding.GetBytes("55 01"));
        }

        private void SendSpamBtn_Click(object sender, EventArgs e) {
            if (PacketInput.Text.Length == 0) return;

            Client.SendPacket(HexEncoding.GetBytes(PacketInput.Text));
            /*if (!sMenuSpam.Checked) {
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
            }*/
        }

        private void SendMenuItem_Click(object sender, EventArgs e) {
            Console.WriteLine("Sup not working");
            SendMenuItem.Checked = true;
            SpamMenuItem.Checked = false;
            //Stop spam if currently spamming!
            /*if (sendSpam.Text.Equals("Stop")) {
                Bw.CancelAsync();
                sendPacket.Enabled = true;
                delay.Enabled = true;
            }

            sendPacket.Width = 505;
            delay.Visible = false;
            sendSpam.Text = "Send";*/
        }

        private void SpamMenuItem_Click(object sender, EventArgs e) {
            Console.WriteLine("Sup not working");
            SendMenuItem.Checked = false;
            SpamMenuItem.Checked = true;
            /*delay.Visible = true;
            sendPacket.Width = 455;
            sendSpam.Text = "Spam";*/
        }
    }
}
