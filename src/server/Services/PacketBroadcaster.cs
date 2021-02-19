using LiteNetLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MultiplayerServer.Abstractions;
using Packets;
using System;

namespace MultiplayerServer.Services
{
    public class PacketBroadcaster : IPacketBroadcaster
    {
        private readonly ILogger<PacketBroadcaster> logger;
        private readonly IServiceProvider provider;
        private ISender sender;

        public PacketBroadcaster(
            ILogger<PacketBroadcaster> logger,
            IServiceProvider provider
            )
        {
            this.logger = logger;
            this.provider = provider;
        }

        void IPacketBroadcaster.CallAssembly(CallAssembly packet, NetPeer peer) => CallEvent(packet, peer, OnCallAssembly);

        void IPacketBroadcaster.FinishGame(FinishGame packet, NetPeer peer) => CallEvent(packet, peer, OnFinishGame);

        void IPacketBroadcaster.LoadScene(LoadScene packet, NetPeer peer) => CallEvent(packet, peer, OnLoadScene);

        void IPacketBroadcaster.Login(LoginRequest packet, NetPeer peer) => CallEvent(packet, peer, OnLogin);

        void IPacketBroadcaster.Move(Move packet, NetPeer peer) => CallEvent(packet, peer, OnMove);

        void IPacketBroadcaster.PlayerDisconnected(PlayerDisconnected packet, NetPeer peer) => CallEvent(packet, peer, OnPlayerDisconnected);

        void IPacketBroadcaster.PlayerInAssembly(PlayerInAssembly packet, NetPeer peer) => CallEvent(packet, peer, OnPlayerInAssembly);

        void IPacketBroadcaster.PlayerInfected(PlayerInfected packet, NetPeer peer) => CallEvent(packet, peer, OnPlayerInfected);

        void IPacketBroadcaster.PlayerKilled(PlayerKilled packet, NetPeer peer) => CallEvent(packet, peer, OnPlayerKilled);

        void IPacketBroadcaster.PlayerOutOfAssembly(PlayerOutOfAssembly packet, NetPeer peer) => CallEvent(packet, peer, OnPlayerOutOfAssembly);

        void IPacketBroadcaster.PlayerPoisoned(PlayerPoisoned packet, NetPeer peer) => CallEvent(packet, peer, OnPlayerPoisoned);

        void IPacketBroadcaster.Rotate(Rotate packet, NetPeer peer) => CallEvent(packet, peer, OnRotate);

        void IPacketBroadcaster.SelectSlot(SelectSlotRequest packet, NetPeer peer) => CallEvent(packet, peer, OnSelectSlot);

        void IPacketBroadcaster.SlotSelected(SlotSelected packet, NetPeer peer) => CallEvent(packet, peer, OnSlotSelected);

        void IPacketBroadcaster.SlotsRequest(SlotsRequest packet, NetPeer peer) => CallEvent(packet, peer, OnSlotsRequest);

        void IPacketBroadcaster.SpawnStage(SpawnStage packet, NetPeer peer) => CallEvent(packet, peer, OnSpawnStage);

        void IPacketBroadcaster.StartGame(StartGame packet, NetPeer peer) => CallEvent(packet, peer, OnStartGame);

        void IPacketBroadcaster.TaskAddCount(TaskAddCount packet, NetPeer peer) => CallEvent(packet, peer, OnTaskAddCount);

        void IPacketBroadcaster.TaskComplete(TaskComplete packet, NetPeer peer) => CallEvent(packet, peer, OnTaskComplete);

        void IPacketBroadcaster.TaskEnableZone(TaskEnableZone packet, NetPeer peer) => CallEvent(packet, peer, OnTaskEnableZone);

        void IPacketBroadcaster.TaskMakeActivable(TaskMakeActivable packet, NetPeer peer) => CallEvent(packet, peer, OnTaskMakeActivable);

        void IPacketBroadcaster.Vote(Vote packet, NetPeer peer) => CallEvent(packet, peer, OnVote);

        private void CallEvent<T>(T packet, NetPeer peer, EventHandler<T> @event)
        {
            try
            {
                LogPacket(packet.GetType(), peer);
                @event?.Invoke(peer, packet);
            }
            catch (GameException ex)
            {
                SendError(ex);
            }
        }

        private void LogPacket(Type type, NetPeer peer)
        {
            var log = string.Format("Packet '{0}' received from peer '{1}'.", type.Name, peer.Id);
            logger.LogInformation(log);
        }

        private void SendError(GameException ex)
        {
            var packet = new Error
            {
                Level = ex.level,
                Message = ex.message,
            };

            sender ??= provider.GetService<ISender>();
            sender.Send(ex.peer, packet);
        }

        public event EventHandler<CallAssembly> OnCallAssembly;

        public event EventHandler<FinishGame> OnFinishGame;

        public event EventHandler<LoadScene> OnLoadScene;

        public event EventHandler<LoginRequest> OnLogin;

        public event EventHandler<Move> OnMove;

        public event EventHandler<PlayerDisconnected> OnPlayerDisconnected;

        public event EventHandler<PlayerInAssembly> OnPlayerInAssembly;

        public event EventHandler<PlayerInfected> OnPlayerInfected;

        public event EventHandler<PlayerKilled> OnPlayerKilled;

        public event EventHandler<PlayerOutOfAssembly> OnPlayerOutOfAssembly;

        public event EventHandler<PlayerPoisoned> OnPlayerPoisoned;

        public event EventHandler<Rotate> OnRotate;

        public event EventHandler<SelectSlotRequest> OnSelectSlot;

        public event EventHandler<SlotSelected> OnSlotSelected;

        public event EventHandler<SlotsRequest> OnSlotsRequest;

        public event EventHandler<SpawnStage> OnSpawnStage;

        public event EventHandler<StartGame> OnStartGame;

        public event EventHandler<TaskAddCount> OnTaskAddCount;

        public event EventHandler<TaskComplete> OnTaskComplete;

        public event EventHandler<TaskEnableZone> OnTaskEnableZone;

        public event EventHandler<TaskMakeActivable> OnTaskMakeActivable;

        public event EventHandler<Vote> OnVote;
    }
}