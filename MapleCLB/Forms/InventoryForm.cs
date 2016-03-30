using System.Collections.Generic;
using System.Windows.Forms;
using MapleCLB.Resources;
using MapleCLB.Types.Items;

namespace MapleCLB.Forms
{
    public partial class InventoryForm : UserControl
    {
        public InventoryForm()
        {
            InitializeComponent();
        }


        public void UpdateInventory(Inventory inventory)
        {
            foreach (KeyValuePair<short, Equip> kvp in inventory.EquipInventory)
            {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Equip[kvp.Value.Id] }, -1);
                EquipListView.Items.Add(listViewItem);
            }

            foreach (KeyValuePair<short, Other> kvp in inventory.UseInventory)
            {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Use[kvp.Value.Id] }, -1);
                UseListView.Items.Add(listViewItem);
            }

            foreach (KeyValuePair<short, Other> kvp in inventory.SetupInventory)
            {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Setup[kvp.Value.Id] }, -1);
                SetupListView.Items.Add(listViewItem);
            }

            foreach (KeyValuePair<short, Other> kvp in inventory.EtcInventory)
            {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Etc[kvp.Value.Id] }, -1);
                EtcListView.Items.Add(listViewItem);
            }

            foreach (KeyValuePair<short, Other> kvp in inventory.CashInventory)
            {
                var listViewItem = new ListViewItem(new[] { kvp.Key.ToString(), ItemData.Etc[kvp.Value.Id] }, -1);
                CashListView.Items.Add(listViewItem);
            }


        }


        public void Clear()
        {
            EtcListView.Items.Clear();
            SetUpListView.Items.Clear();
            UseListView.Items.Clear();
            EquipsListView.Items.Clear();
            EquippedListView.Items.Clear();
            CashListView.Items.Clear();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.ToString())
            {
                case "Drop":
                    Console.WriteLine("Dropped!");
                    break;
                case "Trade":
                    Console.WriteLine("Sup Not Working!");
                    break;
                case "Drop All Tab":
                    Console.WriteLine("Dropping All Tab!");
                    break;
                case "Drop Inventory":
                    Console.WriteLine("Dropping Inventory!");
                    break;
                default:
                    break;

            }





        }

    }
}
