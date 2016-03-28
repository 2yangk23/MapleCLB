using System;
using System.ComponentModel;
using System.Windows.Forms;
using MapleLib.Packet;

namespace MapleCLB.Forms.Tabs {
    public partial class PacketTab : UserControl {
        private TextBox packetInput;
        private TreeView usedTree;

        internal bool LogSend, LogRecv;
        internal IProgress<byte[]> WriteSend, WriteRecv;

        public PacketTab() {
            InitializeComponent();
            InitializeProgress();

            SendTree.NodeMouseClick += (sender, args) => SendTree.SelectedNode = args.Node;
            RecvTree.NodeMouseClick += (sender, args) => RecvTree.SelectedNode = args.Node;

            usedTree = SendTree;
        }

        // TODO: Allow user to switch between TreeView and ListView
        public void SetInput(TextBox packetInput) {
            this.packetInput = packetInput;
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
                    usedTree = SendTree;
                    LogPacket.Checked = LogSend;
                    break;
                case 1:
                    usedTree = RecvTree;
                    LogPacket.Checked = LogRecv;
                    break;
            }
        }

        private void PacketTree_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (SendTree.SelectedNode != null && SendTree.SelectedNode.Level > 0 && 
                PacketTabs.SelectedIndex == 0 && packetInput != null) {
                packetInput.Text = SendTree.SelectedNode.Text;
            }
        }

        private void PacketMenu_Opening(object sender, CancelEventArgs e) {
            RemovePacket.Enabled = usedTree.SelectedNode != null;
        }

        // This will allow you to manage ignored packets
        private void ManagePacket_Click(object sender, EventArgs e) {
            Console.WriteLine("Not working");
        }

        private void IgnorePacket_Click(object sender, EventArgs e) {
            Console.WriteLine("Not working");
        }

        private void RemovePacket_Click(object sender, EventArgs e) {
            usedTree.SelectedNode.Remove();
        }

        private void ClearPacket_Click(object sender, EventArgs e) {
            usedTree.Nodes.Clear();
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
