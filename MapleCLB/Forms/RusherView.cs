using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MapleCLB.MapleClient;
using MapleCLB.MapleClient.Functions;
using MapleCLB.Resources;

namespace MapleCLB.Forms {
    public partial class RusherView : UserControl {
        private Client Client;

        public RusherView() {
            InitializeComponent();
        }

        public void SetClient(Client client) {
            Client = client;
        }

        // TODO: Use custom serialization and split names up
        private static readonly IReadOnlyDictionary<int, string[]> MapNames = ResourceLoader.LoadMapNames();

        public void Update(int srcMap) {
            RushTree.Nodes.Clear();

            List<int> reachable = MapRusher.Reachable(srcMap);
            foreach (int map in reachable) {
                string[] names;
                MapNames.TryGetValue(map, out names);

                if (!RushTree.Nodes.ContainsKey(names[0])) {
                    RushTree.Nodes.Add(names[0], names[0]);
                }
                RushTree.Nodes[names[0]].Nodes.Add($"{map}: {names[1]}");
            }

            MapStatus.Text = $"{srcMap}";
            SetEnabled(true);
        }

        private void RushTree_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (RushTree.SelectedNode == null || RushTree.SelectedNode.Level <= 0) return;
            SetEnabled(false);

            //TODO: Fix ghetto parse on dst
            int src = int.Parse(MapStatus.Text);
            int dst = int.Parse(RushTree.SelectedNode.Text.Split(':')[0]);
            Client.MapRush.Report(MapRusher.Pathfind(src, dst));
        }

        private void SetEnabled(bool enabled) {
            RushTree.Enabled = enabled;
            RushList.Enabled = enabled;
        }
    }
}
