using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using MapleCLB.Forms;
using MapleCLB.MapleClient.Handlers;
using MapleCLB.MapleClient.Scripts;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Tools;
using MapleCLB.Types;
using MapleLib;
using MapleLib.Packet;
using Timer = System.Timers.Timer;

namespace MapleCLB.MapleClient {
    internal enum ClientMode : byte {
        DISCONNECTED,
        CONNECTED,
        LOGIN,
        CASHSHOP,
        GAME
    }

    public class Client {
        private const int SERVER_TIMEOUT = 20000;
        private const int CHANNEL_TIMEOUT = 10000;

        /* UI Info */
        private readonly ClientForm cForm;

        internal readonly MultiKeyDictionary<byte, string, int> CharMap = new MultiKeyDictionary<byte, string, int>();
        //slot/ign -> uid

        internal readonly IProgress<bool> ConnectToggle;

        internal readonly Dictionary<string, int> currentEquipInventory = new Dictionary<string, int>();
        //Dictionary of CLIENTS Data, Name -> Quantity

        internal readonly Dictionary<string, int> currentEtcInventory = new Dictionary<string, int>();
        internal readonly Dictionary<string, int> currentSetUpInventory = new Dictionary<string, int>();
        internal readonly Dictionary<string, int> currentUseInventory = new Dictionary<string, int>();

        /* Client Info */
        private readonly Handshake handshakeHandler;
        private readonly Packet packetHandler;

        private readonly Stopwatch stopWatch = new Stopwatch();

        /* Dictionaries */
        internal readonly Dictionary<int, string> UidMap = new Dictionary<int, string>(); //uid -> ign

        internal readonly IProgress<byte> UpdateChannel;
        internal readonly IProgress<long> UpdateExp;
        internal readonly IProgress<int> UpdateItems;

        internal readonly IProgress<Mapler> UpdateMapler;
        internal readonly IProgress<int> UpdatePeople;
        internal readonly IProgress<string> UpdateWorking;
        internal readonly IProgress<string> WriteLog;
        internal readonly IProgress<byte[]> WriteSend, WriteRecv;

        /* User Info */
        internal Account Account;
        public int autoDCtime;

        internal byte Channel, doWhat;

        /* Timers */
        public Timer dcst;
        public int displayTime;

        public Timer displayTimer;

        internal bool hasFMShop = false;
        internal Mapler Mapler;

        internal IProgress<List<PortalInfo>> MapRush;
        internal ClientMode Mode;
        internal byte PortalCount = 1;

        internal int PortalCrc;
        internal ScriptManager ScriptManager;

        private Session session;
        internal long SessionId;
        internal bool ShowFMFunctions = false;

        internal bool ShowInformation = false;

        internal int totalItemCount;
        internal int totalPeopleCount;
        internal int UserId;

        internal Client(ClientForm form) {
            /* Initialize Form */
            cForm = form;

            ConnectToggle = form.ConnectToggle;
            WriteLog = form.WriteLog;
            WriteSend = form.WriteSend;
            WriteRecv = form.WriteRecv;
            UpdateMapler = form.UpdateMapler;
            UpdateChannel = form.UpdateCh;
            UpdateExp = form.UpdateExp;
            UpdateItems = form.UpdateItems;
            UpdatePeople = form.UpdatePeople;
            UpdateWorking = form.UpdateWorking;

            /* Initialize Client */
            ScriptManager = new ScriptManager(this);
            handshakeHandler = new Handshake(this);
            packetHandler = new Packet(this);

            autoDCtime = 1800000; //30 minutes
            displayTime = 60; //1 Second

            totalItemCount = 0;
            totalPeopleCount = 0;

            dcst = new Timer(autoDCtime);
            dcst.Elapsed += AutoDCForFMShop;

            displayTimer = new Timer(displayTime);
            displayTimer.Elapsed += ConnectTimer;
        }

        // This must be called in client's thread
        internal void Initialize(Account account) {
            Account = account;

            MapRush = new Progress<List<PortalInfo>>(list => {
                if (list == null) {
                    return;
                }
                foreach (var data in list) {
                    SendPacket(Portal.Enter(PortalCount, PortalCrc, data));
                    if (PortalCount++ == 255) {
                        PortalCount = 1; // wrap around
                    }
                    Thread.Sleep(50); // small delay just in case
                }
            });

            /* Start Scripts */
            //ScriptManager.Get<PlayerLoader>().Start();
            //ScriptManager.Get<ChatBot>().Start();
            //ScriptManager.Get<IgnBot>().Start();
            //ScriptManager.Get<SpotStealerBot>().Start();
            //ScriptManager.Get<MesoVac>().Start();
        }

        internal void StartScript(string IGN, string shopNAME, string FH, string X, string Y, bool PermitCB, bool SCMode,
                                  bool takeAnyCB) {
            //change to w.e you wanted? not exactly sure.. :D
            ScriptManager.Get<SpotStealerBot>().Ign = IGN;
            ScriptManager.Get<SpotStealerBot>().ShopName = shopNAME;
            ScriptManager.Get<SpotStealerBot>().Fh = FH;
            ScriptManager.Get<SpotStealerBot>().X = X;
            ScriptManager.Get<SpotStealerBot>().Y = Y;
            ScriptManager.Get<SpotStealerBot>().PermitCb = PermitCB;
            ScriptManager.Get<SpotStealerBot>().ScMode = SCMode;
            ScriptManager.Get<SpotStealerBot>().TakeAnyCb = takeAnyCB;
            //SpotStealerBot script = get<SpotStealerBot>(); ???
            ScriptManager.Get<SpotStealerBot>().Start();
        }

        internal void ClearStats() {
            UpdateMapler.Report(null);
            UpdateChannel.Report(0);
            stopWatch.Stop();
            stopWatch.Reset();
            currentEquipInventory.Clear();
            currentUseInventory.Clear();
            currentSetUpInventory.Clear();
            currentEtcInventory.Clear();
            totalPeopleCount = 0;
        }

        #region Client Connection
        internal void Connect() {
            ConnectToggle.Report(false);
            WriteLog.Report("Connecting to " + Program.LoginIp + ":" + Program.LoginPort);
            var conn = new Connector(Program.LoginIp, Program.LoginPort, Program.AesCipher);
            conn.OnConnected += OnConnected;
            conn.OnError += OnError;

            try {
                conn.Connect(SERVER_TIMEOUT);
                Mode = ClientMode.CONNECTED;
            } catch {
                WriteLog.Report("Failed to connect.");
                if (cForm.AutoRestart.Checked) {
                    Connect(); //Start connection again
                } else {
                    ConnectToggle.Report(true);
                }
            }
        }

        // TODO: Try to reuse same session
        internal void Reconnect(string ip, short port) {
            WriteLog.Report("Reconnecting to " + ip + ":" + port);
            session.Disconnect(false);

            var newIp = IPAddress.Parse(ip);
            var conn = new Connector(newIp, port, Program.AesCipher);
            conn.OnConnected += OnConnected;
            conn.OnError += OnError;

            try {
                conn.Connect(CHANNEL_TIMEOUT);
            } catch {
                WriteLog.Report("Bug Hunting 3");
                session.Disconnect();
            }
        }

        internal void Disconnect() {
            //Session.Disconnect();
            SendPacket(General.ExitCS()); //Temp -.-
        }

        internal void SendPacket(byte[] packet) {
            try {
                if (cForm.IsLogSend) {
                    byte[] copy = new byte[packet.Length];
                    Buffer.BlockCopy(packet, 0, copy, 0, packet.Length);
                    WriteSend.Report(copy);
                }

                session.SendPacket(packet);
            } catch {
                WriteLog.Report("An error occured when attempting to send packet.");
            }
        }

        internal void SendPacket(PacketWriter w) {
            try {
                WriteSend.Report(w.Buffer);
                session.SendPacket(w.ToArray());
            } catch {
                WriteLog.Report("An error occured when attempting to send packet.");
            }
        }
        #endregion

        #region Timer Handlers
        private void AutoDCForFMShop(object sender, ElapsedEventArgs e) {
            if (doWhat == 1 && hasFMShop == false) {
                WriteLog.Report("Disconnecting 5 Minute Test!");
                Disconnect();
                Connect();
            }
        }

        private void ConnectTimer(object sender, ElapsedEventArgs e) {
            var ts = stopWatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}";
            UpdateWorking.Report(elapsedTime);
        }

        internal void StartWatch() {
            stopWatch.Start();
        }
        #endregion

        #region Script Packet Funcs (Concurrent)
        internal bool AddScriptRecv(ushort header, IProgress<PacketReader> progress) {
            return packetHandler.RegisterHandler(header, progress);
        }

        internal void RemoveScriptRecv(ushort header) {
            packetHandler.UnregisterHandler(header);
        }

        internal void WaitScriptRecv(ushort header, AutoResetEvent handle) {
            packetHandler.RegisterWait(header, handle);
        }

        internal void WaitScriptRecv2(ushort header, Blocking<PacketReader> reader) {
            packetHandler.RegisterWait(header, reader);
        }
        #endregion

        #region Event Handlers
        private void OnConnected(object o, Session s) {
            WriteLog.Report("Connected to server.");
            session = s;
            s.OnHandshake += handshakeHandler.Handle;
            s.OnPacket += packetHandler.Handle;
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
            dcst.Enabled = false;
            displayTimer.Enabled = false;
            if (cForm.AutoRestart.Checked) {
                Connect(); //Start connection again
            } else {
                ConnectToggle.Report(true);
            }
        }
        #endregion
    }
}
