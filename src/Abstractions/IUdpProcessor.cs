using LiteNetLib;

namespace MultiplayerServer.Abstractions
{
    public interface IUdpProcessor
    {
        void Init();

        void ReadPackets(NetPeer peer, NetPacketReader reader);
    }
}