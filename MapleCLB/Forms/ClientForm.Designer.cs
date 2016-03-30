using MapleCLB.Forms.Tabs;

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
            this.WorkingStatus = new System.Windows.Forms.Label();
            this.Working = new System.Windows.Forms.Label();
            this.PeopleStatus = new System.Windows.Forms.Label();
            this.People = new System.Windows.Forms.Label();
            this.ItemsStatus = new System.Windows.Forms.Label();
            this.Items = new System.Windows.Forms.Label();
            this.ExpStatus = new System.Windows.Forms.Label();
            this.Exp = new System.Windows.Forms.Label();
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
            this.FM_Functions = new System.Windows.Forms.Button();
            this.CcBtn = new System.Windows.Forms.Button();
            this.CsBtn = new System.Windows.Forms.Button();
            this.MoveBtn = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.AccountTab = new System.Windows.Forms.TabPage();
            this.InitTestBtn = new System.Windows.Forms.Button();
            this.PacketTab = new System.Windows.Forms.TabPage();
            this.PacketView = new MapleCLB.Forms.Tabs.PacketTab();
            this.RushTab = new System.Windows.Forms.TabPage();
            this.RushView = new MapleCLB.Forms.Tabs.RusherTab();
            this.LogTab = new System.Windows.Forms.TabPage();
            this.LogText = new System.Windows.Forms.TextBox();
            this.InventoryTab = new System.Windows.Forms.TabPage();
            this.inventoryTab1 = new MapleCLB.Forms.InventoryTab();
            this.PacketInput = new System.Windows.Forms.TextBox();
            this.DelayInput = new System.Windows.Forms.TextBox();
            this.SendMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpTimer = new System.Windows.Forms.Timer(this.components);
            this.SendSpamBtn = new MapleCLB.Forms.SplitButton();
            this.AccountGroup.SuspendLayout();
            this.StatGroup.SuspendLayout();
            this.FeatureGroup.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.AccountTab.SuspendLayout();
            this.PacketTab.SuspendLayout();
            this.RushTab.SuspendLayout();
            this.LogTab.SuspendLayout();
            this.InventoryTab.SuspendLayout();
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
            this.StatGroup.Controls.Add(this.WorkingStatus);
            this.StatGroup.Controls.Add(this.Working);
            this.StatGroup.Controls.Add(this.PeopleStatus);
            this.StatGroup.Controls.Add(this.People);
            this.StatGroup.Controls.Add(this.ItemsStatus);
            this.StatGroup.Controls.Add(this.Items);
            this.StatGroup.Controls.Add(this.ExpStatus);
            this.StatGroup.Controls.Add(this.Exp);
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
            this.StatGroup.Location = new System.Drawing.Point(371, 6);
            this.StatGroup.Name = "StatGroup";
            this.StatGroup.Size = new System.Drawing.Size(254, 253);
            this.StatGroup.TabIndex = 2;
            this.StatGroup.TabStop = false;
            this.StatGroup.Text = "Stats";
            // 
            // WorkingStatus
            // 
            this.WorkingStatus.AutoSize = true;
            this.WorkingStatus.Location = new System.Drawing.Point(67, 166);
            this.WorkingStatus.Name = "WorkingStatus";
            this.WorkingStatus.Size = new System.Drawing.Size(49, 13);
            this.WorkingStatus.TabIndex = 24;
            this.WorkingStatus.Text = "00:00:00";
            // 
            // Working
            // 
            this.Working.AutoSize = true;
            this.Working.Location = new System.Drawing.Point(6, 166);
            this.Working.Name = "Working";
            this.Working.Size = new System.Drawing.Size(50, 13);
            this.Working.TabIndex = 23;
            this.Working.Text = "Working:";
            // 
            // PeopleStatus
            // 
            this.PeopleStatus.AutoSize = true;
            this.PeopleStatus.Location = new System.Drawing.Point(67, 148);
            this.PeopleStatus.Name = "PeopleStatus";
            this.PeopleStatus.Size = new System.Drawing.Size(57, 13);
            this.PeopleStatus.TabIndex = 22;
            this.PeopleStatus.Text = "Not Active";
            // 
            // People
            // 
            this.People.AutoSize = true;
            this.People.Location = new System.Drawing.Point(6, 148);
            this.People.Name = "People";
            this.People.Size = new System.Drawing.Size(43, 13);
            this.People.TabIndex = 21;
            this.People.Text = "People:";
            // 
            // ItemsStatus
            // 
            this.ItemsStatus.AutoSize = true;
            this.ItemsStatus.Location = new System.Drawing.Point(67, 130);
            this.ItemsStatus.Name = "ItemsStatus";
            this.ItemsStatus.Size = new System.Drawing.Size(16, 13);
            this.ItemsStatus.TabIndex = 20;
            this.ItemsStatus.Text = "-1";
            // 
            // Items
            // 
            this.Items.AutoSize = true;
            this.Items.Location = new System.Drawing.Point(6, 130);
            this.Items.Name = "Items";
            this.Items.Size = new System.Drawing.Size(35, 13);
            this.Items.TabIndex = 19;
            this.Items.Text = "Items:";
            // 
            // ExpStatus
            // 
            this.ExpStatus.AutoSize = true;
            this.ExpStatus.Location = new System.Drawing.Point(67, 112);
            this.ExpStatus.Name = "ExpStatus";
            this.ExpStatus.Size = new System.Drawing.Size(16, 13);
            this.ExpStatus.TabIndex = 18;
            this.ExpStatus.Text = "-1";
            // 
            // Exp
            // 
            this.Exp.AutoSize = true;
            this.Exp.Location = new System.Drawing.Point(6, 112);
            this.Exp.Name = "Exp";
            this.Exp.Size = new System.Drawing.Size(28, 13);
            this.Exp.TabIndex = 17;
            this.Exp.Text = "Exp:";
            // 
            // MesoStatus
            // 
            this.MesoStatus.AutoSize = true;
            this.MesoStatus.Location = new System.Drawing.Point(67, 94);
            this.MesoStatus.Name = "MesoStatus";
            this.MesoStatus.Size = new System.Drawing.Size(16, 13);
            this.MesoStatus.TabIndex = 16;
            this.MesoStatus.Text = "-1";
            // 
            // Mesos
            // 
            this.Mesos.AutoSize = true;
            this.Mesos.Location = new System.Drawing.Point(6, 94);
            this.Mesos.Name = "Mesos";
            this.Mesos.Size = new System.Drawing.Size(41, 13);
            this.Mesos.TabIndex = 8;
            this.Mesos.Text = "Mesos:";
            // 
            // MapStat
            // 
            this.MapStat.AutoSize = true;
            this.MapStat.Location = new System.Drawing.Point(67, 76);
            this.MapStat.Name = "MapStat";
            this.MapStat.Size = new System.Drawing.Size(16, 13);
            this.MapStat.TabIndex = 7;
            this.MapStat.Text = "-1";
            // 
            // ChannelStat
            // 
            this.ChannelStat.AutoSize = true;
            this.ChannelStat.Location = new System.Drawing.Point(67, 58);
            this.ChannelStat.Name = "ChannelStat";
            this.ChannelStat.Size = new System.Drawing.Size(16, 13);
            this.ChannelStat.TabIndex = 6;
            this.ChannelStat.Text = "-1";
            // 
            // LevelStat
            // 
            this.LevelStat.AutoSize = true;
            this.LevelStat.Location = new System.Drawing.Point(67, 40);
            this.LevelStat.Name = "LevelStat";
            this.LevelStat.Size = new System.Drawing.Size(16, 13);
            this.LevelStat.TabIndex = 5;
            this.LevelStat.Text = "-1";
            // 
            // NameStat
            // 
            this.NameStat.AutoSize = true;
            this.NameStat.Location = new System.Drawing.Point(67, 22);
            this.NameStat.Name = "NameStat";
            this.NameStat.Size = new System.Drawing.Size(53, 13);
            this.NameStat.TabIndex = 4;
            this.NameStat.Text = "Unknown";
            // 
            // MapLbl
            // 
            this.MapLbl.AutoSize = true;
            this.MapLbl.Location = new System.Drawing.Point(6, 76);
            this.MapLbl.Name = "MapLbl";
            this.MapLbl.Size = new System.Drawing.Size(31, 13);
            this.MapLbl.TabIndex = 3;
            this.MapLbl.Text = "Map:";
            // 
            // ChannelLbl
            // 
            this.ChannelLbl.AutoSize = true;
            this.ChannelLbl.Location = new System.Drawing.Point(6, 58);
            this.ChannelLbl.Name = "ChannelLbl";
            this.ChannelLbl.Size = new System.Drawing.Size(49, 13);
            this.ChannelLbl.TabIndex = 2;
            this.ChannelLbl.Text = "Channel:";
            // 
            // LevelLbl
            // 
            this.LevelLbl.AutoSize = true;
            this.LevelLbl.Location = new System.Drawing.Point(6, 40);
            this.LevelLbl.Name = "LevelLbl";
            this.LevelLbl.Size = new System.Drawing.Size(36, 13);
            this.LevelLbl.TabIndex = 1;
            this.LevelLbl.Text = "Level:";
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(6, 22);
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
            this.FeatureGroup.Controls.Add(this.FM_Functions);
            this.FeatureGroup.Controls.Add(this.CcBtn);
            this.FeatureGroup.Location = new System.Drawing.Point(209, 6);
            this.FeatureGroup.Name = "FeatureGroup";
            this.FeatureGroup.Size = new System.Drawing.Size(145, 137);
            this.FeatureGroup.TabIndex = 15;
            this.FeatureGroup.TabStop = false;
            this.FeatureGroup.Text = "Features";
            // 
            // FM_Functions
            // 
            this.FM_Functions.Location = new System.Drawing.Point(6, 99);
            this.FM_Functions.Name = "FM_Functions";
            this.FM_Functions.Size = new System.Drawing.Size(133, 34);
            this.FM_Functions.TabIndex = 17;
            this.FM_Functions.Text = "FM Functions";
            this.FM_Functions.UseVisualStyleBackColor = true;
            this.FM_Functions.Click += new System.EventHandler(this.FMFunctions_Click);
            // 
            // CcBtn
            // 
            this.CcBtn.Location = new System.Drawing.Point(6, 19);
            this.CcBtn.Name = "CcBtn";
            this.CcBtn.Size = new System.Drawing.Size(133, 34);
            this.CcBtn.TabIndex = 0;
            this.CcBtn.Text = "Change Channel";
            this.CcBtn.UseVisualStyleBackColor = true;
            this.CcBtn.Click += new System.EventHandler(this.CcBtn_Click);
            // 
            // CsBtn
            // 
            this.CsBtn.Location = new System.Drawing.Point(101, 261);
            this.CsBtn.Name = "CsBtn";
            this.CsBtn.Size = new System.Drawing.Size(57, 34);
            this.CsBtn.TabIndex = 2;
            this.CsBtn.Text = "Cash Shop";
            this.CsBtn.UseVisualStyleBackColor = true;
            this.CsBtn.Click += new System.EventHandler(this.CsBtn_Click);
            // 
            // MoveBtn
            // 
            this.MoveBtn.Location = new System.Drawing.Point(12, 261);
            this.MoveBtn.Name = "MoveBtn";
            this.MoveBtn.Size = new System.Drawing.Size(68, 34);
            this.MoveBtn.TabIndex = 1;
            this.MoveBtn.Text = "Send Movement";
            this.MoveBtn.UseVisualStyleBackColor = true;
            this.MoveBtn.Click += new System.EventHandler(this.MoveBtn_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.AccountTab);
            this.Tabs.Controls.Add(this.PacketTab);
            this.Tabs.Controls.Add(this.RushTab);
            this.Tabs.Controls.Add(this.LogTab);
            this.Tabs.Controls.Add(this.InventoryTab);
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Margin = new System.Windows.Forms.Padding(0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(689, 346);
            this.Tabs.TabIndex = 16;
            // 
            // AccountTab
            // 
            this.AccountTab.Controls.Add(this.CsBtn);
            this.AccountTab.Controls.Add(this.MoveBtn);
            this.AccountTab.Controls.Add(this.InitTestBtn);
            this.AccountTab.Controls.Add(this.AccountGroup);
            this.AccountTab.Controls.Add(this.FeatureGroup);
            this.AccountTab.Controls.Add(this.ConnectBtn);
            this.AccountTab.Controls.Add(this.AutoRestart);
            this.AccountTab.Controls.Add(this.StatGroup);
            this.AccountTab.Location = new System.Drawing.Point(4, 22);
            this.AccountTab.Name = "AccountTab";
            this.AccountTab.Padding = new System.Windows.Forms.Padding(3);
            this.AccountTab.Size = new System.Drawing.Size(681, 320);
            this.AccountTab.TabIndex = 0;
            this.AccountTab.Text = "Account";
            this.AccountTab.UseVisualStyleBackColor = true;
            // 
            // InitTestBtn
            // 
            this.InitTestBtn.Location = new System.Drawing.Point(182, 261);
            this.InitTestBtn.Name = "InitTestBtn";
            this.InitTestBtn.Size = new System.Drawing.Size(84, 35);
            this.InitTestBtn.TabIndex = 16;
            this.InitTestBtn.Text = "Init Test";
            this.InitTestBtn.UseVisualStyleBackColor = true;
            this.InitTestBtn.Click += new System.EventHandler(this.InitTestBtn_Click);
            // 
            // PacketTab
            // 
            this.PacketTab.Controls.Add(this.PacketView);
            this.PacketTab.Location = new System.Drawing.Point(4, 22);
            this.PacketTab.Name = "PacketTab";
            this.PacketTab.Padding = new System.Windows.Forms.Padding(3);
            this.PacketTab.Size = new System.Drawing.Size(681, 320);
            this.PacketTab.TabIndex = 1;
            this.PacketTab.Text = "Packets";
            this.PacketTab.UseVisualStyleBackColor = true;
            // 
            // PacketView
            // 
            this.PacketView.AutoSize = true;
            this.PacketView.Location = new System.Drawing.Point(0, 1);
            this.PacketView.Margin = new System.Windows.Forms.Padding(0);
            this.PacketView.Name = "PacketView";
            this.PacketView.Size = new System.Drawing.Size(681, 319);
            this.PacketView.TabIndex = 0;
            // 
            // RushTab
            // 
            this.RushTab.Controls.Add(this.RushView);
            this.RushTab.Location = new System.Drawing.Point(4, 22);
            this.RushTab.Name = "RushTab";
            this.RushTab.Size = new System.Drawing.Size(681, 320);
            this.RushTab.TabIndex = 3;
            this.RushTab.Text = "Rusher";
            this.RushTab.UseVisualStyleBackColor = true;
            // 
            // RushView
            // 
            this.RushView.Location = new System.Drawing.Point(-1, -1);
            this.RushView.Name = "RushView";
            this.RushView.Size = new System.Drawing.Size(682, 321);
            this.RushView.TabIndex = 0;
            // 
            // LogTab
            // 
            this.LogTab.Controls.Add(this.LogText);
            this.LogTab.Location = new System.Drawing.Point(4, 22);
            this.LogTab.Name = "LogTab";
            this.LogTab.Size = new System.Drawing.Size(681, 320);
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
            this.LogText.Size = new System.Drawing.Size(682, 323);
            this.LogText.TabIndex = 12;
            // 
            // InventoryTab
            // 
            this.InventoryTab.Controls.Add(this.inventoryTab1);
            this.InventoryTab.Location = new System.Drawing.Point(4, 22);
            this.InventoryTab.Name = "InventoryTab";
            this.InventoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.InventoryTab.Size = new System.Drawing.Size(681, 320);
            this.InventoryTab.TabIndex = 4;
            this.InventoryTab.Text = "Inventory";
            this.InventoryTab.UseVisualStyleBackColor = true;
            // 
            // inventoryTab1
            // 
            this.inventoryTab1.Location = new System.Drawing.Point(158, 6);
            this.inventoryTab1.Name = "inventoryTab1";
            this.inventoryTab1.Size = new System.Drawing.Size(324, 285);
            this.inventoryTab1.TabIndex = 0;
            // 
            // PacketInput
            // 
            this.PacketInput.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.PacketInput.Location = new System.Drawing.Point(3, 353);
            this.PacketInput.Name = "PacketInput";
            this.PacketInput.Size = new System.Drawing.Size(601, 21);
            this.PacketInput.TabIndex = 20;
            // 
            // DelayInput
            // 
            this.DelayInput.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.DelayInput.Location = new System.Drawing.Point(559, 353);
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
            // UpTimer
            // 
            this.UpTimer.Interval = 1000;
            this.UpTimer.Tick += new System.EventHandler(this.UpTimer_Tick);
            // 
            // SendSpamBtn
            // 
            this.SendSpamBtn.AutoSize = true;
            this.SendSpamBtn.ContextMenuStrip = this.SendMenu;
            this.SendSpamBtn.Location = new System.Drawing.Point(610, 350);
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
            this.Size = new System.Drawing.Size(689, 377);
            this.AccountGroup.ResumeLayout(false);
            this.AccountGroup.PerformLayout();
            this.StatGroup.ResumeLayout(false);
            this.StatGroup.PerformLayout();
            this.FeatureGroup.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.AccountTab.ResumeLayout(false);
            this.AccountTab.PerformLayout();
            this.PacketTab.ResumeLayout(false);
            this.PacketTab.PerformLayout();
            this.RushTab.ResumeLayout(false);
            this.LogTab.ResumeLayout(false);
            this.LogTab.PerformLayout();
            this.InventoryTab.ResumeLayout(false);
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
        private System.Windows.Forms.Button CcBtn;
        private System.Windows.Forms.Button MoveBtn;
        private System.Windows.Forms.Button CsBtn;
        private System.Windows.Forms.TextBox DelayInput;
        private System.Windows.Forms.TextBox PacketInput;
        private SplitButton SendSpamBtn;
        private System.Windows.Forms.ContextMenuStrip SendMenu;
        private System.Windows.Forms.ToolStripMenuItem SendMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpamMenuItem;
        private System.Windows.Forms.TabPage PacketTab;
        private System.Windows.Forms.Label Mesos;
        private System.Windows.Forms.Label MesoStatus;
        private PacketTab PacketView;
        private System.Windows.Forms.Button InitTestBtn;
        private System.Windows.Forms.Label ExpStatus;
        private System.Windows.Forms.Label Exp;
        private System.Windows.Forms.Button FM_Functions;
        private System.Windows.Forms.Label PeopleStatus;
        private System.Windows.Forms.Label People;
        private System.Windows.Forms.Label ItemsStatus;
        private System.Windows.Forms.Label Items;
        private System.Windows.Forms.Label WorkingStatus;
        private System.Windows.Forms.Label Working;
        private RusherTab RushView;
        private System.Windows.Forms.Timer UpTimer;
        private System.Windows.Forms.TabPage InventoryTab;
        private InventoryTab inventoryTab1;
    }
}
