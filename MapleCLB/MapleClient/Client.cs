using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MapleCLB.MapleLib;
using MapleCLB.Tools;

using MapleCLB.Packets.Send;

namespace MapleCLB.MapleClient {
    enum ClientMode {
        DISCONNECTED,
        CONNECTED,
        LOGIN,
        CASHSHOP,
        GAME,
    }

    //Kelvin smellssssssssss

    public class Client {
        private readonly Connector Conn;
        public Session Session;

        private readonly Handlers.Handshake HandshakeHandler;
        private readonly Handlers.Packet PacketHandler;

        internal ClientMode Mode { get; set; }

        /* Settings */
        public int ServerTimeout, ChannelTimeout;
        public System.Timers.Timer cst;
        public int autoCStime;

        public System.Timers.Timer ccst;
        public int autoCCtime;


        /* Login Info */
        internal string User, Pass, Pic, Ign;
        internal byte World, Channel, doWhat;
        internal int UserId, MapId;
        internal long SessionId;

        public Boolean shouldCC;


        /* Dictionaries */  
        public Dictionary<int, string> UidMap; //uid -> ign
        public MultiKeyDictionary<byte, string, int> CharMap; //slot/ign -> uid

        public Dictionary<string, int> IgnUid;        //IGN -> UID
        public Dictionary<int, string> UidMovementPacket; //UID -> MovementPacket



        public Client() {
            Mode = ClientMode.DISCONNECTED;
            Conn = new Connector(Program.LoginIp, Program.LoginPort);
            HandshakeHandler = new Handlers.Handshake(this);
            PacketHandler = new Handlers.Packet(this);

            Conn.OnConnected += OnConnected;
            Conn.OnError += OnError;

            ServerTimeout = 40000;
            ChannelTimeout = 12000;
            autoCStime = 1000000;

            //cst = new System.Timers.Timer(autoCStime);
           // cst.Elapsed += new System.Timers.ElapsedEventHandler(AutoCS);

            ccst = new System.Timers.Timer(autoCStime);
            ccst.Elapsed += new System.Timers.ElapsedEventHandler(AutoCC);

            shouldCC = false;

            UidMap = new Dictionary<int, string>();
            CharMap = new MultiKeyDictionary<byte, string, int>();
            IgnUid = new Dictionary<string, int>();
            UidMovementPacket = new Dictionary<int, string>();  
        }

        public void Connect() { 
            Program.Gui.BeginInvoke((MethodInvoker)delegate {
                Program.Gui.connect.Enabled = false;
                Program.Gui.disconnect.Enabled = false;
                if (doWhat == 1) {Program.Gui.aCS.Checked = true; Program.Gui.aCS.Enabled = false; }//if ShopBot Mode enaable auto CS and lock checkbox  
            });
            Program.WriteLog(("Connecting to " + Program.LoginIp + ":" + Program.LoginPort));
            Connector conn = new Connector(Program.LoginIp, Program.LoginPort);
            conn.OnConnected += OnConnected;
            conn.OnError += OnError;
            try {
                conn.Connect(ServerTimeout);
                Mode = ClientMode.CONNECTED;
            } catch (Exception) {
                Program.WriteLog(("Failed to connect."));
                if (Program.Gui.aRestart.Checked) {
                    Connect();  //Start connection again
                } else {
                    Program.Gui.BeginInvoke((MethodInvoker)delegate { Program.Gui.connect.Enabled = true; });
                }
            }
        }

        public void Reconnect(string ip, short port) {

            Program.WriteLog(("Reconnecting to " + ip + ":" + port));
            Session.Disconnect(false);

            IPAddress NewIP = IPAddress.Parse(ip);

            Connector conn = new Connector(NewIP, port);
            conn.OnConnected += new EventHandler<Session>(OnConnected);
            conn.OnError += new EventHandler<SocketError>(OnError);

           // Connector conn = new Connector(IPAddress.Loopback, port);
            //conn.OnConnected += OnConnected;
            //conn.OnError += OnError;
            try {
                conn.Connect(ChannelTimeout);
            } catch (Exception) {
                Program.WriteLog("Bug Hunting 3");
                Session.Disconnect();
            }
        }

        public void SendPacket(byte[] packet) //Send packet by hex string
        {
            try {
                Session.SendPacket(packet);
            } catch (Exception) {
                Program.WriteLog("An error occured when attempting to send packet.");
            }
        }

        /* Event Handlers */
        void OnConnected(object o, Session s) {
            Program.WriteLog(("Connected to server."));
            Session = s;
            s.OnHandshake += HandshakeHandler.Handle;
            s.OnPacket += PacketHandler.Handle;
            s.OnDisconnected += OnDisconnected;
            Program.Gui.BeginInvoke((MethodInvoker)delegate { Program.Gui.disconnect.Enabled = true; });
        }

        void OnError(object c, SocketError e) {
            Program.WriteLog(("Connection error code " + e));
            Program.Gui.BeginInvoke((MethodInvoker)delegate {
                Program.Gui.connect.Enabled = true;
                Program.Gui.disconnect.Enabled = false;
            });
        }

        void AutoCC(Object sender, System.Timers.ElapsedEventArgs e)
        {
            if (doWhat == 1)
            {
                Program.WriteLog("Changing to Ch 2");
                shouldCC = true;
                SendPacket(General.ChangeChannel(0x01));
            }
        }

        void AutoCS(Object sender, System.Timers.ElapsedEventArgs e) //AutoCS Event (Timer)
        {
            //if (Program.Gui.aCS.Checked)
            //{
            //    Program.WriteLog(("Auto CS time!"));
            //   SendPacket(General.EnterCS());
            //    Mode = ClientMode.CASHSHOP;
            // }
        }


        public void OnDisconnected(object o, EventArgs e) {
                Program.WriteLog(("Disconnected from server."));
                Mode = ClientMode.DISCONNECTED;
                CharMap.Clear();
                IgnUid.Clear();
                UidMovementPacket.Clear();
                //cst.Enabled = false;
                if (Program.Gui.aRestart.Checked)
                {
                    Connect();  //Start connection again
                }
                else
                {
                    Program.Gui.BeginInvoke((MethodInvoker)delegate
                    {
                        Program.Gui.disconnect.Enabled = false;
                        Program.Gui.connect.Enabled = true;
                    });
                }
        }
    }
}
