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
    }
}