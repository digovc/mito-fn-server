namespace MultiplayerServer.Abstractions
{
    public interface IGlobalIDProvider
    {
        byte GetNextID();
    }
}