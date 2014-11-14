using System;
using System.Windows.Forms;
using MapleCLB.User;
using MapleCLB.Tools;
using System.Threading;

namespace MapleCLB
{
    public partial class MainForm : Form
    {
        public Client c;
        private Thread ct;

        public MainForm()
        {
            InitializeComponent();

            win32.SendMessage(username.Handle, win32.EM_SETCUEBANNER, 0, "Username");
            win32.SendMessage(password.Handle, win32.EM_SETCUEBANNER, 0, "Password");
            win32.SendMessage(pic.Handle, win32.EM_SETCUEBANNER, 0, "PIC");
            win32.SendMessage(character.Handle, win32.EM_SETCUEBANNER, 0, "Character");
            win32.SendMessage(sendPacket.Handle, win32.EM_SETCUEBANNER, 0, "Enter packet to send...");

            c = new Client();
            world.SelectedIndex = 0;
            channel.SelectedIndex = 0;
            selType.SelectedIndex = 0;

            #if DEBUG
            /* Use this for testing account 
            username.Text           = "";
            password.Text           = "";
            pic.Text                = "";
            character.Text          = "";
            world.SelectedIndex     = 0;
            channel.SelectedIndex   = 0;*/
            #endif
        }

        private void connect_Click(object sender, EventArgs e)
        {
            c.user          = username.Text;
            c.pass          = password.Text;
            c.pic           = pic.Text;
            c.ign           = character.Text;
            c.authCode      = ""; //Set empty auth so it fetches new one

            c.world         = (byte)world.SelectedIndex;
            c.channel       = (byte)channel.SelectedIndex;

            ct              = new Thread(() => c.Connect()); //Connect to login servers
            ct.IsBackground = true; //Puts thread in background so it closes with program
            ct.Start();
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            c.session.Disconnect();
        }

        private void connect_EnabledChanged(object sender, EventArgs e)
        {
            username.Enabled    = connect.Enabled;
            password.Enabled    = connect.Enabled;
            pic.Enabled         = connect.Enabled;
            world.Enabled       = connect.Enabled;
            channel.Enabled     = connect.Enabled;
            selType.Enabled     = connect.Enabled;
            character.Enabled   = connect.Enabled;
        }

        private void send_Click(object sender, EventArgs e)
        {
            c.SendPacket(sendPacket.Text);
        }
    }
}
