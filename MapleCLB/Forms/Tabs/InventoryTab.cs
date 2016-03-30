using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapleCLB.Forms
{
    public partial class InventoryTab : UserControl
    {
        public InventoryTab()
        {
            InitializeComponent();
        }

        public void updateInventory(Dictionary<string, int> equipsClient, Dictionary<string, int> useClient, Dictionary<string, int> setUpClient, Dictionary<string, int> etcClient, Dictionary<string, int> cashClient, Dictionary<string,int> equippedClient)
        {
            foreach (KeyValuePair<string, int> kvp in equipsClient)
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] { kvp.Key, "" + kvp.Value }, -1);
                EquipsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem });
            }
            foreach (KeyValuePair<string, int> kvp in useClient)
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] { kvp.Key, "" + kvp.Value }, -1);
                UseListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem });
            }
            foreach (KeyValuePair<string, int> kvp in setUpClient)
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] { kvp.Key, "" + kvp.Value }, -1);
                SetUpListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem });
            }
            foreach (KeyValuePair<string, int> kvp in etcClient)
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] { kvp.Key, "" + kvp.Value }, -1);
                EtcListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem });
            }
            foreach (KeyValuePair<string, int> kvp in cashClient)
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] { kvp.Key, "" + kvp.Value }, -1);
                CashListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem });
            }
            foreach (KeyValuePair<string, int> kvp in equippedClient)
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] { kvp.Key, "" + kvp.Value }, -1);
                EquippedListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem });
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
            //var clickedMenuItem = sender as MenuItem;
            //var menuText = clickedMenuItem.Text;
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
