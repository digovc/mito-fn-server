namespace Packets
{
    public class SlotsResponse
    {
        public string MySlot
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