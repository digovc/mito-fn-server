using MultiplayerServer.Abstractions;

namespace MultiplayerServer.Services
{
    public class GlobalIDProvider : IGlobalIDProvider
    {
        private static byte _lastID = 0;

        byte IGlobalIDProvider.GetNextID()
        {
            return ++_lastID;
        }
    }
}