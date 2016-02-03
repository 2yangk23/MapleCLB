using System.Windows.Forms;
using MapleCLB.Tools;
namespace MapleCLB
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.infoGroup = new System.Windows.Forms.GroupBox();
            this.selType = new System.Windows.Forms.ComboBox();
            this.loginMode = new System.Windows.Forms.ComboBox();
            this.character = new System.Windows.Forms.TextBox();
            this.channel = new System.Windows.Forms.ComboBox();
            this.username = new System.Windows.Forms.TextBox();
            this.world = new System.Windows.Forms.ComboBox();
            this.password = new System.Windows.Forms.TextBox();
            this.pic = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            this.disconnect = new System.Windows.Forms.Button();
            this.aRestart = new System.Windows.Forms.CheckBox();
            this.sendPacket = new System.Windows.Forms.TextBox();
            this.aCS = new System.Windows.Forms.CheckBox();
            this.sendMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sMenuSend = new System.Windows.Forms.ToolStripMenuItem();
            this.sMenuSpam = new System.Windows.Forms.ToolStripMenuItem();
            this.delay = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.CC = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.Label();
            this.sendSpam = new MapleCLB.Tools.SplitButton();
            this.EnterCS = new System.Windows.Forms.Button();
            this.infoGroup.SuspendLayout();
            this.sendMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoGroup
            // 
            this.infoGroup.Controls.Add(this.selType);
            this.infoGroup.Controls.Add(this.loginMode);
            this.infoGroup.Controls.Add(this.character);
            this.infoGroup.Controls.Add(this.channel);
            this.infoGroup.Controls.Add(this.username);
            this.infoGroup.Controls.Add(this.world);
            this.infoGroup.Controls.Add(this.password);
            this.infoGroup.Controls.Add(this.pic);
            this.infoGroup.Location = new System.Drawing.Point(12, 10);
            this.infoGroup.Name = "infoGroup";
            this.infoGroup.Size = new System.Drawing.Size(198, 153);
            this.infoGroup.TabIndex = 9;
            this.infoGroup.TabStop = false;
            this.infoGroup.Text = "Account Information";
            // 
            // selType
            // 
            this.selType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selType.FormattingEnabled = true;
            this.selType.Items.AddRange(new object[] {
            "Slot",
            "IGN",
            "UID"});
            this.selType.Location = new System.Drawing.Point(6, 97);
            this.selType.Name = "selType";
            this.selType.Size = new System.Drawing.Size(61, 21);
            this.selType.TabIndex = 8;
            // 
            // loginMode
            // 
            this.loginMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loginMode.FormattingEnabled = true;
            this.loginMode.Items.AddRange(new object[] {
            "Login Mode",
            "ShopBot Mode"});
            this.loginMode.Location = new System.Drawing.Point(6, 124);
            this.loginMode.Name = "loginMode";
            this.loginMode.Size = new System.Drawing.Size(191, 21);
            this.loginMode.TabIndex = 23;
            // 
            // character
            // 
            this.character.Location = new System.Drawing.Point(73, 98);
            this.character.MaxLength = 12;
            this.character.Name = "character";
            this.character.Size = new System.Drawing.Size(118, 20);
            this.character.TabIndex = 7;
            // 
            // channel
            // 
            this.channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.channel.FormattingEnabled = true;
            this.channel.Items.AddRange(new object[] {
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
            this.channel.Location = new System.Drawing.Point(121, 71);
            this.channel.Name = "channel";
            this.channel.Size = new System.Drawing.Size(70, 21);
            this.channel.TabIndex = 6;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(6, 19);
            this.username.MaxLength = 50;
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(185, 20);
            this.username.TabIndex = 2;
            // 
            // world
            // 
            this.world.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.world.FormattingEnabled = true;
            this.world.Items.AddRange(new object[] {
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
            this.world.Location = new System.Drawing.Point(6, 71);
            this.world.Name = "world";
            this.world.Size = new System.Drawing.Size(109, 21);
            this.world.TabIndex = 5;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(6, 45);
            this.password.MaxLength = 20;
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(109, 20);
            this.password.TabIndex = 3;
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(121, 45);
            this.pic.MaxLength = 16;
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(70, 20);
            this.pic.TabIndex = 4;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(12, 169);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(198, 27);
            this.connect.TabIndex = 0;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.EnabledChanged += new System.EventHandler(this.connect_EnabledChanged);
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.log.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log.Location = new System.Drawing.Point(227, 12);
            this.log.MaxLength = 0;
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(370, 259);
            this.log.TabIndex = 11;
            // 
            // disconnect
            // 
            this.disconnect.Enabled = false;
            this.disconnect.Location = new System.Drawing.Point(12, 202);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(198, 27);
            this.disconnect.TabIndex = 12;
            this.disconnect.Text = " Disconnect";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // aRestart
            // 
            this.aRestart.AutoSize = true;
            this.aRestart.Location = new System.Drawing.Point(12, 270);
            this.aRestart.Name = "aRestart";
            this.aRestart.Size = new System.Drawing.Size(85, 17);
            this.aRestart.TabIndex = 13;
            this.aRestart.Text = "Auto Restart";
            this.aRestart.UseVisualStyleBackColor = true;
            // 
            // sendPacket
            // 
            this.sendPacket.Font = new System.Drawing.Font("Consolas", 9F);
            this.sendPacket.Location = new System.Drawing.Point(8, 293);
            this.sendPacket.Name = "sendPacket";
            this.sendPacket.Size = new System.Drawing.Size(505, 22);
            this.sendPacket.TabIndex = 14;
            // 
            // aCS
            // 
            this.aCS.AutoSize = true;
            this.aCS.Location = new System.Drawing.Point(103, 270);
            this.aCS.Name = "aCS";
            this.aCS.Size = new System.Drawing.Size(65, 17);
            this.aCS.TabIndex = 16;
            this.aCS.Text = "Auto CS";
            this.aCS.UseVisualStyleBackColor = true;
            // 
            // sendMenu
            // 
            this.sendMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sMenuSend,
            this.sMenuSpam});
            this.sendMenu.Name = "sendMenu";
            this.sendMenu.Size = new System.Drawing.Size(105, 48);
            // 
            // sMenuSend
            // 
            this.sMenuSend.Checked = true;
            this.sMenuSend.CheckOnClick = true;
            this.sMenuSend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sMenuSend.Name = "sMenuSend";
            this.sMenuSend.Size = new System.Drawing.Size(104, 22);
            this.sMenuSend.Text = "Send";
            this.sMenuSend.Click += new System.EventHandler(this.sMenuSend_Click);
            // 
            // sMenuSpam
            // 
            this.sMenuSpam.CheckOnClick = true;
            this.sMenuSpam.Name = "sMenuSpam";
            this.sMenuSpam.Size = new System.Drawing.Size(104, 22);
            this.sMenuSpam.Text = "Spam";
            this.sMenuSpam.Click += new System.EventHandler(this.sMenuSpam_Click);
            // 
            // delay
            // 
            this.delay.Font = new System.Drawing.Font("Consolas", 9F);
            this.delay.Location = new System.Drawing.Point(468, 293);
            this.delay.Name = "delay";
            this.delay.Size = new System.Drawing.Size(45, 22);
            this.delay.TabIndex = 19;
            this.delay.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 321);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 27);
            this.button1.TabIndex = 20;
            this.button1.Text = " Send Move";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CC
            // 
            this.CC.Location = new System.Drawing.Point(12, 321);
            this.CC.Name = "CC";
            this.CC.Size = new System.Drawing.Size(198, 27);
            this.CC.TabIndex = 21;
            this.CC.Text = "CC";
            this.CC.UseVisualStyleBackColor = true;
            this.CC.Click += new System.EventHandler(this.CC_Click);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(628, 29);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(41, 13);
            this.name.TabIndex = 22;
            this.name.Text = "Name: ";
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // sendSpam
            // 
            this.sendSpam.AutoSize = true;
            this.sendSpam.ContextMenuStrip = this.sendMenu;
            this.sendSpam.Location = new System.Drawing.Point(519, 293);
            this.sendSpam.Name = "sendSpam";
            this.sendSpam.Size = new System.Drawing.Size(78, 25);
            this.sendSpam.SplitMenuStrip = this.sendMenu;
            this.sendSpam.TabIndex = 17;
            this.sendSpam.Text = "Send";
            this.sendSpam.UseVisualStyleBackColor = true;
            this.sendSpam.Click += new System.EventHandler(this.sendSpam_Click);
            // 
            // Enter CS
            // 
            this.EnterCS.Location = new System.Drawing.Point(431, 321);
            this.EnterCS.Name = "button2";
            this.EnterCS.Size = new System.Drawing.Size(166, 27);
            this.EnterCS.TabIndex = 23;
            this.EnterCS.Text = "EnterCS";
            this.EnterCS.UseVisualStyleBackColor = true;
            this.EnterCS.Click += new System.EventHandler(this.EnterCS_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 351);
            this.Controls.Add(this.EnterCS);
            this.Controls.Add(this.name);
            this.Controls.Add(this.CC);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.delay);
            this.Controls.Add(this.sendSpam);
            this.Controls.Add(this.aCS);
            this.Controls.Add(this.sendPacket);
            this.Controls.Add(this.aRestart);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.log);
            this.Controls.Add(this.infoGroup);
            this.Controls.Add(this.connect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "[170.1.1] MapleStory Clientless Bot";
            this.infoGroup.ResumeLayout(false);
            this.infoGroup.PerformLayout();
            this.sendMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox infoGroup;
        private TextBox character;
        private ComboBox channel;
        private TextBox username;
        private ComboBox world;
        private TextBox password;
        private TextBox pic;
        public TextBox log;
        public ComboBox selType;
        public Button connect;
        public Button disconnect;
        public CheckBox aRestart;
        private TextBox sendPacket;
        public CheckBox aCS;
        private SplitButton sendSpam;
        private ContextMenuStrip sendMenu;
        private ToolStripMenuItem sMenuSend;
        private ToolStripMenuItem sMenuSpam;
        private TextBox delay;
        public Button button1;
        public Button CC;
        private Label name;
        private ComboBox loginMode;
        private Button EnterCS;
    }
}

