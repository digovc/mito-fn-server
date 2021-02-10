using LiteNetLib;
using System;

namespace MultiplayerServer
{
    public class GameException : Exception
    {
        public readonly byte level;
        public readonly string message;
        public readonly NetPeer peer;

        public GameException(string message, NetPeer peer, byte level = 0)
        {
            this.message = message;
            this.level = level;
            this.peer = peer;
        }
    }
}