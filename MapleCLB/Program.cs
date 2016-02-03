using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using MapleCLB.MapleLib.Crypto;

namespace MapleCLB {
    internal static class Program {
        private static readonly byte[] UserKey = //170.1
        {
            0x49, 0x00, 0x00, 0x00,
            0x99, 0x00, 0x00, 0x00,
            0x69, 0x00, 0x00, 0x00,
            0xA5, 0x00, 0x00, 0x00,
            0x4F, 0x00, 0x00, 0x00,
            0x65, 0x00, 0x00, 0x00,
            0x22, 0x00, 0x00, 0x00,
            0x1F, 0x00, 0x00, 0x00
        };

        public static readonly IPAddress LoginIp = IPAddress.Parse("8.31.99.143");
        public static readonly short LoginPort = 8484;
        public static readonly AesCipher AesCipher = new AesCipher(UserKey);
        public static MainForm Gui;
        public static string Hwid1 = Tools.HexEncoding.GetRandomHexString(4);
        public static string Hwid2 = Tools.HexEncoding.GetRandomHexString(4);

        public static void WriteLog(string value) {
            Debug.WriteLine(value);
            Gui.log.BeginInvoke((MethodInvoker) delegate { Gui.log.AppendText(value + Environment.NewLine); });
        }

        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Gui = new MainForm();
            Application.Run(Gui);
        }
    }
}
