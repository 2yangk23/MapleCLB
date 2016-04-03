using System.IO;

namespace MapleCLB.Types {
    public sealed class Settings {
        public string Temp = "Settings to come...";

        public Settings() { }

        public Settings(BinaryReader br) {
            Temp = br.ReadString();
        }

        public void WriteTo(BinaryWriter bw) {
            bw.Write(Temp);
        }
    }
}
