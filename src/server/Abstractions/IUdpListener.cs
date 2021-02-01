using LiteNetLib;

namespace MultiplayerServer.Abstractions
{
    public interface IUdpListener
    {
        EventBasedNetListener GetListener();

        void Init();
    }
}