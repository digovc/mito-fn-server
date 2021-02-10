using LiteNetLib;
using MultiplayerServer.Abstractions;
using Packets;

namespace MultiplayerServer.Game
{
    public class GameManager : IGameManager
    {
        private readonly IPacketBroadcaster broadcaster;
        private readonly Models.Game game = new Models.Game();

        public GameManager(IPacketBroadcaster broadcaster)
        {
            this.broadcaster = broadcaster;
        }

        void IGameManager.Init()
        {
            game.Started = false;

            broadcaster.OnStartGame += StartGame;
            broadcaster.OnFinishGame += FinishGame;
        }

        private void FinishGame(object peer, FinishGame packet)
        {
            game.Started = false;
        }

        private void StartGame(object peer, StartGame packet)
        {
            if (game.Started)
            {
                throw new GameException("Game already running.", peer as NetPeer, Error.CRITICAL);
            }

            game.Started = true;
        }
    }
}