using LiteNetLib;

namespace MultiplayerServer.Abstractions
{
    public interface ISender
    {
        void Broadcast<T>(NetPeer peerSender, T packet) where T : class, new();

        void Send<T>(NetPeer peerTo, T packet) where T : class, new();
    }
}