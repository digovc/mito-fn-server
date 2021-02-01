using LiteNetLib;

namespace MultiplayerServer.Models
{
    public class Player : Entity
    {
        public NetPeer Peer
        {
            get; set;
        }
    }
}