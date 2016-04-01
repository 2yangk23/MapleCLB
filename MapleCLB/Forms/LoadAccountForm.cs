using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MapleCLB.MapleClient.Functions;
using MapleCLB.Types;

namespace MapleCLB.Forms {
    public partial class LoadAccountForm : Form {
        public Dictionary<string, Tuple<Account, Settings>> Loading;

        public LoadAccountForm() {
            InitializeComponent();
            AccountList.Columns[0].Width = AccountList.Width - 4;

            Loading = new Dictionary<string, Tuple<Account, Settings>>();
            LoadAccounts();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys key) {
            switch (key) {
                case Keys.F5:
                    LoadAccounts(); // Refresh
                    return true;
                default:
                    return false;
            }
        }

        private void LoadAccounts() {
            AccountList.Items.Clear();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Accounts\", "*.acc");
            AccountList.Columns[0].Width = AccountList.Width - (files.Length > 11 ? 21 : 4);
            foreach (string path in files) {
                AccountList.Items.Add(Path.GetFileNameWithoutExtension(path), path);
            }
            SelectBtn.Enabled = AccountList.Items.Count != 0;
        }

        #region Event Handlers
        private void AccountList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
            e.Cancel = true;
            e.NewWidth = AccountList.Columns[e.ColumnIndex].Width;
        }

        private void AccountList_ColumnClick(object sender, ColumnClickEventArgs e) {
            AccountList.Sort();
        }

        private void CancelBtn_Click(object sender, EventArgs e) {
            AccountList.SelectedIndices.Clear();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectAccount_Click(object sender, EventArgs e) {
            if (AccountList.SelectedItems.Count == 0) { // Nothing selected
                return;
            }
            for (int i = 0; i < AccountList.SelectedItems.Count; i++) {
                var selected = AccountList.SelectedItems[i];
                Tuple<Account, Settings> tuple = AccountLoader.Load(selected.ImageKey);
                Loading[selected.Text] = tuple;
            }
            AccountList.SelectedIndices.Clear();
            DialogResult = DialogResult.OK;
            Visible = false;
        }
        #endregion
    }
}
