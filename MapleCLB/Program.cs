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
        private static readonly byte[] userKey = { //174.2
            0xE9, 0x00, 0x00, 0x00,
            0x9D, 0x00, 0x00, 0x00,
            0x6A, 0x00, 0x00, 0x00,
            0xB4, 0x00, 0x00, 0x00,
            0x7B, 0x00, 0x00, 0x00,
            0xC0, 0x00, 0x00, 0x00,
            0xFF, 0x00, 0x00, 0x00,
            0xAD, 0x00, 0x00, 0x00
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
