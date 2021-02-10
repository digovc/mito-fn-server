using LiteNetLib;
using LiteNetLib.Utils;
using MultiplayerServer.Abstractions;
using Packets;

namespace MultiplayerServer.Services
{
    public class UdpProcessor : IUdpProcessor
    {
        private readonly IPacketBroadcaster broadcaster;

        private NetPacketProcessor _processor;

        public UdpProcessor(IPacketBroadcaster broadcaster)
        {
            this.broadcaster = broadcaster;
        }

        void IUdpProcessor.Init()
        {
            _processor = new NetPacketProcessor();

            _processor.SubscribeReusable<CallAssembly, NetPeer>(broadcaster.CallAssembly);
            _processor.SubscribeReusable<FinishGame, NetPeer>(broadcaster.FinishGame);
            _processor.SubscribeReusable<LoadScene, NetPeer>(broadcaster.LoadScene);
            _processor.SubscribeReusable<LoginRequest, NetPeer>(broadcaster.Login);
            _processor.SubscribeReusable<Move, NetPeer>(broadcaster.Move);
            _processor.SubscribeReusable<PlayerInAssembly, NetPeer>(broadcaster.PlayerInAssembly);
            _processor.SubscribeReusable<PlayerInfected, NetPeer>(broadcaster.PlayerInfected);
            _processor.SubscribeReusable<PlayerKilled, NetPeer>(broadcaster.PlayerKilled);
            _processor.SubscribeReusable<PlayerOutOfAssembly, NetPeer>(broadcaster.PlayerOutOfAssembly);
            _processor.SubscribeReusable<PlayerPoisoned, NetPeer>(broadcaster.PlayerPoisoned);
            _processor.SubscribeReusable<Rotate, NetPeer>(broadcaster.Rotate);
            _processor.SubscribeReusable<SelectSlotRequest, NetPeer>(broadcaster.SelectSlot);
            _processor.SubscribeReusable<SpawnStage, NetPeer>(broadcaster.SpawnStage);
            _processor.SubscribeReusable<StartGame, NetPeer>(broadcaster.StartGame);
            _processor.SubscribeReusable<TaskAddCount, NetPeer>(broadcaster.TaskAddCount);
            _processor.SubscribeReusable<TaskComplete, NetPeer>(broadcaster.TaskComplete);
            _processor.SubscribeReusable<TaskEnableZone, NetPeer>(broadcaster.TaskEnableZone);
            _processor.SubscribeReusable<TaskMakeActivable, NetPeer>(broadcaster.TaskMakeActivable);
            _processor.SubscribeReusable<Vote, NetPeer>(broadcaster.Vote);
        }

        void IUdpProcessor.ReadPackets(NetPeer peer, NetPacketReader reader)
        {
            _processor.ReadAllPackets(reader, peer);
        }

        byte[] IUdpProcessor.Write<T>(T packet)
        {
            return _processor.Write(packet);
        }
    }
}