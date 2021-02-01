using LiteNetLib;
using Microsoft.Extensions.Configuration;
using MultiplayerServer.Abstractions;

namespace MultiplayerServer.Services
{
    public class UdpListener : IUdpListener
    {
        private readonly IConfiguration configuration;
        private readonly IUdpProcessor processor;
        private EventBasedNetListener _listener;
        private byte _peersCount = 0;

        public UdpListener(
            IConfiguration configuration,
            IUdpProcessor processor
            )
        {
            this.configuration = configuration;
            this.processor = processor;
        }

        EventBasedNetListener IUdpListener.GetListener()
        {
            return _listener;
        }

        void IUdpListener.Init()
        {
            _listener = new EventBasedNetListener();

            _listener.ConnectionRequestEvent += ConnectionRequestEvent;
            _listener.PeerDisconnectedEvent += PeerDisconnectedEvent;
            _listener.PeerConnectedEvent += PeerConnectedEvent;
            _listener.NetworkReceiveEvent += NetworkReceiveEvent;
        }

        private void ConnectionRequestEvent(ConnectionRequest request)
        {
            var password = configuration["ConnectionPassword"];

            request.AcceptIfKey(password);

            var maxPlayers = byte.Parse(configuration["MaxPlayers"]);

            if (_peersCount > maxPlayers)
            {
                request.Reject();
            }

            _peersCount++;
        }

        private void NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            processor.ReadPackets(peer, reader);
        }

        private void PeerConnectedEvent(NetPeer peer)
        {
        }

        private void PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            _peersCount--;
        }
    }
}