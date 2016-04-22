using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MapleCLB.Forms;
using MapleLib.Crypto;

namespace MapleCLB {
    internal static class Program {
        private static MainForm gui;

        private static int rngSeed = Environment.TickCount;
        private static readonly byte[] userKey = { //172.0
            0xF1, 0x00, 0x00, 0x00,
            0xB9, 0x00, 0x00, 0x00,
            0xF8, 0x00, 0x00, 0x00,
            0x0C, 0x00, 0x00, 0x00,
            0xF6, 0x00, 0x00, 0x00,
            0xB4, 0x00, 0x00, 0x00,
            0xEB, 0x00, 0x00, 0x00,
            0xB1, 0x00, 0x00, 0x00
        };

        public static readonly IPAddress LoginIp = IPAddress.Parse("8.31.99.143");
        public static readonly short LoginPort = 8484;
        public static readonly AesCipher AesCipher = new AesCipher(userKey);

        public static readonly ThreadLocal<Random> Rng =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref rngSeed)));

        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            gui = new MainForm();
            Application.Run(gui);
        }
    }
}
