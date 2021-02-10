namespace Packets
{
    public class Vote
    {
        public byte VoterGlobalID
        {
            get; set;
        }

        public byte TargetGlobalID
        {
            get; set;
        }
    }
}