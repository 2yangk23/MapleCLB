using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Timers;
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

    public class Client {
        private readonly ClientForm CForm;
        public readonly IProgress<bool> ConnectToggle;
        public readonly IProgress<string> WriteLog;
        public readonly IProgress<string> WritePacketLog;
        public readonly IProgress<string> UpdateName;



        public Session Session;

        private readonly Handlers.Handshake HandshakeHandler;
        private readonly Handlers.Packet PacketHandler;

        internal ClientMode Mode { get; set; }

        /* Settings */
        public int ServerTimeout, ChannelTimeout;
        public Timer cst;
        public int autoCStime;

        public Timer ccst;
        public int autoCCtime;


        /* Login Info */
        internal string User, Pass, Pic, Name;
        internal byte Select, World, Channel, doWhat;
        internal int UserId, MapId;
        internal long SessionId;

        public bool shouldCC;

        /* Dictionaries */  
        public Dictionary<int, string> UidMap; //uid -> ign
        public MultiKeyDictionary<byte, string, int> CharMap; //slot/ign -> uid

        public Dictionary<string, int> IgnUid;        //IGN -> UID
        public Dictionary<int, string> UidMovementPacket; //UID -> MovementPacket

        public Client(ClientForm form) {
            CForm = form;

            ConnectToggle = form.ConnectToggle;
            WriteLog = form.WriteLog;
            WritePacketLog = form.WritePacketLog;
            UpdateName = form.UpdateName;

            Mode = ClientMode.DISCONNECTED;
            var conn = new Connector(Program.LoginIp, Program.LoginPort);
            HandshakeHandler = new Handlers.Handshake(this);
            PacketHandler = new Handlers.Packet(this);

            conn.OnConnected += OnConnected;
            conn.OnError += OnError;

            ServerTimeout = 40000;
            ChannelTimeout = 12000;
            autoCStime = 3600000;

            //cst = new System.Timers.Timer(autoCStime);
            //cst.Elapsed += new System.Timers.ElapsedEventHandler(AutoCS);

            ccst = new Timer(autoCStime);
            ccst.Elapsed += AutoCC;

            shouldCC = false;

            UidMap = new Dictionary<int, string>();
            CharMap = new MultiKeyDictionary<byte, string, int>();
            IgnUid = new Dictionary<string, int>();
            UidMovementPacket = new Dictionary<int, string>();  
        }

        public void Connect() {
            ConnectToggle.Report(false);
            WriteLog.Report("Connecting to " + Program.LoginIp + ":" + Program.LoginPort);
            var conn = new Connector(Program.LoginIp, Program.LoginPort);
            conn.OnConnected += OnConnected;
            conn.OnError += OnError;

            try {
                conn.Connect(ServerTimeout);
                Mode = ClientMode.CONNECTED;
            } catch (Exception) {
                WriteLog.Report("Failed to connect.");
                if (CForm.AutoRestart.Checked) {
                    Connect(); //Start connection again
                } else {
                    ConnectToggle.Report(true);
                }
            }
        }

        // TODO: Try to reuse same session
        public void Reconnect(string ip, short port) {
            WriteLog.Report("Reconnecting to " + ip + ":" + port);
            Session.Disconnect(false);

            var newIp = IPAddress.Parse(ip);
            var conn = new Connector(newIp, port);
            conn.OnConnected += OnConnected;
            conn.OnError += OnError;

            try {
                conn.Connect(ChannelTimeout);
            } catch (Exception) {
                WriteLog.Report("Bug Hunting 3");
                Session.Disconnect();
            }
        }

        public void SendPacket(byte[] packet) {
            try {
                Session.SendPacket(packet);
            } catch (Exception) {
                WriteLog.Report("An error occured when attempting to send packet.");
            }
        }

        /* Event Handlers */
        void OnConnected(object o, Session s) {
            WriteLog.Report(("Connected to server."));
            Session = s;
            s.OnHandshake += HandshakeHandler.Handle;
            s.OnPacket += PacketHandler.Handle;
            s.OnDisconnected += OnDisconnected;
        }

        void OnError(object c, SocketError e) {
            WriteLog.Report(("Connection error code " + e));
            ConnectToggle.Report(false);
        }

        void AutoCC(object sender, ElapsedEventArgs e)
        {
            if (doWhat == 1)
            {
                WriteLog.Report("Changing to Ch 2");
                shouldCC = true;
                SendPacket(General.ChangeChannel(0x01));
            }
        }


        void AutoCS(object sender, ElapsedEventArgs e) //AutoCS Event (Timer)
        {
            //if (Program.Gui.aCS.Checked)
            //{
            //    WriteLog.Report(("Auto CS time!"));
            //   SendPacket(General.EnterCS());
            //    Mode = ClientMode.CASHSHOP;
            // }
        }


        public void OnDisconnected(object o, EventArgs e) {
            WriteLog.Report(("Disconnected from server."));
            Mode = ClientMode.DISCONNECTED;
            CharMap.Clear();
            IgnUid.Clear();
            UidMovementPacket.Clear();
            //cst.Enabled = false;
            ccst.Enabled = false;
            if (CForm.AutoRestart.Checked) {
                Connect();  //Start connection again
            } else {
                ConnectToggle.Report(true);
            }
        }
    }
}
