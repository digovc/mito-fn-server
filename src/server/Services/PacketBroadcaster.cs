using LiteNetLib;
using Microsoft.Extensions.DependencyInjection;
using MultiplayerServer.Abstractions;
using Packets;
using System;

namespace MultiplayerServer.Services
{
    public class PacketBroadcaster : IPacketBroadcaster
    {
        private readonly IServiceProvider provider;
        private ISender sender;

        public PacketBroadcaster(IServiceProvider provider)
        {
            this.provider = provider;
        }

        void IPacketBroadcaster.CallAssembly(CallAssembly packet, NetPeer peer)
        {
            try
            {
                OnCallAssembly?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.FinishGame(FinishGame packet, NetPeer peer)
        {
            try
            {
                OnFinishGame?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.LoadScene(LoadScene packet, NetPeer peer)
        {
            try
            {
                OnLoadScene?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.Login(LoginRequest packet, NetPeer peer)
        {
            try
            {
                OnLogin?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.Move(Move packet, NetPeer peer)
        {
            try
            {
                OnMove?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.PlayerInAssembly(PlayerInAssembly packet, NetPeer peer)
        {
            try
            {
                OnPlayerInAssembly?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.PlayerInfected(PlayerInfected packet, NetPeer peer)
        {
            try
            {
                OnPlayerInfected?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.PlayerKilled(PlayerKilled packet, NetPeer peer)
        {
            try
            {
                OnPlayerKilled?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.PlayerOutOfAssembly(PlayerOutOfAssembly packet, NetPeer peer)
        {
            try
            {
                OnPlayerOutOfAssembly?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.PlayerPoisoned(PlayerPoisoned packet, NetPeer peer)
        {
            try
            {
                OnPlayerPoisoned?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.Rotate(Rotate packet, NetPeer peer)
        {
            try
            {
                OnRotate?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.SelectSlot(SelectSlotRequest packet, NetPeer peer)
        {
            try
            {
                OnSelectSlot?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.SpawnStage(SpawnStage packet, NetPeer peer)
        {
            try
            {
                OnSpawnStage?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.StartGame(StartGame packet, NetPeer peer)
        {
            try
            {
                OnStartGame?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.TaskAddCount(TaskAddCount packet, NetPeer peer)
        {
            try
            {
                OnTaskAddCount?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.TaskComplete(TaskComplete packet, NetPeer peer)
        {
            try
            {
                OnTaskComplete?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.TaskEnableZone(TaskEnableZone packet, NetPeer peer)
        {
            try
            {
                OnTaskEnableZone?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.TaskMakeActivable(TaskMakeActivable packet, NetPeer peer)
        {
            try
            {
                OnTaskMakeActivable?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        void IPacketBroadcaster.Vote(Vote packet, NetPeer peer)
        {
            try
            {
                OnVote?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        private void SendError(GameException ex)
        {
            var packet = new Error
            {
                Level = ex.level,
                Message = ex.message,
            };

            sender = sender ?? provider.GetService<ISender>();
            sender.Send(ex.peer, packet);
        }

        public event EventHandler<CallAssembly> OnCallAssembly;

        public event EventHandler<FinishGame> OnFinishGame;

        public event EventHandler<LoadScene> OnLoadScene;

        public event EventHandler<LoginRequest> OnLogin;

        public event EventHandler<Move> OnMove;

        public event EventHandler<PlayerInAssembly> OnPlayerInAssembly;

        public event EventHandler<PlayerInfected> OnPlayerInfected;

        public event EventHandler<PlayerKilled> OnPlayerKilled;

        public event EventHandler<PlayerOutOfAssembly> OnPlayerOutOfAssembly;

        public event EventHandler<PlayerPoisoned> OnPlayerPoisoned;

        public event EventHandler<Rotate> OnRotate;

        public event EventHandler<SelectSlotRequest> OnSelectSlot;

        public event EventHandler<SpawnStage> OnSpawnStage;

        public event EventHandler<StartGame> OnStartGame;

        public event EventHandler<TaskAddCount> OnTaskAddCount;

        public event EventHandler<TaskComplete> OnTaskComplete;

        public event EventHandler<TaskEnableZone> OnTaskEnableZone;

        public event EventHandler<TaskMakeActivable> OnTaskMakeActivable;

        public event EventHandler<Vote> OnVote;
    }
}