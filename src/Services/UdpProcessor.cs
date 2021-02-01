using LiteNetLib;
using LiteNetLib.Utils;
using MultiplayerServer.Abstractions;
using MultiplayerServer.Packets;
using System;

namespace MultiplayerServer.Services
{
    public class UdpProcessor : IUdpProcessor
    {
        private NetPacketProcessor _processor;

        void IUdpProcessor.Init()
        {
            _processor = new NetPacketProcessor();

            _processor.SubscribeReusable<MovePacket, NetPeer>(MoveEvent);
        }

        void IUdpProcessor.ReadPackets(NetPeer peer, NetPacketReader reader)
        {
            _processor.ReadAllPackets(reader, peer);
        }

        private void MoveEvent(MovePacket packet, NetPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}