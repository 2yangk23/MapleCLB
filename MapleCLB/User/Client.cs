using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using MapleCLB.MaplePacketLib;
using MapleCLB.Packets;
using MapleCLB.Packets.Function;
using MapleCLB.Tools;
using MaplePacketLib;

namespace MapleCLB.User
{
    enum ClientMode
    {
        DISCONNECTED,
        CONNECTED,
        LOGIN,
        CASHSHOP,
        GAME,
    }

    public class Client
    {
        public Session session;
        private ClientMode cmode = ClientMode.DISCONNECTED;
        private Auth a;
        private PacketHandler h;
        private System.Timers.Timer cst;

        /* Settings */
        public int serverTimeout, channelTimeout;

        /* Login Info */
        public string user, pass, pic, ign; //ign = slot/ign/uid depending on settings
        public byte world, channel;
        public int uid, mapID;
        public long sessionID;
        public string authCode;

        /* Dictionaries */
        public Dictionary<int, string> uidMap; //uid -> ign
        public MultiKeyDictionary<byte, string, int> charMap; //slot/ign -> uid

        public Client()
        {
            Random RNG = new Random();
            Program.hwid1 = RNG.Next(0, Int32.MaxValue);
            Program.hwid2 = RNG.Next(0, Int16.MaxValue);
            a = new Auth();
            h = new PacketHandler();
            cst = new System.Timers.Timer(120000);
            cst.Elapsed += new System.Timers.ElapsedEventHandler(AutoCS);

            serverTimeout = 1000;
            channelTimeout = 10000;

            uidMap = new Dictionary<int, string>();
            charMap = new MultiKeyDictionary<byte, string, int>();
            authCode = "";

            // Register packet handlers
            //h.RegisterHeader(0x00, new ResetAuth()); TODO: FIX AUTH RESET
            h.RegisterHeader(RecvOps.LOGIN_SECOND, new LoginSecond());
            h.RegisterHeader(RecvOps.CHARLIST, new SelectCharacter());
            h.RegisterHeader(RecvOps.SERVER_IP, new ServerIP());
            h.RegisterHeader(RecvOps.CHANNEL_IP, new ChannelIP());
            h.RegisterHeader(RecvOps.PING, new PingPong());

            h.RegisterHeader(0x1C, new WhoKnows());

            h.RegisterHeader(RecvOps.SPAWN_PLAYER, new SpawnPlayer());
            h.RegisterHeader(RecvOps.REMOVE_PLAYER, new RemovePlayer());
        }

        public void Connect() //TODO: Initial connect always results in a disconnect, BUG
        {
            Program.gui.BeginInvoke((MethodInvoker)delegate
            {
                Program.gui.connect.Enabled = false;
                Program.gui.disconnect.Enabled = false;
            });
            Program.WriteLog(("Connecting to " + Program.loginIP + ":" + Program.loginPort));
            Connector conn = new Connector(Program.loginIP, Program.loginPort, Program.cipher);
            conn.OnConnected += new EventHandler<Session>(OnConnected);
            conn.OnError += new EventHandler<SocketError>(OnError);
            try
            {
                conn.Connect(serverTimeout);
                cmode = ClientMode.CONNECTED;
            }
            catch (Exception)
            {
                Program.WriteLog(("Failed to connect."));
                if (Program.gui.aRestart.Checked)
                    Connect();  //Start connection again
                else
                    Program.gui.BeginInvoke((MethodInvoker)delegate { Program.gui.connect.Enabled = true; });
            }
        }

        public void Reconnect(string ip, short port)
        {
            Program.WriteLog(("Reconnecting to " + ip + ":" + port));
            session.Disconnect(false);
            Connector conn = new Connector(ip, port, Program.cipher);
            conn.OnConnected += new EventHandler<Session>(OnConnected);
            conn.OnError += new EventHandler<SocketError>(OnError);
            try {
                conn.Connect(channelTimeout);
            }
            catch (Exception) {
                session.Disconnect();
            }
        }

        public void SendPacket(PacketWriter packet) //Send packet by PacketWriter
        {
            try {
                session.SendPacket(packet);
            }
            catch (Exception) {
                Program.WriteLog(("An error occured when attempting to send packet."));
            }
        }

        public void SendPacket(string packet) //Send packet by hex string
        {
            PacketWriter pw = new PacketWriter();
            pw.WriteHexString(HexEncoding.fillRandom(packet));

            SendPacket(pw);
        }

        public void AutoCS() //AutoCS function
        {
            SendPacket(General.EnterCS());
            cmode = ClientMode.CASHSHOP;
        }

        /* Event Handlers */
        void AutoCS(Object sender, System.Timers.ElapsedEventArgs e) //AutoCS Event (Timer)
        {
            if (Program.gui.aCS.Checked)
            {
                SendPacket(General.EnterCS());
                cmode = ClientMode.CASHSHOP;
            }
        }

        void OnConnected(object o, Session s)
        {
            Program.WriteLog(("Connected to server."));
            session = s;
            s.OnHandshake += new EventHandler<ServerInfo>(OnHandshake);
            s.OnPacket += new EventHandler<byte[]>(OnPacket);
            s.OnDisconnected += new EventHandler(OnDisconnected);
            Program.gui.BeginInvoke((MethodInvoker)delegate { Program.gui.disconnect.Enabled = true; });
        }

        void OnError(object c, SocketError e)
        {
            Program.WriteLog(("Connection error code " + e));
            Program.gui.BeginInvoke((MethodInvoker)delegate
            {
                Program.gui.connect.Enabled = true;
                Program.gui.disconnect.Enabled = false;
            });
        }

        public void OnDisconnected(object o, EventArgs e)
        {
            Program.WriteLog(("Disconnected from server."));
            cmode = ClientMode.DISCONNECTED;
            charMap.Clear();
            cst.Enabled = false;
            if (Program.gui.aRestart.Checked) {
                Connect();  //Start connection again
            }
            else
            {
                Program.gui.BeginInvoke((MethodInvoker)delegate
                {
                    Program.gui.disconnect.Enabled = false;
                    Program.gui.connect.Enabled = true;
                });
            }
        }

        public void OnHandshake(object o, ServerInfo i)
        {
            switch (cmode)
            {
                case ClientMode.CONNECTED: //if handshake for login hasn't happened yet
                    Program.WriteLog(("Validating login for MapleStory v" + i.Version + "." + i.Subversion));
                    SendPacket(Login.Validate(i.Version, Int16.Parse(i.Subversion)));
                    SendPacket("66 00 08"); //Tell server client is ready (Add to Ops)
                    cmode = ClientMode.LOGIN;

                    if (authCode == "")
                    {
                        new Thread(delegate()
                        {
                            Thread.CurrentThread.IsBackground = true; //cause it to close with program
                            
                            Program.WriteLog(("Fetching login cookie..."));
                            authCode = a.loginAuth(user, pass); //get auth code from website
                            Thread.Sleep(30000); //sleep for long time because who cares it doesnt work
                            /*PacketWriter pw = new PacketWriter(0x68);
                            pw.WriteByte(1);
                            SendPacket(pw);
                            SendPacket(new PacketWriter(0x76));
                            SendPacket(new PacketWriter(0x96));*/
                            if (authCode != "error")
                            {
                                Program.WriteLog(("Selecting world and channel..."));
                                SendPacket(Login.SelectServer(authCode, world, channel));
                            }
                            else
                            {
                                session.Disconnect();
                            }
                        }).Start();
                    }
                    else
                    {
                        SendPacket(Login.SelectServer(authCode, world, channel));
                    }
                    break;

                case ClientMode.LOGIN: //char select -> channel
                    Program.WriteLog(("Logged in!"));
                    SendPacket(Login.EnterServer(world, uid, sessionID));
                    cst.Enabled = true;
                    cmode = ClientMode.GAME;
                    break;

                case ClientMode.GAME: //channel -> channel | CS -> channel
                    SendPacket(Login.EnterServer(world, uid, sessionID));
                    break;

                //If client is in CASHSHOP mode it will auto leave CS
                case ClientMode.CASHSHOP: //channel -> CS
                    SendPacket(Login.EnterServer(world, uid, sessionID));
                    SendPacket(General.ExitCS());
                    cmode = ClientMode.GAME;
                    break;
            }
        }

        public void OnPacket(object o, byte[] packet)
        {
            h.Handle(this, new PacketReader(packet));
        }
    }
}
