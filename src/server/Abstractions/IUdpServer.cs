using LiteNetLib;
using System.Collections.Generic;

namespace MultiplayerServer.Abstractions
{
    public interface IUdpServer : ITicker
    {
        IEnumerable<NetPeer> GetAllPeers();

        void Init();
    }
}