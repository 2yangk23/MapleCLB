using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using MapleCLB.Types;

namespace MapleCLB.Resources {
    public class MapData {
        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

        public static ReadOnlyDictionary<int, MapNode> Nodes = LoadMaps();
        public static ReadOnlyDictionary<int, string[]> Names = LoadMapNames();

        private static ReadOnlyDictionary<int, MapNode> LoadMaps() {
            using (var file = Assembly.GetManifestResourceStream("MapleCLB.Resources.Map.node.map")) {
                var br = new BinaryReader(file);

                int count = br.ReadInt32();
                Dictionary<int, MapNode> readMap = new Dictionary<int, MapNode>(count);
                for (int i = 0; i < count; i++) {
                    int key = br.ReadInt32();
                    var node = new MapNode(br);
                    readMap.Add(key, node);
                }

                return new ReadOnlyDictionary<int, MapNode>(readMap);
            }
        }

        private static ReadOnlyDictionary<int, string[]> LoadMapNames() {
            using (var file = Assembly.GetManifestResourceStream("MapleCLB.Resources.Map.name.map")) {
                var br = new BinaryReader(file);

                int count = br.ReadInt32();
                Dictionary<int, string[]> mapNames = new Dictionary<int, string[]>(count);
                for (int i = 0; i < count; i++) {
                    int key = br.ReadInt32();
                    string[] name = { br.ReadString(), br.ReadString() };
                    mapNames[key] = name;
                }

                return new ReadOnlyDictionary<int, string[]>(mapNames);
            }
        } 
    }
}
