using System.Collections.Generic;
using System.Windows.Forms;

namespace MapleCLB.Forms
{
    public partial class Information : Form {
        //To Do : Split up inventory calls
        public void UpdateInventory(Dictionary<string, int> equipsClient, Dictionary<string, int> useClient, Dictionary<string, int> setUpClient, Dictionary<string, int> etcClient) {
            foreach (KeyValuePair<string, int> kvp in equipsClient) {
                ListViewItem listViewItem = new ListViewItem(new[] { kvp.Key, "" + kvp.Value }, -1);
                EquipListView.Items.AddRange(new[] { listViewItem });
            }

            foreach (KeyValuePair<string, int> kvp in useClient) {
                ListViewItem listViewItem = new ListViewItem(new[] { kvp.Key, "" + kvp.Value }, -1);
                UseListView.Items.AddRange(new[] { listViewItem });
            }

            foreach (KeyValuePair<string, int> kvp in setUpClient) {
                ListViewItem listViewItem = new ListViewItem(new[] { kvp.Key, "" + kvp.Value }, -1);
                SetUpListView.Items.AddRange(new[] { listViewItem });
            }

            foreach (KeyValuePair<string, int> kvp in etcClient) {
                ListViewItem listViewItem = new ListViewItem(new[] { kvp.Key, "" + kvp.Value }, -1);
                EtcListView.Items.AddRange(new[] { listViewItem });
            }
        }
        //Temp
        public void Clear() {
            EtcListView.Items.Clear();
            SetUpListView.Items.Clear();
            UseListView.Items.Clear();
            EquipListView.Items.Clear();
        }
        public Information() {
            InitializeComponent();
        }
    }
}
