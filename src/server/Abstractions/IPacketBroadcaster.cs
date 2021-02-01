using LiteNetLib;
using Packets;
using System;

namespace MultiplayerServer.Abstractions
{
    public interface IPacketBroadcaster
    {
        void Login(LoginRequest packet, NetPeer peer);

        void Move(Move packet, NetPeer peer);

        event EventHandler<LoginRequest> OnLogin;

        event EventHandler<Move> OnMove;
    }
}