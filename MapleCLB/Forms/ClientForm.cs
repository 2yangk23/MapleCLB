﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapleCLB.MapleClient;
using MapleCLB.MapleClient.Scripts;
using MapleCLB.Packets.Send;
using MapleCLB.Types;
using SharedTools;

namespace MapleCLB.Forms {
    public partial class ClientForm : UserControl {
        private readonly Client client;
        private DateTime uptime;

        public IProgress<bool> ConnectToggle;
        public IProgress<byte[]> WriteSend, WriteRecv;
        public Progress<string> WriteLog;
        public Progress<Mapler> UpdateMapler; 
        public Progress<byte> UpdateCh;

        public Progress<long> UpdateMesos; 
        public Progress<long> UpdateExp;
        public Progress<int> UpdateItems;
        public Progress<int> UpdatePeople;
        public Progress<string> UpdateWorking;

        public FreeMarketForm FmFunctions;

        public Information Test = new Information();
        public bool IsLogSend => PacketView.LogSend;

        public ClientForm() {
            InitializeComponent();
            UserInput.SetCueBanner("Username");
            PassInput.SetCueBanner("Password");
            PicInput.SetCueBanner("PIC");
            CharInput.SetCueBanner("Character");
            PacketInput.SetCueBanner("Enter packet to send...");
            DelayInput.SetCueBanner("Delay");
            InitializeProgress();

            client = new Client(this);
            PacketView.SetInput(PacketInput);
            RushView.SetClient(client);

            FmFunctions = new FreeMarketForm(this);

#if DEBUG
            /*Use this for testing account 
            username.Text           = "";
            password.Text           = "";
            pic.Text                = "";
            character.Text          = "";
            world.SelectedIndex     = 0;
            channel.SelectedIndex   = 0;*/
#endif
            InitializeAccount();
        }

        public void InitializeAccount(Account account = null) {
            if (account == null) {
                string[] users = {
                    "T.heOldKingCoal@gmail.com",
                    "t.hemapleblc@gmail.com",
                    "t.h.e.m.apleblc@gmail.com",
                    "t.h.e.m.a.pleblc@gmail.com",
                    "t.h.e.m.a.p.leblc@gmail.com",
                    "t.h.e.m.a.p.l.eblc@gmail.com",
                    "The.OldKingCoal@gmail.com",
                    "TheOl.dKingCoal@gmail.com"
                };

                UserInput.Text = users[Math.Abs(Environment.TickCount) % users.Length];
                PassInput.Text = "maplestory";
                PicInput.Text = "777000";
                CharInput.Text = "1";
                SelectList.SelectedIndex = 0;
                WorldList.SelectedIndex = 2;
                ChannelList.SelectedIndex = 6;
                ModeList.SelectedIndex = 0;
            } else {
                UserInput.Text = account.Username;
                PassInput.Text = account.Password;
                PicInput.Text = account.Pic;
                CharInput.Text = account.Select;
                SelectList.SelectedIndex = (byte)account.SelectMode;
                WorldList.SelectedIndex = account.World;
                ChannelList.SelectedIndex = account.Channel;
                ModeList.SelectedIndex = 0;
            }
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
                UpTimer.Enabled = !b;
                uptime = new DateTime(); // reset time after disconnect?
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
                    ExpStatus.Text = (decimal.Divide(m.Exp, Resources.Exp.PlayerExp[m.Level]) * 100).ToString("F") + '%';
                    RushView.Update(m.Map);
                } else {
                    NameStat.Text = "Unknown";
                    MapStat.Text = "-1";
                    LevelStat.Text = "-1";
                    ExpStatus.Text = "-1";
                    //Just going to leave this here... :D
                    ItemsStatus.Text = "-1";
                    PeopleStatus.Text = "Not Active";
                }
            });
            UpdateMesos = new Progress<long>(d => MesoStatus.Text = d.ToString());
            UpdateCh    = new Progress<byte>(d => ChannelStat.Text = d.ToString());
            UpdateItems = new Progress<int>(d => ItemsStatus.Text = d.ToString());
            UpdatePeople = new Progress<int>(d => PeopleStatus.Text = d.ToString());
        }

        public Account GetAccount() {
            return new Account {
                Username = UserInput.Text,
                Password = PassInput.Text,
                Pic = PicInput.Text,
                Select = CharInput.Text,
                SelectMode = (SelectMode)SelectList.SelectedIndex,
                Channel = (byte)ChannelList.SelectedIndex,
                World = (byte)WorldList.SelectedIndex
            };
        }

        /* Temporary stuff*/
        private void ConnectBtn_Click(object sender, EventArgs e) {
            if (ConnectBtn.Text.Equals("Connect")) {
                var account = GetAccount();
                client.doWhat = (byte) ModeList.SelectedIndex;

                Task.Factory.StartNew(() => {
                    client.Initialize(account);
                    client.Connect();
                }, TaskCreationOptions.LongRunning);
            } else {
                client.Disconnect();
            }
        }

        private void InitTestBtn_Click(object sender, EventArgs e) {
            Task.Factory.StartNew(() => client.Initialize(null), TaskCreationOptions.LongRunning);
        }

        private void CcBtn_Click(object sender, EventArgs e) {
            client.Log.Report("Changing to Ch 2");
            client.SendPacket(General.ChangeChannel(0x01));
        }

        private void MoveBtn_Click(object sender, EventArgs e) {
            client.SendPacket(Movement.beforeTeleport());
            client.SendPacket(Movement.beforeTeleport());
            client.SendPacket(Movement.Teleport(client.PortalCount, 0x26F611E3, new Position(80, 34), 52));
        }

        private void FMFunctions_Click(object sender, EventArgs e){
            if (client.ShowFMFunctions == false){
                FmFunctions.Show();
                client.ShowFMFunctions = true;
            }
            else {
                FmFunctions.Hide();
                client.ShowFMFunctions = false;
            }
        }
        
        internal void StartScript(string target, string shopName, short x, short y, short fh, StealMode mode, ShopType shopType) {
            client.StartScript(target, shopName, x, y, fh, mode, shopType);
        }

        //Temp
        internal void WriteLine(string line){
            Console.WriteLine(line);
        }

        private void CsBtn_Click(object sender, EventArgs e) {
            Console.WriteLine("Sup not working");
            //Client.SendPacket(General.EnterCS());
        }

        private void SendSpamBtn_Click(object sender, EventArgs e) {
            if (PacketInput.Text.Length == 0) return;

            client.SendPacket(PacketInput.Text.ToByteArray());
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

        private void UpTimer_Tick(object sender, EventArgs e) {
            uptime = uptime.AddSeconds(1);
            WorkingStatus.Text = uptime.ToString("HH:mm:ss");
        }

        private void InventoryTabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (Tabs.SelectedTab == InventoryTab)
            {
                //Console.WriteLine("ALMOST DONE BRB CLASS");
                inventoryTab1.clear();
                if(client.Inventory != null)
                    inventoryTab1.updateInventory(client.Inventory);
            }
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
