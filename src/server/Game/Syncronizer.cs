using LiteNetLib;
using MultiplayerServer.Abstractions;

namespace MultiplayerServer.Game
{
    public class Syncronizer : ISyncronizer
    {
        private readonly IPacketBroadcaster broadcaster;
        private readonly ISender sender;

        public Syncronizer(
            IPacketBroadcaster broadcaster,
            ISender sender
            )
        {
            this.broadcaster = broadcaster;
            this.sender = sender;
        }

        void ISyncronizer.Init()
        {
            broadcaster.OnCallAssembly += Sync;
            broadcaster.OnFinishGame += Sync;
            broadcaster.OnLoadScene += Sync;
            broadcaster.OnMove += Sync;
            broadcaster.OnPlayerInAssembly += Sync;
            broadcaster.OnPlayerInfected += Sync;
            broadcaster.OnPlayerKilled += Sync;
            broadcaster.OnPlayerOutOfAssembly += Sync;
            broadcaster.OnPlayerPoisoned += Sync;
            broadcaster.OnRotate += Sync;
            broadcaster.OnSelectSlot += Sync;
            broadcaster.OnSpawnStage += Sync;
            broadcaster.OnStartGame += Sync;
            broadcaster.OnTaskAddCount += Sync;
            broadcaster.OnTaskComplete += Sync;
            broadcaster.OnTaskEnableZone += Sync;
            broadcaster.OnTaskMakeActivable += Sync;
            broadcaster.OnVote += Sync;
        }

        private void Sync<T>(object peer, T packet) where T : class, new()
        {
            sender.Broadcast(peer as NetPeer, packet);
        }
    }
}