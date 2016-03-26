using System;
using System.ComponentModel;
using System.Windows.Forms;
using MapleLib.Packet;

namespace MapleCLB.Forms {
    public partial class PacketView : UserControl {
        private TextBox PacketInput;
        internal bool LogSend, LogRecv;
        private TreeView UsedTree;

        public Progress<byte[]> WriteSend, WriteRecv;

        public PacketView() {
            InitializeComponent();
            InitializeProgress();

            SendTree.NodeMouseClick += (sender, args) => SendTree.SelectedNode = args.Node;
            RecvTree.NodeMouseClick += (sender, args) => RecvTree.SelectedNode = args.Node;

            UsedTree = SendTree;
        }

        // TODO: Allow user to switch between TreeView and ListView
        public void SetInput(TextBox packetInput) {
            PacketInput = packetInput;
        }

        // TODO: Try doing this without PacketReader
        private void InitializeProgress() {
            WriteSend = new Progress<byte[]>(a => {
                if (!LogSend) return;

                var reader = new PacketReader(a);
                string key = reader.ReadShort().ToString("X").PadLeft(4, '0');

                if (!SendTree.Nodes.ContainsKey(key)) {
                    SendTree.Nodes.Add(key, key);
                }
                SendTree.Nodes[key].Nodes.Add(reader.ToString());
            });

            WriteRecv = new Progress<byte[]>(a => {
                if (!LogRecv) return;

                var reader = new PacketReader(a);
                string key = reader.ReadShort().ToString("X").PadLeft(4, '0');

                if (!RecvTree.Nodes.ContainsKey(key)) {
                    RecvTree.Nodes.Add(key, key);
                }
                RecvTree.Nodes[key].Nodes.Add(reader.ToString());
            });
        }

        private void PacketTabs_SelectedIndexChanged(object sender, EventArgs e) {
            switch (PacketTabs.SelectedIndex) {
                case 0:
                    UsedTree = SendTree;
                    LogPacket.Checked = LogSend;
                    break;
                case 1:
                    UsedTree = RecvTree;
                    LogPacket.Checked = LogRecv;
                    break;
            }
        }

        private void PacketTree_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (SendTree.SelectedNode != null && SendTree.SelectedNode.Level > 0 && 
                PacketTabs.SelectedIndex == 0 && PacketInput != null) {
                PacketInput.Text = SendTree.SelectedNode.Text;
            }
        }

        private void PacketMenu_Opening(object sender, CancelEventArgs e) {
            RemovePacket.Enabled = UsedTree.SelectedNode != null;
        }

        // This will allow you to manage ignored packets
        private void ManagePacket_Click(object sender, EventArgs e) {
            Console.WriteLine("Not working");
        }

        private void IgnorePacket_Click(object sender, EventArgs e) {
            Console.WriteLine("Not working");
        }

        private void RemovePacket_Click(object sender, EventArgs e) {
            UsedTree.SelectedNode.Remove();
        }

        private void ClearPacket_Click(object sender, EventArgs e) {
            UsedTree.Nodes.Clear();
        }

        private void LogPacket_CheckedChanged(object sender, EventArgs e) {
            switch (PacketTabs.SelectedIndex) {
                case 0:
                    LogSend = LogPacket.Checked;
                    break;
                case 1:
                    LogRecv = LogPacket.Checked;
                    break;
            }
        }
    }
}
