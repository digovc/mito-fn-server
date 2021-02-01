using LiteNetLib;
using LiteNetLib.Utils;
using MultiplayerServer.Abstractions;
using Packets;

namespace MultiplayerServer.Services
{
    public class UdpProcessor : IUdpProcessor
    {
        private readonly IPacketBroadcaster broadcaster;

        private NetPacketProcessor _processor;

        public UdpProcessor(IPacketBroadcaster broadcaster)
        {
            this.broadcaster = broadcaster;
        }

        void IUdpProcessor.Init()
        {
            _processor = new NetPacketProcessor();

            _processor.SubscribeReusable<LoginRequest, NetPeer>(broadcaster.Login);
            _processor.SubscribeReusable<Move, NetPeer>(broadcaster.Move);
        }

        void IUdpProcessor.ReadPackets(NetPeer peer, NetPacketReader reader)
        {
            _processor.ReadAllPackets(reader, peer);
        }

        byte[] IUdpProcessor.Write<T>(T packet)
        {
            return _processor.Write(packet);
        }
    }
}