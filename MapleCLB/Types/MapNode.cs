using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace MapleCLB.Types {
    public sealed class PortalInfo {
        public Position Position { get; set; }
        public string Name { get; set; }
    }

    public sealed class MapNode {
        public ReadOnlyDictionary<int, PortalInfo> Portals;
        public ReadOnlyDictionary<int, int> Choice { get; set; }
        public int Id { get; }

        public MapNode(int id, ReadOnlyDictionary<int, PortalInfo> portals,
                       ReadOnlyDictionary<int, int> choice) {
            Id = id;
            Portals = portals;
            Choice = choice;
        }

        public MapNode(BinaryReader reader) {
            Id = reader.ReadInt32();

            int count = reader.ReadInt32();
            Dictionary<int, PortalInfo> portals = new Dictionary<int, PortalInfo>(count);
            for (int i = 0; i < count; i++) {
                int key = reader.ReadInt32();
                portals[key] = new PortalInfo {
                    Position = new Position(reader.ReadInt16(), reader.ReadInt16()),
                    Name = reader.ReadString()
                };
            }

            count = reader.ReadInt32();
            Dictionary<int, int> choice = new Dictionary<int, int>(count);
            for (int i = 0; i < count; i++) {
                int key = reader.ReadInt32();
                int value = reader.ReadInt32();
                choice[key] = value;
            }

            // Assign Dictionaries
            Portals = new ReadOnlyDictionary<int, PortalInfo>(portals);
            Choice = new ReadOnlyDictionary<int, int>(choice);
        }

        public override bool Equals(object o) {
            var map = o as MapNode;
            return Id == map?.Id;
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }
    }
}
