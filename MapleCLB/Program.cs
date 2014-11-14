using System;
using System.Diagnostics;
using System.Windows.Forms;
using MaplePacketLib.Cryptography;

namespace MapleCLB
{
    static class Program
    {
        public static MainForm gui;
        public static AesCipher cipher;
        public static int hwid1, hwid2;
        public const string loginIP = "8.31.99.141";
        public const short loginPort = 8484;

        public static readonly byte[] userKey = new byte[32] //156.1
        {
            0xCA, 0x00, 0x00, 0x00, 
            0x89, 0x00, 0x00, 0x00, 
            0xFD, 0x00, 0x00, 0x00, 
            0xF6, 0x00, 0x00, 0x00, 
            0x99, 0x00, 0x00, 0x00, 
            0xF0, 0x00, 0x00, 0x00, 
            0xA6, 0x00, 0x00, 0x00, 
            0x0B, 0x00, 0x00, 0x00
        };

        public static void writeLog(string value)
        {
            Debug.WriteLine(value);
            gui.log.BeginInvoke((MethodInvoker)delegate { gui.log.AppendText(value + Environment.NewLine); });
        }

        [STAThread]
        static void Main()
        {
            cipher = new AesCipher(userKey);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new MainForm();
            Application.Run(gui);
        }
    }
}
