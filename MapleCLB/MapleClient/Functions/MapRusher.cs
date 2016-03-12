using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MapleCLB.Resources;
using MapleCLB.Types;

namespace MapleCLB.MapleClient.Functions {
    internal class MapRusher {
        private static readonly IReadOnlyDictionary<int, MapNode> Maps = ResourceLoader.LoadMaps();

        internal static List<Tuple<short[], string>> Pathfind(int src, int dst) {
            List<Tuple<short[], string>> directions = new List<Tuple<short[], string>>();

            // Already on destination map
            if (src == dst) {
                return directions;
            }

            // Can move to map with 1 portal
            ReadOnlyDictionary<int, Tuple<short[], string>> curPortals = Maps[src].Portals;
            if (curPortals.ContainsKey(dst)) {
                directions.Add(curPortals[dst]);
                return directions;
            }

            // Cannot reach destination
            if (!Maps[src].Choice.ContainsKey(dst)) {
                return null;
            }

            // Find path to destination
            int curr = src;
            while (curr != dst) {
                int next = Maps[curr].Choice[dst];
                directions.Add(Maps[curr].Portals[next]);
                curr = next;
            }

            return directions;
        }

        internal static List<int> Reachable(int src) {
            return new List<int>(Maps[src].Choice.Keys);
        }
    }
}
