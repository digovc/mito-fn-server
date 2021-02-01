using LiteNetLib;
using System;

namespace MultiplayerServer.Abstractions
{
    public interface IUdpListener
    {
        EventBasedNetListener GetListener();

        void Init();

        event EventHandler<NetPeer> OnDisconnect;
    }
}