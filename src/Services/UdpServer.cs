using LiteNetLib;
using MultiplayerServer.Abstractions;
using System;

namespace MultiplayerServer.Services
{
    public class UdpServer : IUdpServer
    {
        private EventBasedNetListener _listener;
        private NetManager _server;

        void IUdpServer.Init()
        {
            InitListener();
            InitServer();
        }

        void ITicker.Tick()
        {
            _server.PollEvents();
        }

        private void ConnectionRequestEvent(ConnectionRequest request)
        {
            if (_server.ConnectedPeersCount > 5)
            {
                throw new OverflowException("Max players joinned.");
            }
        }

        private void InitListener()
        {
            _listener = new EventBasedNetListener();
            _listener.ConnectionRequestEvent += ConnectionRequestEvent;
            _listener.PeerConnectedEvent += PeerConnectedEvent;
        }

        private void InitServer()
        {
            _server = new NetManager(_listener);
            _server.Start(9876);
        }

        private void PeerConnectedEvent(NetPeer peer)
        {
        }
    }
}