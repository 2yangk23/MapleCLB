using System;
using System.IO;
using MapleCLB.Types;
using SharedTools;

namespace MapleCLB.MapleClient.Functions {
    internal static class AccountLoader {
        private static readonly byte[] key = { // TODO: Constant key is not very secure...
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };

        public static Tuple<Account, Settings> Load(string path) {
            if (!File.Exists(path)) {
                return null;
            }

            using (var file = File.Open(path, FileMode.Open))
            using (var cs = EncryptedStream.CreateDecryptionStream(key, file))
            using (var br = new BinaryReader(cs)) {
                var account = new Account(br);
                var settings = new Settings(br);
                return new Tuple<Account, Settings>(account, settings);
            }
        }

        public static void Save(string path, Account account, Settings settings) {
            Precondition.NotNull(account);
            Precondition.NotNull(settings);

            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? "");
            using (var file = File.Open(path, FileMode.Create))
            using (var cs = EncryptedStream.CreateEncryptionStream(key, file))
            using (var bw = new BinaryWriter(cs)) {
                account.WriteTo(bw);
                settings.WriteTo(bw);
            }
        }
    }
}
