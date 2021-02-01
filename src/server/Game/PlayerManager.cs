using LiteNetLib;
using Microsoft.Extensions.Configuration;
using MultiplayerServer.Abstractions;
using MultiplayerServer.Models;
using Packets;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace MultiplayerServer.Game
{
    public class PlayerManager : IPlayerManager
    {
        private readonly IPacketBroadcaster broadcaster;
        private readonly IConfiguration configuration;
        private readonly IUdpListener listener;
        private readonly ISender sender;
        private List<Player> _players;

        public PlayerManager(
            IPacketBroadcaster broadcaster,
            IConfiguration configuration,
            IUdpListener listener,
            ISender sender
            )
        {
            this.broadcaster = broadcaster;
            this.configuration = configuration;
            this.listener = listener;
            this.sender = sender;
        }

        void IPlayerManager.Init()
        {
            _players = new List<Player>();

            broadcaster.OnLogin += Login;
            broadcaster.OnMove += Move;

            listener.OnDisconnect += Disconnect;
        }

        private void Disconnect(object sender, NetPeer peer)
        {
            _players.RemoveAll(x => x.Peer == peer);
        }

        private byte GetMaxPlayers()
        {
            return byte.Parse(configuration["MaxPlayers"]);
        }

        private Player GetPlayer(int globalID)
        {
            return _players.FirstOrDefault(x => x.GlobalID == globalID);
        }

        private void Login(object peer, LoginRequest packet)
        {
            if (_players.Count == GetMaxPlayers())
            {
                SendServerFull(peer);
                return;
            }

            var globalID = (byte)(_players.Count + 1);

            var player = new Player
            {
                GlobalID = globalID,
                Peer = peer as NetPeer,
                Position = new Vector3(),
            };

            _players.Add(player);

            LoginResponse(peer, player);
            PlayerJoined(peer, player);
        }

        private void LoginResponse(object peer, Player player)
        {
            var packet = new LoginResponse
            {
                GlobalID = player.GlobalID,
            };

            sender.Send(peer as NetPeer, packet);
        }

        private void Move(object peer, Move packet)
        {
            var player = GetPlayer(packet.GlobalID);
            var newPosition = new Vector3(packet.X, packet.Y, packet.Z);

            if (player.Position.Equals(newPosition))
            {
                return;
            }

            player.Position = newPosition;

            var movePacket = new Move
            {
                GlobalID = player.GlobalID,
                X = player.Position.X,
                Y = player.Position.Y,
                Z = player.Position.Z,
            };

            sender.Broadcast(peer as NetPeer, movePacket);
        }

        private void PlayerJoined(object peer, Player player)
        {
            var packet = new PlayerJoined
            {
                GlobalID = player.GlobalID,
            };

            sender.Broadcast(peer as NetPeer, packet);
        }

        private void SendServerFull(object peer)
        {
            var error = new Error
            {
                Level = Error.CRITICAL,
                Message = "Server full!",
            };

            sender.Send(peer as NetPeer, error);
        }
    }
}