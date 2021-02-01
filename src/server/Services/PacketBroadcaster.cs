using LiteNetLib;
using MultiplayerServer.Abstractions;
using Packets;
using System;

namespace MultiplayerServer.Services
{
    public class PacketBroadcaster : IPacketBroadcaster
    {
        void IPacketBroadcaster.Login(LoginRequest packet, NetPeer peer)
        {
            OnLogin?.Invoke(peer, packet);
        }

        void IPacketBroadcaster.Move(Move packet, NetPeer peer)
        {
            OnMove?.Invoke(peer, packet);
        }

        public event EventHandler<LoginRequest> OnLogin;

        public event EventHandler<Move> OnMove;
    }
}