using System.Numerics;

namespace MultiplayerServer.Models
{
    public class Entity
    {
        public byte GlobalID
        {
            get; set;
        }

        public Vector3 Position
        {
            get; set;
        }
    }
}