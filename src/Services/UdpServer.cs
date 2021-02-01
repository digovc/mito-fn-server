using LiteNetLib;
using MultiplayerServer.Abstractions;

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