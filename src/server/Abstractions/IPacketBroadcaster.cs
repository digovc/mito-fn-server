using LiteNetLib;
using Packets;
using System;

namespace MultiplayerServer.Abstractions
{
    public interface IPacketBroadcaster
    {
        void CallAssembly(CallAssembly packet, NetPeer peer);

        void FinishGame(FinishGame packet, NetPeer peer);

        void LoadScene(LoadScene packet, NetPeer peer);

        void Login(LoginRequest packet, NetPeer peer);

        void Move(Move packet, NetPeer peer);

        void PlayerDisconnected(PlayerDisconnected packet, NetPeer peer);

        void PlayerInAssembly(PlayerInAssembly packet, NetPeer peer);

        void PlayerInfected(PlayerInfected packet, NetPeer peer);

        void PlayerKilled(PlayerKilled packet, NetPeer peer);

        void PlayerOutOfAssembly(PlayerOutOfAssembly packet, NetPeer peer);

        void PlayerPoisoned(PlayerPoisoned packet, NetPeer peer);

        void Rotate(Rotate packet, NetPeer peer);

        void SelectSlot(SelectSlotRequest packet, NetPeer peer);

        void SlotSelected(SlotSelected packet, NetPeer peer);

        void SlotsRequest(SlotsRequest packet, NetPeer peer);

        void SpawnStage(SpawnStage packet, NetPeer peer);

        void StartGame(StartGame packet, NetPeer peer);

        void TaskAddCount(TaskAddCount packet, NetPeer peer);

        void TaskComplete(TaskComplete packet, NetPeer peer);

        void TaskEnableZone(TaskEnableZone packet, NetPeer peer);

        void TaskMakeActivable(TaskMakeActivable packet, NetPeer peer);

        void Vote(Vote packet, NetPeer peer);

        event EventHandler<CallAssembly> OnCallAssembly;

        event EventHandler<FinishGame> OnFinishGame;

        event EventHandler<LoadScene> OnLoadScene;

        event EventHandler<LoginRequest> OnLogin;

        event EventHandler<Move> OnMove;

        event EventHandler<PlayerDisconnected> OnPlayerDisconnected;

        event EventHandler<PlayerInAssembly> OnPlayerInAssembly;

        event EventHandler<PlayerInfected> OnPlayerInfected;

        event EventHandler<PlayerKilled> OnPlayerKilled;

        event EventHandler<PlayerOutOfAssembly> OnPlayerOutOfAssembly;

        event EventHandler<PlayerPoisoned> OnPlayerPoisoned;

        event EventHandler<Rotate> OnRotate;

        event EventHandler<SelectSlotRequest> OnSelectSlot;

        event EventHandler<SlotSelected> OnSlotSelected;

        event EventHandler<SlotsRequest> OnSlotsRequest;

        event EventHandler<SpawnStage> OnSpawnStage;

        event EventHandler<StartGame> OnStartGame;

        event EventHandler<TaskAddCount> OnTaskAddCount;

        event EventHandler<TaskComplete> OnTaskComplete;

        event EventHandler<TaskEnableZone> OnTaskEnableZone;

        event EventHandler<TaskMakeActivable> OnTaskMakeActivable;

        event EventHandler<Vote> OnVote;
    }
}