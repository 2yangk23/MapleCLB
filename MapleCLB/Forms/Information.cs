using System.Collections.Generic;
using System.Windows.Forms;

namespace MapleCLB.Forms
{
    public partial class Information : Form
    {
        //To Do : Split up inventory calls
        public void updateInventory(Dictionary<string, int> equipsClient, Dictionary<string, int> useClient, Dictionary<string, int> setUpClient, Dictionary<string, int> etcClient)
        {
            foreach( KeyValuePair<string, int> kvp in equipsClient )
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] { kvp.Key, "" + kvp.Value }, -1);
                EquipListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem });
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
        }

        public void Clear() //Temp
        {
            EtcListView.Items.Clear();
            SetUpListView.Items.Clear();
            UseListView.Items.Clear();
            EquipListView.Items.Clear();
        }
        public Information()
        {
            InitializeComponent();
        }
    }
}
