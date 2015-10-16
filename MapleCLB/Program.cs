using System;
using System.Diagnostics;
using System.Windows.Forms;
using MaplePacketLib.Cryptography;

namespace MapleCLB {
    static class Program {
        public static MainForm gui;
        public static AesCipher cipher;
        public static int hwid1, hwid2;
        public const string loginIP = "8.31.99.141";
        public const short loginPort = 8484;

        public static readonly byte[] userKey = new byte[32] //166.1
        {
            0xD6, 0x00, 0x00, 0x00, 
            0x1F, 0x00, 0x00, 0x00, 
            0xCE, 0x00, 0x00, 0x00, 
            0xD9, 0x00, 0x00, 0x00, 
            0x41, 0x00, 0x00, 0x00, 
            0x07, 0x00, 0x00, 0x00, 
            0xB7, 0x00, 0x00, 0x00, 
            0x07, 0x00, 0x00, 0x00
        };

        public static void WriteLog(string value) {
            Debug.WriteLine(value);
            gui.log.BeginInvoke((MethodInvoker)delegate { gui.log.AppendText(value + Environment.NewLine); });
        }

        [STAThread]
        static void Main() {
            cipher = new AesCipher(userKey);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new MainForm();
            Application.Run(gui);
        }
    }
}
