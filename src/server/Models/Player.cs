using LiteNetLib;

namespace MultiplayerServer.Models
{
    public class Player : Entity
    {
        public bool InAssembly
        {
            get; set;
        }

        public bool IsInfected
        {
            get; set;
        }

        public bool IsKilled
        {
            get; set;
        }

        public bool IsPoisoned
        {
            get; set;
        }

        public NetPeer Peer
        {
            get; set;
        }

        public string Slot
        {
            get; set;
        }
    }
}