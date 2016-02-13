using System;
using System.Net;
using System.Windows.Forms;
using MapleCLB.Forms;
using MapleCLB.MapleLib.Crypto;

namespace MapleCLB {
    internal static class Program {
        public static readonly IPAddress LoginIp = IPAddress.Parse("8.31.99.143");
        public static readonly short LoginPort = 8484;

        private static readonly byte[] UserKey = //170.2
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
        public static readonly AesCipher AesCipher = new AesCipher(UserKey);
        
        private static MainForm gui;

        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            gui = new MainForm();
            Application.Run(gui);
        }
    }
}
