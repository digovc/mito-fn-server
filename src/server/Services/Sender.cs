using LiteNetLib;
using Microsoft.Extensions.Logging;
using MultiplayerServer.Abstractions;

namespace MultiplayerServer.Services
{
    public class Sender : ISender
    {
        private readonly ILogger<Sender> logger;
        private readonly IUdpProcessor processor;
        private readonly IUdpServer server;

        public Sender(
            ILogger<Sender> logger,
            IUdpProcessor processor,
            IUdpServer server
            )
        {
            this.logger = logger;
            this.processor = processor;
            this.server = server;
        }

        void ISender.Broadcast<T>(NetPeer peerSender, T packet)
        {
            var log = string.Format("Sending packet '{0}' to all from peer '{1}'.", packet.GetType().Name, peerSender.Id);
            logger.LogInformation(log);
            var peers = server.GetAllPeers();
            var data = processor.Write(packet);

            foreach (var peer in peers)
            {
                if (peer != peerSender)
                {
                    peer.Send(data, DeliveryMethod.ReliableUnordered);
                }
            }
        }

        void ISender.Send<T>(NetPeer peerTo, T packet)
        {
            var log = string.Format("Sending packet '{0}' to peer '{1}'.", packet.GetType().Name, peerTo.Id);
            logger.LogInformation(log);
            var data = processor.Write(packet);
            peerTo.Send(data, DeliveryMethod.ReliableUnordered);
        }
    }
}