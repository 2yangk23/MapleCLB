using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using MapleCLB.Forms;
using MapleCLB.MapleClient.Handlers;
using MapleCLB.MapleLib;
using MapleCLB.MapleLib.Packet;
using MapleCLB.Tools;

using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Types;
using Timer = System.Timers.Timer;

namespace MapleCLB.MapleClient {
    internal enum ClientMode : byte {
        DISCONNECTED,
        CONNECTED,
        LOGIN,
        CASHSHOP,
        GAME,
    }

    public class Client {
        
        private const int SERVER_TIMEOUT = 20000;
        private const int CHANNEL_TIMEOUT = 10000;

        /* UI Info */
        private readonly ClientForm CForm;
        internal readonly IProgress<bool> ConnectToggle;
        internal readonly IProgress<string> WriteLog;
        internal readonly IProgress<byte[]> WriteSend, WriteRecv;

        internal readonly IProgress<Mapler> UpdateMapler;
        internal readonly IProgress<byte> UpdateChannel;

        /* Client Info */
        private readonly Handshake HandshakeHandler;
        private readonly Packet PacketHandler;

        private Session Session;
        internal ScriptManager ScriptManager;
        internal ClientMode Mode;

        private IProgress<byte[]> SendPacketProgress;

        /* Timers */
        public Timer cst;
        public int autoCStime;

        public Timer ccst;
        public int autoCCtime;

        /* User Info */
        internal Account Account;
        internal Mapler Mapler;
        internal int UserId;
        internal long SessionId;

        internal byte Channel, doWhat;

        internal bool shouldCC;

        internal bool ShowInformation;

        /* Dictionaries */
        internal readonly Dictionary<int, string> UidMap = new Dictionary<int, string>(); //uid -> ign
        internal readonly MultiKeyDictionary<byte, string, int> CharMap = new MultiKeyDictionary<byte, string, int>(); //slot/ign -> uid

        internal readonly Dictionary<string, int> IgnUid = new Dictionary<string, int>();            //IGN -> UID
        internal readonly Dictionary<int, byte[]> UidMovementPacket = new Dictionary<int, byte[]>(); //UID -> MovementPacket

        internal readonly Dictionary<int, string> EquipToString;// Dictionary of ALL Data, ID -> Name
        internal readonly Dictionary<int, string> UseToString;
        internal readonly Dictionary<int, string> SetUpToString;
        internal readonly Dictionary<int, string> EtcToString;
        internal readonly Dictionary<int, string> CashToString;

        internal readonly Dictionary<string, int> currentEquipInventory = new Dictionary<string, int>(); //Dictionary of CLIENTS Data, Name -> Quantity
        internal readonly Dictionary<string, int> currentUseInventory = new Dictionary<string, int>();
        internal readonly Dictionary<string, int> currentSetUpInventory = new Dictionary<string, int>();
        internal readonly Dictionary<string, int> currentEtcInventory = new Dictionary<string, int>();

        internal Client(ClientForm form) {
            /* Initialize Form */
            CForm = form;

            ConnectToggle   = form.ConnectToggle;
            WriteLog        = form.WriteLog;
            WriteSend       = form.WriteSend;
            WriteRecv       = form.WriteRecv;
            UpdateMapler    = form.UpdateMapler;
            UpdateChannel   = form.UpdateCh;

            /* Initialize Client */
            ScriptManager = new ScriptManager(this);
            HandshakeHandler = new Handshake(this);
            PacketHandler = new Packet(this);

            autoCStime = 3600000;

            //cst = new System.Timers.Timer(autoCStime);
            //cst.Elapsed += new System.Timers.ElapsedEventHandler(AutoCS);

            ccst = new Timer(autoCStime);
            ccst.Elapsed += AutoCC;

            shouldCC = false;

            ShowInformation = false;


            EquipToString = Tools.ItemParse.Parsing_Data("Equip");
            UseToString = Tools.ItemParse.Parsing_Data("Use");
            SetUpToString = Tools.ItemParse.Parsing_Data("SetUp");
            EtcToString = Tools.ItemParse.Parsing_Data("Etc");
            CashToString = Tools.ItemParse.Parsing_Data("Cash");
        }

        // This must be called in client's thread
        internal void Initialize(Account account) {
            Account = account;

            /* Initialize Progress */
            SendPacketProgress = new Progress<byte[]>(SendBytePacket);

            /* Start Scripts */
            //ScriptManager.Get<PlayerLoader>().Start();
            //ScriptManager.Get<ChatBot>().Start();
            //ScriptManager.Get<IgnBot>().Start();

        }

        internal void Connect() {
            ConnectToggle.Report(false);
            WriteLog.Report("Connecting to " + Program.LoginIp + ":" + Program.LoginPort);
            var conn = new Connector(Program.LoginIp, Program.LoginPort);
            conn.OnConnected += OnConnected;
            conn.OnError += OnError;

            try {
                conn.Connect(SERVER_TIMEOUT);
                Mode = ClientMode.CONNECTED;
            } catch {
                WriteLog.Report("Failed to connect.");
                if (CForm.AutoRestart.Checked) {
                    Connect(); //Start connection again
                } else {
                    ConnectToggle.Report(true);
                }
            }
        }

        // TODO: Try to reuse same session
        internal void Reconnect(string ip, short port) {
            WriteLog.Report("Reconnecting to " + ip + ":" + port);
            Session.Disconnect(false);

            var newIp = IPAddress.Parse(ip);
            var conn = new Connector(newIp, port);
            conn.OnConnected += OnConnected;
            conn.OnError += OnError;

            try {
                conn.Connect(CHANNEL_TIMEOUT);
            } catch {
                WriteLog.Report("Bug Hunting 3");
                Session.Disconnect();
            }
        }

        internal void Disconnect() {
            Session.Disconnect();
        }

        public void SendPacket(byte[] packet) {
            SendPacketProgress?.Report(packet);
        }

        private void SendBytePacket(byte[] packet) {
            try {
                WriteSend.Report(packet);

                byte[] copy = new byte[packet.Length];
                Buffer.BlockCopy(packet, 0, copy, 0, packet.Length);
                Session.SendPacket(copy);
            } catch {
                WriteLog.Report("An error occured when attempting to send packet.");
            }
        }

        internal void ClearStats() {
            UpdateMapler.Report(null);
            UpdateChannel.Report(0);
        }

        /* Script Packet Funcs (Concurrent) */
        internal bool AddScriptRecv(ushort header, IProgress<PacketReader> progress) {
            return PacketHandler.RegisterHandler(header, progress);
        }

        internal void RemoveScriptRecv(ushort header) {
            PacketHandler.UnregisterHandler(header);
        }

        internal void WaitScriptRecv(ushort header, AutoResetEvent handle) {
            PacketHandler.RegisterWait(header, handle);
        }

        internal void WaitScriptRecv2(ushort header, Blocking<PacketReader> reader) {
            PacketHandler.RegisterWait(header, reader);
        }

        /* Timer Handlers */
        private void AutoCC(object sender, ElapsedEventArgs e) {
            if (doWhat == 1) {
                WriteLog.Report("Changing to Ch 2");
                shouldCC = true;
                SendBytePacket(General.ChangeChannel(0x01));
            }
        }

        //AutoCS Event (Timer)
        private void AutoCS(object sender, ElapsedEventArgs e) {
            //if (Program.Gui.aCS.Checked)
            //{
            //    WriteLog.Report(("Auto CS time!"));
            //   SendPacket(General.EnterCS());
            //    Mode = ClientMode.CASHSHOP;
            // }
        }

        /* Event Handlers */
        private void OnConnected(object o, Session s) {
            WriteLog.Report("Connected to server.");
            Session = s;
            s.OnHandshake += HandshakeHandler.Handle;
            s.OnPacket += PacketHandler.Handle;
            s.OnDisconnected += OnDisconnected;
        }

        private void OnError(object c, SocketError e) {
            WriteLog.Report("Connection error code " + e);
            ConnectToggle.Report(false);
        }

        private void OnDisconnected(object o, EventArgs e) {
            WriteLog.Report("Disconnected from server.");
            Mode = ClientMode.DISCONNECTED;
            CharMap.Clear();
            IgnUid.Clear();
            UidMovementPacket.Clear();
            ClearStats();
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
