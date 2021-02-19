namespace Packets
{
    public class LoginResponse
    {
        public byte GlobalID
        {
            get; set;
        }

        public bool IsMaster
        {
            get; set;
        }

        public string[] OccupiedSlots
        {
            get; set;
        }

        public byte[] OccupiedSlotsIDs
        {
            get; set;
        }
    }
}