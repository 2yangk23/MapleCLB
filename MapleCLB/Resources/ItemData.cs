using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace MapleCLB.Resources {
    public class ItemData {
        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ReadOnlyDictionary<int, string> Equip = LoadNames("equip.item");
        public static ReadOnlyDictionary<int, string> Use = LoadNames("use.item");
        public static ReadOnlyDictionary<int, string> Setup = LoadNames("setup.item");
        public static ReadOnlyDictionary<int, string> Etc = LoadNames("etc.item");
        public static ReadOnlyDictionary<int, string> Cash = LoadNames("cash.item");

        private static ReadOnlyDictionary<int, string> LoadNames(string filename) {
            using (var file = Assembly.GetManifestResourceStream("MapleCLB.Resources.Item." + filename)) {
                var br = new BinaryReader(file);

                int count = br.ReadInt32();
                Dictionary<int, string> names = new Dictionary<int, string>(count);
                for (int i = 0; i < count; i++) {
                    int key = br.ReadInt32();
                    names[key] = br.ReadString();
                }

                return new ReadOnlyDictionary<int, string>(names);
            }
        }
    }
}
