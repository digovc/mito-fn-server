using LiteNetLib;
using MultiplayerServer.Abstractions;

namespace MultiplayerServer.Services
{
    public class Sender : ISender
    {
        private readonly IUdpProcessor processor;
        private readonly IUdpServer server;

        public Sender(IUdpProcessor processor, IUdpServer server)
        {
            this.processor = processor;
            this.server = server;
        }

        void ISender.Broadcast<T>(NetPeer peerSender, T packet)
        {
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
            var data = processor.Write(packet);
            peerTo.Send(data, DeliveryMethod.ReliableUnordered);
        }
    }
}