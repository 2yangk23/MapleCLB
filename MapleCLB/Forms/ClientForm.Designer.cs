namespace MapleCLB.Forms {
    partial class ClientForm {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.AccountGroup = new System.Windows.Forms.GroupBox();
            this.ModeList = new System.Windows.Forms.ComboBox();
            this.CharInput = new System.Windows.Forms.TextBox();
            this.SelectList = new System.Windows.Forms.ComboBox();
            this.ChannelList = new System.Windows.Forms.ComboBox();
            this.WorldList = new System.Windows.Forms.ComboBox();
            this.PicInput = new System.Windows.Forms.TextBox();
            this.PassInput = new System.Windows.Forms.TextBox();
            this.UserInput = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.StatGroup = new System.Windows.Forms.GroupBox();
            this.MesoStatus = new System.Windows.Forms.Label();
            this.Mesos = new System.Windows.Forms.Label();
            this.MapStat = new System.Windows.Forms.Label();
            this.ChannelStat = new System.Windows.Forms.Label();
            this.LevelStat = new System.Windows.Forms.Label();
            this.NameStat = new System.Windows.Forms.Label();
            this.MapLbl = new System.Windows.Forms.Label();
            this.ChannelLbl = new System.Windows.Forms.Label();
            this.LevelLbl = new System.Windows.Forms.Label();
            this.NameLbl = new System.Windows.Forms.Label();
            this.AutoRestart = new System.Windows.Forms.CheckBox();
            this.FeatureGroup = new System.Windows.Forms.GroupBox();
            this.CsBtn = new System.Windows.Forms.Button();
            this.MoveBtn = new System.Windows.Forms.Button();
            this.CcBtn = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.AccountTab = new System.Windows.Forms.TabPage();
            this.PacketTab = new System.Windows.Forms.TabPage();
            this.PacketView = new MapleCLB.Forms.PacketView();
            this.RushTab = new System.Windows.Forms.TabPage();
            this.RushStatusBar = new System.Windows.Forms.StatusStrip();
            this.MapStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.MapStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSep = new System.Windows.Forms.ToolStripStatusLabel();
            this.RushStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.RushStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusSep2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.elliniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.henesysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RushTree = new System.Windows.Forms.TreeView();
            this.LogTab = new System.Windows.Forms.TabPage();
            this.LogText = new System.Windows.Forms.TextBox();
            this.PacketInput = new System.Windows.Forms.TextBox();
            this.DelayInput = new System.Windows.Forms.TextBox();
            this.SendMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendSpamBtn = new MapleCLB.Tools.SplitButton();
            this.AccountGroup.SuspendLayout();
            this.StatGroup.SuspendLayout();
            this.FeatureGroup.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.AccountTab.SuspendLayout();
            this.PacketTab.SuspendLayout();
            this.RushTab.SuspendLayout();
            this.RushStatusBar.SuspendLayout();
            this.LogTab.SuspendLayout();
            this.SendMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountGroup
            // 
            this.AccountGroup.Controls.Add(this.ModeList);
            this.AccountGroup.Controls.Add(this.CharInput);
            this.AccountGroup.Controls.Add(this.SelectList);
            this.AccountGroup.Controls.Add(this.ChannelList);
            this.AccountGroup.Controls.Add(this.WorldList);
            this.AccountGroup.Controls.Add(this.PicInput);
            this.AccountGroup.Controls.Add(this.PassInput);
            this.AccountGroup.Controls.Add(this.UserInput);
            this.AccountGroup.Location = new System.Drawing.Point(6, 6);
            this.AccountGroup.Name = "AccountGroup";
            this.AccountGroup.Size = new System.Drawing.Size(197, 154);
            this.AccountGroup.TabIndex = 0;
            this.AccountGroup.TabStop = false;
            this.AccountGroup.Text = "Account";
            // 
            // ModeList
            // 
            this.ModeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeList.FormattingEnabled = true;
            this.ModeList.Items.AddRange(new object[] {
            "Login Mode",
            "ShopBot Mode"});
            this.ModeList.Location = new System.Drawing.Point(6, 125);
            this.ModeList.Name = "ModeList";
            this.ModeList.Size = new System.Drawing.Size(185, 21);
            this.ModeList.TabIndex = 24;
            // 
            // CharInput
            // 
            this.CharInput.Location = new System.Drawing.Point(73, 99);
            this.CharInput.MaxLength = 12;
            this.CharInput.Name = "CharInput";
            this.CharInput.Size = new System.Drawing.Size(118, 20);
            this.CharInput.TabIndex = 10;
            // 
            // SelectList
            // 
            this.SelectList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectList.FormattingEnabled = true;
            this.SelectList.Items.AddRange(new object[] {
            "Slot",
            "Name"});
            this.SelectList.Location = new System.Drawing.Point(6, 98);
            this.SelectList.Name = "SelectList";
            this.SelectList.Size = new System.Drawing.Size(61, 21);
            this.SelectList.TabIndex = 9;
            // 
            // ChannelList
            // 
            this.ChannelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChannelList.FormattingEnabled = true;
            this.ChannelList.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.ChannelList.Location = new System.Drawing.Point(121, 71);
            this.ChannelList.Name = "ChannelList";
            this.ChannelList.Size = new System.Drawing.Size(70, 21);
            this.ChannelList.TabIndex = 7;
            // 
            // WorldList
            // 
            this.WorldList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WorldList.FormattingEnabled = true;
            this.WorldList.Items.AddRange(new object[] {
            "Scania",
            "Bera",
            "Broa",
            "Windia",
            "Khaini",
            "Bellocan",
            "Mardia",
            "Kradia",
            "Yellonde",
            "Dementhos",
            "Galicia",
            "El Nido",
            "Zenith",
            "Arcania",
            "Chaos",
            "Nova",
            "Renegades"});
            this.WorldList.Location = new System.Drawing.Point(6, 71);
            this.WorldList.Name = "WorldList";
            this.WorldList.Size = new System.Drawing.Size(109, 21);
            this.WorldList.TabIndex = 6;
            // 
            // PicInput
            // 
            this.PicInput.Location = new System.Drawing.Point(121, 45);
            this.PicInput.MaxLength = 16;
            this.PicInput.Name = "PicInput";
            this.PicInput.Size = new System.Drawing.Size(70, 20);
            this.PicInput.TabIndex = 5;
            // 
            // PassInput
            // 
            this.PassInput.Location = new System.Drawing.Point(6, 45);
            this.PassInput.MaxLength = 20;
            this.PassInput.Name = "PassInput";
            this.PassInput.PasswordChar = '*';
            this.PassInput.Size = new System.Drawing.Size(109, 20);
            this.PassInput.TabIndex = 4;
            // 
            // UserInput
            // 
            this.UserInput.Location = new System.Drawing.Point(6, 19);
            this.UserInput.MaxLength = 50;
            this.UserInput.Name = "UserInput";
            this.UserInput.Size = new System.Drawing.Size(185, 20);
            this.UserInput.TabIndex = 3;
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(6, 166);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(198, 27);
            this.ConnectBtn.TabIndex = 1;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // StatGroup
            // 
            this.StatGroup.Controls.Add(this.MesoStatus);
            this.StatGroup.Controls.Add(this.Mesos);
            this.StatGroup.Controls.Add(this.MapStat);
            this.StatGroup.Controls.Add(this.ChannelStat);
            this.StatGroup.Controls.Add(this.LevelStat);
            this.StatGroup.Controls.Add(this.NameStat);
            this.StatGroup.Controls.Add(this.MapLbl);
            this.StatGroup.Controls.Add(this.ChannelLbl);
            this.StatGroup.Controls.Add(this.LevelLbl);
            this.StatGroup.Controls.Add(this.NameLbl);
            this.StatGroup.Location = new System.Drawing.Point(6, 199);
            this.StatGroup.Name = "StatGroup";
            this.StatGroup.Size = new System.Drawing.Size(420, 101);
            this.StatGroup.TabIndex = 2;
            this.StatGroup.TabStop = false;
            this.StatGroup.Text = "Stats";
            // 
            // MesoStatus
            // 
            this.MesoStatus.AutoSize = true;
            this.MesoStatus.Location = new System.Drawing.Point(210, 19);
            this.MesoStatus.Name = "MesoStatus";
            this.MesoStatus.Size = new System.Drawing.Size(16, 13);
            this.MesoStatus.TabIndex = 16;
            this.MesoStatus.Text = "-1";
            // 
            // Mesos
            // 
            this.Mesos.AutoSize = true;
            this.Mesos.Location = new System.Drawing.Point(156, 19);
            this.Mesos.Name = "Mesos";
            this.Mesos.Size = new System.Drawing.Size(41, 13);
            this.Mesos.TabIndex = 8;
            this.Mesos.Text = "Mesos:";
            // 
            // MapStat
            // 
            this.MapStat.AutoSize = true;
            this.MapStat.Location = new System.Drawing.Point(64, 73);
            this.MapStat.Name = "MapStat";
            this.MapStat.Size = new System.Drawing.Size(16, 13);
            this.MapStat.TabIndex = 7;
            this.MapStat.Text = "-1";
            // 
            // ChannelStat
            // 
            this.ChannelStat.AutoSize = true;
            this.ChannelStat.Location = new System.Drawing.Point(64, 55);
            this.ChannelStat.Name = "ChannelStat";
            this.ChannelStat.Size = new System.Drawing.Size(16, 13);
            this.ChannelStat.TabIndex = 6;
            this.ChannelStat.Text = "-1";
            // 
            // LevelStat
            // 
            this.LevelStat.AutoSize = true;
            this.LevelStat.Location = new System.Drawing.Point(64, 37);
            this.LevelStat.Name = "LevelStat";
            this.LevelStat.Size = new System.Drawing.Size(13, 13);
            this.LevelStat.TabIndex = 5;
            this.LevelStat.Text = "0";
            // 
            // NameStat
            // 
            this.NameStat.AutoSize = true;
            this.NameStat.Location = new System.Drawing.Point(64, 19);
            this.NameStat.Name = "NameStat";
            this.NameStat.Size = new System.Drawing.Size(53, 13);
            this.NameStat.TabIndex = 4;
            this.NameStat.Text = "Unknown";
            // 
            // MapLbl
            // 
            this.MapLbl.AutoSize = true;
            this.MapLbl.Location = new System.Drawing.Point(6, 73);
            this.MapLbl.Name = "MapLbl";
            this.MapLbl.Size = new System.Drawing.Size(31, 13);
            this.MapLbl.TabIndex = 3;
            this.MapLbl.Text = "Map:";
            // 
            // ChannelLbl
            // 
            this.ChannelLbl.AutoSize = true;
            this.ChannelLbl.Location = new System.Drawing.Point(6, 55);
            this.ChannelLbl.Name = "ChannelLbl";
            this.ChannelLbl.Size = new System.Drawing.Size(49, 13);
            this.ChannelLbl.TabIndex = 2;
            this.ChannelLbl.Text = "Channel:";
            // 
            // LevelLbl
            // 
            this.LevelLbl.AutoSize = true;
            this.LevelLbl.Location = new System.Drawing.Point(6, 37);
            this.LevelLbl.Name = "LevelLbl";
            this.LevelLbl.Size = new System.Drawing.Size(36, 13);
            this.LevelLbl.TabIndex = 1;
            this.LevelLbl.Text = "Level:";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(6, 19);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(38, 13);
            this.NameLbl.TabIndex = 0;
            this.NameLbl.Text = "Name:";
            // 
            // AutoRestart
            // 
            this.AutoRestart.AutoSize = true;
            this.AutoRestart.Location = new System.Drawing.Point(219, 172);
            this.AutoRestart.Name = "AutoRestart";
            this.AutoRestart.Size = new System.Drawing.Size(85, 17);
            this.AutoRestart.TabIndex = 14;
            this.AutoRestart.Text = "Auto Restart";
            this.AutoRestart.UseVisualStyleBackColor = true;
            // 
            // FeatureGroup
            // 
            this.FeatureGroup.Controls.Add(this.CsBtn);
            this.FeatureGroup.Controls.Add(this.MoveBtn);
            this.FeatureGroup.Controls.Add(this.CcBtn);
            this.FeatureGroup.Location = new System.Drawing.Point(209, 6);
            this.FeatureGroup.Name = "FeatureGroup";
            this.FeatureGroup.Size = new System.Drawing.Size(223, 154);
            this.FeatureGroup.TabIndex = 15;
            this.FeatureGroup.TabStop = false;
            this.FeatureGroup.Text = "Features";
            // 
            // CsBtn
            // 
            this.CsBtn.Location = new System.Drawing.Point(6, 112);
            this.CsBtn.Name = "CsBtn";
            this.CsBtn.Size = new System.Drawing.Size(211, 34);
            this.CsBtn.TabIndex = 2;
            this.CsBtn.Text = "Cash Shop";
            this.CsBtn.UseVisualStyleBackColor = true;
            this.CsBtn.Click += new System.EventHandler(this.CsBtn_Click);
            // 
            // MoveBtn
            // 
            this.MoveBtn.Location = new System.Drawing.Point(6, 65);
            this.MoveBtn.Name = "MoveBtn";
            this.MoveBtn.Size = new System.Drawing.Size(211, 34);
            this.MoveBtn.TabIndex = 1;
            this.MoveBtn.Text = "Send Movement";
            this.MoveBtn.UseVisualStyleBackColor = true;
            this.MoveBtn.Click += new System.EventHandler(this.MoveBtn_Click);
            // 
            // CcBtn
            // 
            this.CcBtn.Location = new System.Drawing.Point(6, 19);
            this.CcBtn.Name = "CcBtn";
            this.CcBtn.Size = new System.Drawing.Size(211, 34);
            this.CcBtn.TabIndex = 0;
            this.CcBtn.Text = "Change Channel";
            this.CcBtn.UseVisualStyleBackColor = true;
            this.CcBtn.Click += new System.EventHandler(this.CcBtn_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.AccountTab);
            this.Tabs.Controls.Add(this.PacketTab);
            this.Tabs.Controls.Add(this.RushTab);
            this.Tabs.Controls.Add(this.LogTab);
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(639, 341);
            this.Tabs.TabIndex = 16;
            // 
            // AccountTab
            // 
            this.AccountTab.Controls.Add(this.AccountGroup);
            this.AccountTab.Controls.Add(this.FeatureGroup);
            this.AccountTab.Controls.Add(this.ConnectBtn);
            this.AccountTab.Controls.Add(this.AutoRestart);
            this.AccountTab.Controls.Add(this.StatGroup);
            this.AccountTab.Location = new System.Drawing.Point(4, 22);
            this.AccountTab.Name = "AccountTab";
            this.AccountTab.Padding = new System.Windows.Forms.Padding(3);
            this.AccountTab.Size = new System.Drawing.Size(631, 315);
            this.AccountTab.TabIndex = 0;
            this.AccountTab.Text = "Account";
            this.AccountTab.UseVisualStyleBackColor = true;
            // 
            // PacketTab
            // 
            this.PacketTab.Controls.Add(this.PacketView);
            this.PacketTab.Location = new System.Drawing.Point(4, 22);
            this.PacketTab.Name = "PacketTab";
            this.PacketTab.Padding = new System.Windows.Forms.Padding(3);
            this.PacketTab.Size = new System.Drawing.Size(631, 315);
            this.PacketTab.TabIndex = 1;
            this.PacketTab.Text = "Packets";
            this.PacketTab.UseVisualStyleBackColor = true;
            // 
            // PacketView
            // 
            this.PacketView.Location = new System.Drawing.Point(3, 3);
            this.PacketView.Name = "PacketView";
            this.PacketView.Size = new System.Drawing.Size(628, 312);
            this.PacketView.TabIndex = 0;
            // 
            // RushTab
            // 
            this.RushTab.Controls.Add(this.RushStatusBar);
            this.RushTab.Controls.Add(this.RushTree);
            this.RushTab.Location = new System.Drawing.Point(4, 22);
            this.RushTab.Name = "RushTab";
            this.RushTab.Size = new System.Drawing.Size(631, 315);
            this.RushTab.TabIndex = 3;
            this.RushTab.Text = "Rusher";
            this.RushTab.UseVisualStyleBackColor = true;
            // 
            // RushStatusBar
            // 
            this.RushStatusBar.BackColor = System.Drawing.Color.White;
            this.RushStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MapStatusLbl,
            this.MapStatus,
            this.StatusSep,
            this.RushStatusLbl,
            this.RushStatus,
            this.StatusSep2,
            this.toolStripDropDownButton1});
            this.RushStatusBar.Location = new System.Drawing.Point(0, 293);
            this.RushStatusBar.Name = "RushStatusBar";
            this.RushStatusBar.Size = new System.Drawing.Size(631, 22);
            this.RushStatusBar.TabIndex = 1;
            this.RushStatusBar.Text = "Status";
            // 
            // MapStatusLbl
            // 
            this.MapStatusLbl.Name = "MapStatusLbl";
            this.MapStatusLbl.Size = new System.Drawing.Size(34, 17);
            this.MapStatusLbl.Text = "Map:";
            // 
            // MapStatus
            // 
            this.MapStatus.Name = "MapStatus";
            this.MapStatus.Size = new System.Drawing.Size(51, 17);
            this.MapStatus.Text = "Uknown";
            // 
            // StatusSep
            // 
            this.StatusSep.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusSep.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.StatusSep.Name = "StatusSep";
            this.StatusSep.Size = new System.Drawing.Size(4, 17);
            // 
            // RushStatusLbl
            // 
            this.RushStatusLbl.Name = "RushStatusLbl";
            this.RushStatusLbl.Size = new System.Drawing.Size(42, 17);
            this.RushStatusLbl.Text = "Status:";
            // 
            // RushStatus
            // 
            this.RushStatus.Name = "RushStatus";
            this.RushStatus.Size = new System.Drawing.Size(26, 17);
            this.RushStatus.Text = "Idle";
            // 
            // StatusSep2
            // 
            this.StatusSep2.Name = "StatusSep2";
            this.StatusSep2.Size = new System.Drawing.Size(344, 17);
            this.StatusSep2.Spring = true;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elliniaToolStripMenuItem,
            this.henesysToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(115, 20);
            this.toolStripDropDownButton1.Text = "Continenent Rush";
            this.toolStripDropDownButton1.ToolTipText = "Continenent Rush";
            // 
            // elliniaToolStripMenuItem
            // 
            this.elliniaToolStripMenuItem.Name = "elliniaToolStripMenuItem";
            this.elliniaToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.elliniaToolStripMenuItem.Text = "Ellinia";
            // 
            // henesysToolStripMenuItem
            // 
            this.henesysToolStripMenuItem.Name = "henesysToolStripMenuItem";
            this.henesysToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.henesysToolStripMenuItem.Text = "Henesys";
            // 
            // RushTree
            // 
            this.RushTree.Location = new System.Drawing.Point(2, 3);
            this.RushTree.Name = "RushTree";
            this.RushTree.Size = new System.Drawing.Size(626, 290);
            this.RushTree.TabIndex = 0;
            // 
            // LogTab
            // 
            this.LogTab.Controls.Add(this.LogText);
            this.LogTab.Location = new System.Drawing.Point(4, 22);
            this.LogTab.Name = "LogTab";
            this.LogTab.Size = new System.Drawing.Size(631, 315);
            this.LogTab.TabIndex = 2;
            this.LogTab.Text = "Logs";
            this.LogTab.UseVisualStyleBackColor = true;
            // 
            // LogText
            // 
            this.LogText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LogText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogText.Location = new System.Drawing.Point(-1, -1);
            this.LogText.Margin = new System.Windows.Forms.Padding(0);
            this.LogText.MaxLength = 0;
            this.LogText.Multiline = true;
            this.LogText.Name = "LogText";
            this.LogText.ReadOnly = true;
            this.LogText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogText.Size = new System.Drawing.Size(632, 318);
            this.LogText.TabIndex = 12;
            // 
            // PacketInput
            // 
            this.PacketInput.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.PacketInput.Location = new System.Drawing.Point(3, 350);
            this.PacketInput.Name = "PacketInput";
            this.PacketInput.Size = new System.Drawing.Size(555, 21);
            this.PacketInput.TabIndex = 20;
            // 
            // DelayInput
            // 
            this.DelayInput.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.DelayInput.Location = new System.Drawing.Point(513, 350);
            this.DelayInput.Name = "DelayInput";
            this.DelayInput.Size = new System.Drawing.Size(45, 21);
            this.DelayInput.TabIndex = 22;
            this.DelayInput.Visible = false;
            // 
            // SendMenu
            // 
            this.SendMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SendMenuItem,
            this.SpamMenuItem});
            this.SendMenu.Name = "SendMenu";
            this.SendMenu.Size = new System.Drawing.Size(105, 48);
            // 
            // SendMenuItem
            // 
            this.SendMenuItem.Checked = true;
            this.SendMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SendMenuItem.Name = "SendMenuItem";
            this.SendMenuItem.Size = new System.Drawing.Size(104, 22);
            this.SendMenuItem.Text = "Send";
            this.SendMenuItem.Click += new System.EventHandler(this.SendMenuItem_Click);
            // 
            // SpamMenuItem
            // 
            this.SpamMenuItem.Name = "SpamMenuItem";
            this.SpamMenuItem.Size = new System.Drawing.Size(104, 22);
            this.SpamMenuItem.Text = "Spam";
            this.SpamMenuItem.Click += new System.EventHandler(this.SpamMenuItem_Click);
            // 
            // SendSpamBtn
            // 
            this.SendSpamBtn.AutoSize = true;
            this.SendSpamBtn.ContextMenuStrip = this.SendMenu;
            this.SendSpamBtn.Location = new System.Drawing.Point(564, 347);
            this.SendSpamBtn.Name = "SendSpamBtn";
            this.SendSpamBtn.Size = new System.Drawing.Size(75, 27);
            this.SendSpamBtn.SplitMenuStrip = this.SendMenu;
            this.SendSpamBtn.TabIndex = 23;
            this.SendSpamBtn.Text = "Send";
            this.SendSpamBtn.UseVisualStyleBackColor = true;
            this.SendSpamBtn.Click += new System.EventHandler(this.SendSpamBtn_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.SendSpamBtn);
            this.Controls.Add(this.DelayInput);
            this.Controls.Add(this.PacketInput);
            this.Controls.Add(this.Tabs);
            this.Name = "ClientForm";
            this.Size = new System.Drawing.Size(639, 375);
            this.AccountGroup.ResumeLayout(false);
            this.AccountGroup.PerformLayout();
            this.StatGroup.ResumeLayout(false);
            this.StatGroup.PerformLayout();
            this.FeatureGroup.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.AccountTab.ResumeLayout(false);
            this.AccountTab.PerformLayout();
            this.PacketTab.ResumeLayout(false);
            this.RushTab.ResumeLayout(false);
            this.RushTab.PerformLayout();
            this.RushStatusBar.ResumeLayout(false);
            this.RushStatusBar.PerformLayout();
            this.LogTab.ResumeLayout(false);
            this.LogTab.PerformLayout();
            this.SendMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox AccountGroup;
        private System.Windows.Forms.TextBox UserInput;
        private System.Windows.Forms.TextBox PassInput;
        private System.Windows.Forms.TextBox PicInput;
        private System.Windows.Forms.ComboBox WorldList;
        private System.Windows.Forms.ComboBox ChannelList;
        public System.Windows.Forms.ComboBox SelectList;
        private System.Windows.Forms.TextBox CharInput;
        private System.Windows.Forms.ComboBox ModeList;
        public System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.GroupBox StatGroup;
        private System.Windows.Forms.Label MapStat;
        private System.Windows.Forms.Label ChannelStat;
        private System.Windows.Forms.Label LevelStat;
        public System.Windows.Forms.Label NameStat;
        private System.Windows.Forms.Label MapLbl;
        private System.Windows.Forms.Label ChannelLbl;
        private System.Windows.Forms.Label LevelLbl;
        private System.Windows.Forms.Label NameLbl;
        public System.Windows.Forms.CheckBox AutoRestart;
        private System.Windows.Forms.GroupBox FeatureGroup;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage AccountTab;
        private System.Windows.Forms.TabPage LogTab;
        private System.Windows.Forms.TabPage RushTab;
        public System.Windows.Forms.TextBox LogText;
        private System.Windows.Forms.TreeView RushTree;
        private System.Windows.Forms.StatusStrip RushStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel MapStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel MapStatus;
        private System.Windows.Forms.ToolStripStatusLabel StatusSep;
        private System.Windows.Forms.ToolStripStatusLabel RushStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel RushStatus;
        private System.Windows.Forms.ToolStripStatusLabel StatusSep2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem elliniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem henesysToolStripMenuItem;
        private System.Windows.Forms.Button CcBtn;
        private System.Windows.Forms.Button MoveBtn;
        private System.Windows.Forms.Button CsBtn;
        private System.Windows.Forms.TextBox DelayInput;
        private System.Windows.Forms.TextBox PacketInput;
        private Tools.SplitButton SendSpamBtn;
        private System.Windows.Forms.ContextMenuStrip SendMenu;
        private System.Windows.Forms.ToolStripMenuItem SendMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpamMenuItem;
        private System.Windows.Forms.TabPage PacketTab;
        private System.Windows.Forms.Label Mesos;
        private System.Windows.Forms.Label MesoStatus;
        private Forms.PacketView PacketView;
    }
}
