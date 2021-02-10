namespace Packets
{
    public class SelectSlotRequest
    {
        public byte GlobalID
        {
            get; set;
        }

        public string Slot
        {
            get; set;
        }
    }
}