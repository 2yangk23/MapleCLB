using System.Windows.Forms;

namespace MapleCLB.Forms
{
    sealed partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AccountTabs = new System.Windows.Forms.TabControl();
            this.Account1 = new System.Windows.Forms.TabPage();
            this.AddTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.lolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lolToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.ClientForm = new ClientForm();
            this.AccountTabs.SuspendLayout();
            this.Account1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountTabs
            // 
            this.AccountTabs.Controls.Add(this.Account1);
            this.AccountTabs.Controls.Add(this.AddTab);
            this.AccountTabs.Location = new System.Drawing.Point(3, 27);
            this.AccountTabs.Name = "AccountTabs";
            this.AccountTabs.SelectedIndex = 0;
            this.AccountTabs.Size = new System.Drawing.Size(703, 409);
            this.AccountTabs.TabIndex = 25;
            this.AccountTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.AccountTabs_Selecting);
            // 
            // Account1
            // 
            this.Account1.Controls.Add(this.ClientForm);
            this.Account1.Location = new System.Drawing.Point(4, 22);
            this.Account1.Name = "Account1";
            this.Account1.Padding = new System.Windows.Forms.Padding(3);
            this.Account1.Size = new System.Drawing.Size(695, 383);
            this.Account1.TabIndex = 0;
            this.Account1.Text = "Default";
            this.Account1.UseVisualStyleBackColor = true;
            // 
            // AddTab
            // 
            this.AddTab.Location = new System.Drawing.Point(4, 22);
            this.AddTab.Name = "AddTab";
            this.AddTab.Size = new System.Drawing.Size(695, 383);
            this.AddTab.TabIndex = 1;
            this.AddTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu,
            this.lolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(707, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainMenu
            // 
            this.MainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNew,
            this.FileOpen,
            this.FileSave,
            this.FileSaveAs,
            this.toolStripSeparator1,
            this.FileExit});
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(37, 20);
            this.MainMenu.Text = "File";
            // 
            // FileNew
            // 
            this.FileNew.Name = "FileNew";
            this.FileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileNew.Size = new System.Drawing.Size(155, 22);
            this.FileNew.Text = "New";
            this.FileNew.Click += new System.EventHandler(this.FileNew_Click);
            // 
            // FileOpen
            // 
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpen.Size = new System.Drawing.Size(155, 22);
            this.FileOpen.Text = "Open...";
            this.FileOpen.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // FileSave
            // 
            this.FileSave.Name = "FileSave";
            this.FileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSave.Size = new System.Drawing.Size(155, 22);
            this.FileSave.Text = "Save";
            this.FileSave.Click += new System.EventHandler(this.FileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // FileExit
            // 
            this.FileExit.Name = "FileExit";
            this.FileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.FileExit.ShowShortcutKeys = false;
            this.FileExit.Size = new System.Drawing.Size(155, 22);
            this.FileExit.Text = "Exit";
            this.FileExit.Click += new System.EventHandler(this.FileExit_Click);
            // 
            // lolToolStripMenuItem
            // 
            this.lolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lolToolStripMenuItem1,
            this.lolToolStripMenuItem2});
            this.lolToolStripMenuItem.Name = "lolToolStripMenuItem";
            this.lolToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.lolToolStripMenuItem.Text = "Help";
            // 
            // lolToolStripMenuItem1
            // 
            this.lolToolStripMenuItem1.Name = "lolToolStripMenuItem1";
            this.lolToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.lolToolStripMenuItem1.Text = "im a homo";
            // 
            // lolToolStripMenuItem2
            // 
            this.lolToolStripMenuItem2.Name = "lolToolStripMenuItem2";
            this.lolToolStripMenuItem2.Size = new System.Drawing.Size(132, 22);
            this.lolToolStripMenuItem2.Text = "About";
            // 
            // FileSaveAs
            // 
            this.FileSaveAs.Name = "FileSaveAs";
            this.FileSaveAs.Size = new System.Drawing.Size(155, 22);
            this.FileSaveAs.Text = "Save As...";
            this.FileSaveAs.Click += new System.EventHandler(this.FileSaveAs_Click);
            // 
            // ClientForm
            // 
            this.ClientForm.AutoScroll = true;
            this.ClientForm.Location = new System.Drawing.Point(3, 3);
            this.ClientForm.Margin = new System.Windows.Forms.Padding(0);
            this.ClientForm.Name = "ClientForm";
            this.ClientForm.Size = new System.Drawing.Size(689, 377);
            this.ClientForm.TabIndex = 24;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 438);
            this.Controls.Add(this.AccountTabs);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MapleStory Clientless Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.AccountTabs.ResumeLayout(false);
            this.Account1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ClientForm ClientForm;
        private TabControl AccountTabs;
        private TabPage Account1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem MainMenu;
        private ToolStripMenuItem FileOpen;
        private ToolStripMenuItem lolToolStripMenuItem;
        private ToolStripMenuItem lolToolStripMenuItem1;
        private ToolStripMenuItem lolToolStripMenuItem2;
        private ToolStripMenuItem FileSave;
        private TabPage AddTab;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem FileExit;
        private ToolStripMenuItem FileNew;
        private ToolStripMenuItem FileSaveAs;
    }
}

