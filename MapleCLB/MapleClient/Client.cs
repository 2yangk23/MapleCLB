using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
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
using MapleCLB.MapleClient.Scripts;

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

        public const int FM1_CRC = 0x2A7AC228;

        /* UI Info */
        private readonly ClientForm CForm;
        internal readonly IProgress<bool> ConnectToggle;
        internal readonly IProgress<string> WriteLog;
        internal readonly IProgress<byte[]> WriteSend, WriteRecv;

        internal readonly IProgress<Mapler> UpdateMapler;
        
        internal readonly IProgress<byte> UpdateChannel;
        internal readonly IProgress<long> UpdateExp;
        internal readonly IProgress<int> UpdateItems;
        internal readonly IProgress<int> UpdatePeople;
        internal readonly IProgress<String> UpdateWorking;


        /* Client Info */
        private readonly Handshake HandshakeHandler;
        private readonly Packet PacketHandler;

        private Session Session;
        internal ScriptManager ScriptManager;
        internal ClientMode Mode;

        /* Timers */
        public Timer ccst;
        public int autoCCtime;

        public Timer displayTimer;
        public int displayTime;

        /* User Info */
        internal Account Account;
        internal Mapler Mapler;
        internal int UserId;
        internal long SessionId;

        internal byte Channel, doWhat;

        internal bool shouldCC;

        internal int totalItemCount;
        internal int totalPeopleCount;

        internal bool ShowInformation = false;
        internal bool ShowFMFunctions = false;

        Stopwatch stopWatch = new Stopwatch();

        /* Dictionaries */
        internal readonly Dictionary<int, string> UidMap = new Dictionary<int, string>(); //uid -> ign
        internal readonly MultiKeyDictionary<byte, string, int> CharMap = new MultiKeyDictionary<byte, string, int>(); //slot/ign -> uid

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
            UpdateExp       = form.UpdateExp;
            UpdateItems     = form.UpdateItems;
            UpdatePeople    = form.UpdatePeople;
            UpdateWorking   = form.UpdateWorking;

            /* Initialize Client */
            ScriptManager = new ScriptManager(this);
            HandshakeHandler = new Handshake(this);
            PacketHandler = new Packet(this);

            autoCCtime = 300000; //5 Minutes
            displayTime = 60;

            totalItemCount = 0;
            totalPeopleCount = 0;

            ccst = new Timer(autoCCtime);
            ccst.Elapsed += AutoCC;

            displayTimer = new Timer(displayTime);
            displayTimer.Elapsed += ConnectTimer;

            shouldCC = false;

            EquipToString = Tools.ItemParse.Parsing_Data("Equip");
            UseToString = Tools.ItemParse.Parsing_Data("Use");
            SetUpToString = Tools.ItemParse.Parsing_Data("SetUp");
            EtcToString = Tools.ItemParse.Parsing_Data("Etc");
            CashToString = Tools.ItemParse.Parsing_Data("Cash");
        }

        // This must be called in client's thread
        internal void Initialize(Account account) {
            Account = account;

            /* Start Scripts */
            //ScriptManager.Get<PlayerLoader>().Start();
            //ScriptManager.Get<ChatBot>().Start();
            //ScriptManager.Get<IgnBot>().Start();
            //ScriptManager.Get<SpotStealerBot>().Start();
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

        internal void SendPacket(byte[] packet) {
            try {
                if (CForm.IsLogSend) {
                    byte[] copy = new byte[packet.Length];
                    Buffer.BlockCopy(packet, 0, copy, 0, packet.Length);
                    WriteSend.Report(copy);
                }

                Session.SendPacket(packet);
            } catch {
                WriteLog.Report("An error occured when attempting to send packet.");
            }
        }

        internal void SendPacket(PacketWriter w) {
            try {
                WriteSend.Report(w.GetBuffer());
                Session.SendPacket(w.ToArray());
            } catch {
                WriteLog.Report("An error occured when attempting to send packet.");
            }
        }

        internal void ClearStats() {
            UpdateMapler.Report(null);
            UpdateChannel.Report(0);
            stopWatch.Stop();
            stopWatch.Reset();
            totalPeopleCount = 0;
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
            if (doWhat == 1){
                WriteLog.Report("Disconnecting 5 Minute Test!");
                Disconnect();
                Thread.Sleep(10000);
                Connect();
            }
        }
        private void ConnectTimer(object sender, ElapsedEventArgs e){
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",ts.Hours, ts.Minutes, ts.Seconds);
            UpdateWorking.Report(elapsedTime);
        }
        internal void StartWatch() {
            stopWatch.Start();
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
            ClearStats();
            ccst.Enabled = false;
            displayTimer.Enabled = false;
            if (CForm.AutoRestart.Checked) {
                Connect();  //Start connection again
            } else {
                ConnectToggle.Report(true);
            }
        }
    }
}
