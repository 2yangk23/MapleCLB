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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ololToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lolToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientForm1 = new MapleCLB.Forms.ClientForm();
            this.AccountTabs.SuspendLayout();
            this.Account1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountTabs
            // 
            this.AccountTabs.Controls.Add(this.Account1);
            this.AccountTabs.Location = new System.Drawing.Point(3, 27);
            this.AccountTabs.Name = "AccountTabs";
            this.AccountTabs.SelectedIndex = 0;
            this.AccountTabs.Size = new System.Drawing.Size(703, 409);
            this.AccountTabs.TabIndex = 25;
            // 
            // Account1
            // 
            this.Account1.Controls.Add(this.clientForm1);
            this.Account1.Location = new System.Drawing.Point(4, 22);
            this.Account1.Name = "Account1";
            this.Account1.Padding = new System.Windows.Forms.Padding(3);
            this.Account1.Size = new System.Drawing.Size(695, 383);
            this.Account1.TabIndex = 0;
            this.Account1.Text = "Some Acc";
            this.Account1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ololToolStripMenuItem,
            this.lolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(707, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ololToolStripMenuItem
            // 
            this.ololToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lollToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.ololToolStripMenuItem.Name = "ololToolStripMenuItem";
            this.ololToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.ololToolStripMenuItem.Text = "File";
            // 
            // lollToolStripMenuItem
            // 
            this.lollToolStripMenuItem.Name = "lollToolStripMenuItem";
            this.lollToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.lollToolStripMenuItem.Text = "Load";
            // 
            // lolToolStripMenuItem
            // 
            this.lolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lolToolStripMenuItem1,
            this.lolToolStripMenuItem2});
            this.lolToolStripMenuItem.Name = "lolToolStripMenuItem";
            this.lolToolStripMenuItem.Size = new System.Drawing.Size(32, 20);
            this.lolToolStripMenuItem.Text = "lol";
            // 
            // lolToolStripMenuItem1
            // 
            this.lolToolStripMenuItem1.Name = "lolToolStripMenuItem1";
            this.lolToolStripMenuItem1.Size = new System.Drawing.Size(87, 22);
            this.lolToolStripMenuItem1.Text = "lol";
            // 
            // lolToolStripMenuItem2
            // 
            this.lolToolStripMenuItem2.Name = "lolToolStripMenuItem2";
            this.lolToolStripMenuItem2.Size = new System.Drawing.Size(87, 22);
            this.lolToolStripMenuItem2.Text = "lol";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // clientForm1
            // 
            this.clientForm1.AutoScroll = true;
            this.clientForm1.Location = new System.Drawing.Point(3, 3);
            this.clientForm1.Margin = new System.Windows.Forms.Padding(0);
            this.clientForm1.Name = "clientForm1";
            this.clientForm1.Size = new System.Drawing.Size(689, 377);
            this.clientForm1.TabIndex = 24;
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
            this.AccountTabs.ResumeLayout(false);
            this.Account1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ClientForm clientForm1;
        private TabControl AccountTabs;
        private TabPage Account1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ololToolStripMenuItem;
        private ToolStripMenuItem lollToolStripMenuItem;
        private ToolStripMenuItem lolToolStripMenuItem;
        private ToolStripMenuItem lolToolStripMenuItem1;
        private ToolStripMenuItem lolToolStripMenuItem2;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
    }
}

