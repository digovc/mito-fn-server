namespace Packets
{
    public class SlotSelected
    {
        public byte OwnerGlobalID
        {
            get; set;
        }

        public string Slot
        {
            get; set;
        }
    }
}