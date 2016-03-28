using System.Collections.Generic;
using System.Windows.Forms;
using MapleCLB.Resources;
using MapleCLB.Types.Items;

namespace MapleCLB.Forms {
    public partial class Information : Form {
        public Information() {
            InitializeComponent();
        }

        //To Do : Split up inventory calls
        public void UpdateInventory(Inventory inventory) {
            foreach (KeyValuePair<short, Equip> kvp in inventory.EquipInventory) {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Equip[kvp.Value.Id] }, -1);
                EquipListView.Items.Add(listViewItem);
            }

            foreach (KeyValuePair<short, Other> kvp in inventory.UseInventory) {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Use[kvp.Value.Id] }, -1);
                UseListView.Items.Add(listViewItem);
            }

            foreach (KeyValuePair<short, Other> kvp in inventory.SetupInventory) {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Setup[kvp.Value.Id] }, -1);
                SetupListView.Items.Add(listViewItem);
            }

            foreach (KeyValuePair<short, Other> kvp in inventory.EtcInventory) {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Etc[kvp.Value.Id] }, -1);
                EtcListView.Items.Add(listViewItem);
            }
        }

        //Temp
        public void Clear() {
            EtcListView.Items.Clear();
            SetupListView.Items.Clear();
            UseListView.Items.Clear();
            EquipListView.Items.Clear();
        }
    }
}
