using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using MapleCLB.Forms;
using MapleCLB.MapleClient.Handlers;
using MapleCLB.MapleLib;
using MapleCLB.Tools;

using MapleCLB.Packets.Send;
using MapleCLB.Types;
using Timer = System.Timers.Timer;

namespace MapleCLB.MapleClient {
    enum ClientMode : byte {
        DISCONNECTED,
        CONNECTED,
        LOGIN,
        CASHSHOP,
        GAME,
    }

    public class Client {
        private static int seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        /* UI Info */
        private readonly ClientForm CForm;
        public readonly IProgress<bool> ConnectToggle;
        public readonly IProgress<string> WriteLog;
        public readonly IProgress<byte[]> WriteSend, WriteRecv;

        public readonly IProgress<Mapler> UpdateMapler; 
        public readonly IProgress<byte> UpdateChannel;

        /* Client Info */
        public Session Session;
        public int Hwid1 = Rng.Value.Next(0, int.MaxValue);
        public short Hwid2 = (short) Rng.Value.Next(0, short.MaxValue);

        private readonly Handshake HandshakeHandler;
        private readonly Packet PacketHandler;

        internal ClientMode Mode { get; set; }

        /* Settings */
        public int ServerTimeout, ChannelTimeout;
        public Timer cst;
        public int autoCStime;

        public Timer ccst;
        public int autoCCtime;

        /* User Info */
        internal string User, Pass, Pic, Selection;
        internal Mapler Mapler;
        internal int UserId; 
        internal byte World, Channel, Select, doWhat;

        internal long SessionId;

        public bool shouldCC;

        /* Dictionaries */  
        public Dictionary<int, string> UidMap = new Dictionary<int, string>(); //uid -> ign
        public MultiKeyDictionary<byte, string, int> CharMap = new MultiKeyDictionary<byte, string, int>(); //slot/ign -> uid

        public Dictionary<string, int> IgnUid = new Dictionary<string, int>();            //IGN -> UID
        public Dictionary<int, byte[]> UidMovementPacket = new Dictionary<int, byte[]>(); //UID -> MovementPacket

        public Client(ClientForm form) {
            /* Initialize Form */
            CForm = form;

            ConnectToggle   = form.ConnectToggle;
            WriteLog        = form.WriteLog;
            WriteSend       = form.WriteSend;
            WriteRecv       = form.WriteRecv;
            UpdateMapler    = form.UpdateMapler;
            UpdateChannel   = form.UpdateCh;

            /* Initialize Client */
            Mode = ClientMode.DISCONNECTED;
            HandshakeHandler = new Handshake(this);
            PacketHandler = new Packet(this);

            ServerTimeout = 40000;
            ChannelTimeout = 12000;
            autoCStime = 3600000;

            //cst = new System.Timers.Timer(autoCStime);
            //cst.Elapsed += new System.Timers.ElapsedEventHandler(AutoCS);

            ccst = new Timer(autoCStime);
            ccst.Elapsed += AutoCC;

            shouldCC = false;
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
                if (CForm.IsLogSend()) {
                    byte[] copy = new byte[packet.Length];
                    Buffer.BlockCopy(packet, 0, copy, 0, packet.Length);
                    WriteSend.Report(copy);
                }
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

        void AutoCC(object sender, ElapsedEventArgs e) {
            if (doWhat == 1) {
                WriteLog.Report("Changing to Ch 2");
                shouldCC = true;
                SendPacket(General.ChangeChannel(0x01));
            }
        }

        //AutoCS Event (Timer)
        void AutoCS(object sender, ElapsedEventArgs e) {
            //if (Program.Gui.aCS.Checked)
            //{
            //    WriteLog.Report(("Auto CS time!"));
            //   SendPacket(General.EnterCS());
            //    Mode = ClientMode.CASHSHOP;
            // }
        }

        public void ClearStats() {
            UpdateMapler.Report(null);
            UpdateChannel.Report(0);
        }

        public void OnDisconnected(object o, EventArgs e) {
            WriteLog.Report(("Disconnected from server."));
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
