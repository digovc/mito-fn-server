using LiteNetLib;
using MultiplayerServer.Abstractions;
using System.Collections.Generic;

namespace MultiplayerServer.Services
{
    public class UdpServer : IUdpServer
    {
        private readonly IUdpListener listener;
        private NetManager _server;

        public UdpServer(IUdpListener listener)
        {
            this.listener = listener;
        }

        IEnumerable<NetPeer> IUdpServer.GetAllPeers()
        {
            return _server.ConnectedPeerList;
        }

        void IUdpServer.Init()
        {
            var netListener = listener.GetListener();

            _server = new NetManager(netListener);
            _server.Start(9876);
        }

        void ITicker.Tick()
        {
            _server.PollEvents();
        }
    }
}