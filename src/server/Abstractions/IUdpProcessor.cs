using LiteNetLib;

namespace MultiplayerServer.Abstractions
{
    public interface IUdpProcessor
    {
        void Init();

        void ReadPackets(NetPeer peer, NetPacketReader reader);

        byte[] Write<T>(T packet) where T : class, new();
    }
}