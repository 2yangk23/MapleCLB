﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using MapleCLB.Forms;
using MapleCLB.MapleClient.Handlers;
using MapleCLB.MapleClient.Scripts;
using MapleCLB.Packets.Send;
using MapleCLB.ScriptLib;
using MapleCLB.Types;
using MapleCLB.Types.Items;
using MapleCLB.Types.Map;
using MapleLib;
using MapleLib.Packet;
using SharedTools;
using Timer = System.Timers.Timer;

namespace MapleCLB.MapleClient {
    internal enum ClientState : byte {
        DISCONNECTED,
        LOGIN,
        CASHSHOP,
        GAME
    }

    public class Client {
        private const int SERVER_TIMEOUT = 20000;
        private const int CHANNEL_TIMEOUT = 10000;

        /* UI Info */
        private readonly ClientForm cForm;

        internal readonly IProgress<byte> UpdateChannel;
        internal readonly IProgress<long> UpdateExp;
        internal readonly IProgress<int> UpdateItems;
        internal readonly IProgress<long> UpdateMesos;
        internal readonly IProgress<Mapler> UpdateMapler;
        internal readonly IProgress<int> UpdatePeople;
        internal readonly IProgress<string> Log;

        /* Client Info */
        public readonly ScriptManager ScriptManager;
        private readonly Handshake handshakeHandler;
        private readonly Packet packetHandler;
        private Session session;

        internal ClientState State;
        internal long SessionId;

        /* Map Info */
        internal readonly ConcurrentDictionary<int, Player> UidMap; 
        internal readonly ConcurrentDictionary<int, Reactor> ReactorMap;
        internal readonly ConcurrentDictionary<int, Monster> MonsterMap;

        /* User Info */
        internal Account Account;
        internal Mapler Mapler;
        internal Inventory Inventory;

        internal int UserId;
        internal byte Channel;

        internal IProgress<List<PortalInfo>> MapRush;
        internal byte PortalCount = 1;
        internal int PortalCrc;

        /* Random stuff */
        public Timer dcst = new Timer(30 * 60 * 1000); // 30 minutes
        internal byte doWhat;

        internal bool hasFMShop = false;
        internal bool ShowFMFunctions = false;
        internal bool ShowInformation = false;

        internal int totalItemCount = 0;
        internal int totalPeopleCount;

        internal Client(ClientForm form) {
            /* Initialize Form */
            cForm = form;

            Log = form.WriteLog;
            UpdateMapler = form.UpdateMapler;
            UpdateChannel = form.UpdateCh;
            UpdateExp = form.UpdateExp;
            UpdateMesos = form.UpdateMesos;
            UpdateItems = form.UpdateItems;
            UpdatePeople = form.UpdatePeople;

            /* Initialize Client */
            UidMap = new ConcurrentDictionary<int, Player>();
            ReactorMap = new ConcurrentDictionary<int, Reactor>();
            MonsterMap = new ConcurrentDictionary<int, Monster>();

            ScriptManager = new ScriptManager(this);
            handshakeHandler = new Handshake(this);
            packetHandler = new Packet(this, cForm.WriteRecv);

            dcst.Elapsed += AutoDCForFMShop;
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
                    Thread.Sleep(30); // small delay just in case
                }
            });

            /* Start Scripts */
            //ScriptManager.Get<PlayerLoader>().Start();
            //ScriptManager.Get<ChatBot>().Start();
            //ScriptManager.Get<IgnBot>().Start();
            //ScriptManager.Get<SpotStealerBot>().Start();
            ScriptManager.Get<MesoVac>().Start();
        }

        //TODO: This shouldn't be in client
        internal void StartScript(string target, string shopName, short x, short y, short fh, StealMode mode, ShopType shopType) {
            //change to w.e you wanted? not exactly sure.. :D
            var script = ScriptManager.Get<SpotStealer>();
            script.Target = target;
            script.ShopName = shopName;
            script.X = x;
            script.Y = y;
            script.Fh = fh;
            script.Mode = mode;
            script.Type = shopType;
            ScriptManager.Get<SpotStealer>().Start();
        }

        internal void ClearStats() {
            UpdateMapler.Report(null);
            UpdateChannel.Report(0);
            UpdateMesos.Report(-1);
            Inventory?.Clear();
            totalPeopleCount = 0;
        }

        #region Client Connection
        internal void Connect() {
            Log.Report("Connecting to " + Program.LoginIp + ":" + Program.LoginPort);

            cForm.ConnectToggle.Report(false);
            try {
                var connector = new Connector(Program.LoginIp, Program.LoginPort, Program.AesCipher);
                connector.OnConnected += OnConnected;
                connector.OnError += OnError;

                connector.Connect(SERVER_TIMEOUT);
            } catch {
                Log.Report("Failed to connect.");
                if (cForm.AutoRestart.Checked) {
                    Connect(); //Start connection again
                } else {
                    cForm.ConnectToggle.Report(true);
                }
            }
        }

        internal void Reconnect(string ip, short port) {
            Log.Report("Reconnecting to " + ip + ":" + port);

            session.Disconnect(false);
            try {
                var connector = new Connector(IPAddress.Parse(ip), port, Program.AesCipher);
                connector.OnConnected += OnConnected;
                connector.OnError += OnError;

                connector.Connect(CHANNEL_TIMEOUT);
                //session.Reconnect(IPAddress.Parse(ip), port, CHANNEL_TIMEOUT);
            } catch {
                Log.Report("Bug Hunting 3");
                session.Disconnect();
            }
        }

        internal void Disconnect() {
            session.Disconnect();
        }

        public void WriteLog(string message) {
            Log.Report(message);
        }

        public void SendPacket(byte[] packet) {
            try {
                if (cForm.IsLogSend) {
                    byte[] copy = new byte[packet.Length];
                    Buffer.BlockCopy(packet, 0, copy, 0, packet.Length);
                    cForm.WriteSend.Report(copy);
                }

                session.SendPacket(packet);
            } catch {
                Log.Report("An error occured when attempting to send packet.");
            }
        }

        public void SendPacket(PacketWriter w) {
            try {
                cForm.WriteSend.Report(w.Buffer);
                session.SendPacket(w.ToArray());
            } catch {
                Log.Report("An error occured when attempting to send packet.");
            }
        }
        #endregion

        #region Script Packet Funcs (Concurrent)
        public bool AddScriptRecv(ushort header, Action<Client, PacketReader> handler) {
            return packetHandler.RegisterHandler(header, handler);
        }

        public void RemoveScriptRecv(ushort header) {
            packetHandler.UnregisterHandler(header);
        }

        public void WaitScriptRecv(ushort header, Blocking<PacketReader> reader, bool returnPacket) {
            packetHandler.RegisterWait(header, reader, returnPacket);
        }
        #endregion

        #region Event Handlers
        private void OnConnected(object o, Session s) {
            Log.Report("Connected to server.");
            session = s;
            s.OnHandshake += handshakeHandler.Handle;
            s.OnPacket += packetHandler.Handle;
            s.OnDisconnected += OnDisconnected;
        }

        private void OnError(object c, SocketError e) {
            Log.Report("Connection error code " + e);
            cForm.ConnectToggle.Report(false);
        }

        private void OnDisconnected(object o, EventArgs e) {
            Log.Report("Disconnected from server.");
            State = ClientState.DISCONNECTED;
            ClearStats();
            dcst.Enabled = false;
            if (cForm.AutoRestart.Checked) {
                Connect(); //Start connection again
            } else {
                cForm.ConnectToggle.Report(true);
            }
        }
        #endregion

        private void AutoDCForFMShop(object sender, ElapsedEventArgs e) {
            if (doWhat == 1 && hasFMShop == false) {
                Log.Report("Disconnecting 5 Minute Test!");
                Disconnect();
                Connect();
            }
        }
    }
}
