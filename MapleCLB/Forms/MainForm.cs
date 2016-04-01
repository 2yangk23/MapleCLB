using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MapleCLB.MapleClient.Functions;
using MapleCLB.Types;
using Microsoft.VisualBasic;

namespace MapleCLB.Forms {
    public sealed partial class MainForm : Form {
        private readonly LoadAccountForm loadingForm = new LoadAccountForm();

        public MainForm() {
            InitializeComponent();
            Text = $"[{Assembly.GetEntryAssembly().GetName().Version}] MapleStory Clientless Bot";
        }

        private void CreateTab(string title, Tuple<Account, Settings> data = null) {
            var accountTab = new TabPage(title) {
                UseVisualStyleBackColor = true
            };
            var clientForm = new ClientForm();
            clientForm.InitializeAccount(data?.Item1);
            // TODO: Initialize Settings
            accountTab.Controls.Add(clientForm);

            AccountTabs.TabPages.Insert(AccountTabs.TabCount - 1, accountTab);
            AccountTabs.SelectedTab = accountTab;
        }

        private void SaveAccount(string filename, Account account) {
            string path = Directory.GetCurrentDirectory() + $@"\Accounts\{filename}.acc";
            var settings = new Settings();
            AccountLoader.Save(path, account, settings);
            AccountTabs.SelectedTab.Text = filename;
        }

        private static bool ConfirmClose() {
            var result = MessageBox.Show("Are you sure you to close to application?", "Close Application?",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        #region Event Handlers
        private void AccountTabs_Selecting(object sender, TabControlCancelEventArgs e) {
            if (e.TabPage != AddTab) {
                return;
            }
            CreateTab("Default");
        }

        private void FileNew_Click(object sender, EventArgs e) {
            CreateTab("Default");
        }

        private void FileOpen_Click(object sender, EventArgs e) {
            if (loadingForm.ShowDialog() != DialogResult.OK) {
                return;
            }
            if (loadingForm.Loading.Count == 0) {
                CreateTab("Default");
            } else {
                foreach (KeyValuePair<string, Tuple<Account, Settings>> t in loadingForm.Loading) {
                    CreateTab(t.Key, t.Value);
                }
                loadingForm.Loading.Clear();
            }
        }

        private void FileSave_Click(object sender, EventArgs e) {
            if ("Default".Equals(AccountTabs.SelectedTab.Text)) {
                FileSaveAs_Click(sender, e);
                return;
            }
            var clientForm = AccountTabs.SelectedTab.Controls["ClientForm"] as ClientForm;
            SaveAccount(AccountTabs.SelectedTab.Text, clientForm?.GetAccount());
        }

        private void FileSaveAs_Click(object sender, EventArgs e) {
            var clientForm = AccountTabs.SelectedTab.Controls["ClientForm"] as ClientForm;
            var account = clientForm?.GetAccount();
            string filename = account?.Username ?? "";
            var result = InputForm.Show("Save Account", ref filename, "Profile Name", "Save");
            if (result == DialogResult.OK) {
                SaveAccount(filename, account);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!ConfirmClose()) {
                e.Cancel = true;
            }
        }

        private void FileExit_Click(object sender, EventArgs e) {
            if (ConfirmClose()) {
                Application.Exit();
            }
        }
        #endregion
    }
}
