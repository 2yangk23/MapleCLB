using System;
using MapleCLB.Tools;
using MapleLib.Packet;

namespace ScriptLib {
    public interface IScriptClient {
        ScriptManager<T> GetScriptManager<T>() where T : IScriptClient;

        void WaitScriptRecv(ushort header, Blocking<PacketReader> reader, bool returnPacket);
        bool AddScriptRecv(ushort header, EventHandler<PacketReader> handler);
        void RemoveScriptRecv(ushort header);

        void SendPacket(PacketWriter packet);
        void SendPacket(byte[] packet);
    }
}
