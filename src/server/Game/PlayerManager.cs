﻿using LiteNetLib;
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
        private readonly IGlobalIDProvider globalIDProvider;
        private readonly IUdpListener listener;
        private readonly ISender sender;
        private List<Player> _players;
        private bool isMasterPresent = false;

        public PlayerManager(
            IPacketBroadcaster broadcaster,
            IConfiguration configuration,
            IGlobalIDProvider globalIDProvider,
            IUdpListener listener,
            ISender sender
            )
        {
            this.broadcaster = broadcaster;
            this.configuration = configuration;
            this.globalIDProvider = globalIDProvider;
            this.listener = listener;
            this.sender = sender;
        }

        void IPlayerManager.Init()
        {
            _players = new List<Player>();

            broadcaster.OnFinishGame += FinishGame_;
            broadcaster.OnLogin += Login;
            broadcaster.OnMove += Move;
            broadcaster.OnPlayerInAssembly += InAssembly;
            broadcaster.OnPlayerInfected += Infect;
            broadcaster.OnPlayerKilled += Kill;
            broadcaster.OnPlayerOutOfAssembly += OutAssembly;
            broadcaster.OnPlayerPoisoned += Poisoned;
            broadcaster.OnRotate += Rotate;
            broadcaster.OnSelectSlot += SelectSlot;
            broadcaster.OnSlotsRequest += SlotsRequest;

            listener.OnDisconnect += Disconnect;
        }

        private void BroadcastSlotSelected(NetPeer peer, Player player)
        {
            var packet = new SlotSelected
            {
                OwnerGlobalID = player.GlobalID,
                Slot = player.Slot,
            };

            sender.Broadcast(peer, packet);
        }

        private void Disconnect(object sender, NetPeer peer)
        {
            var player = GetPlayer(peer);

            if (player.IsMaster)
            {
                isMasterPresent = false;
            }

            _players.RemoveAll(x => x.Peer == peer);

            var packet = new PlayerDisconnected
            {
                GlobalID = player.GlobalID,
            };

            this.sender.Broadcast(peer, packet);
        }

        private void FinishGame_(object peer, FinishGame packet)
        {
            foreach (var player in _players)
            {
                player.InAssembly = false;
                player.IsInfected = false;
                player.IsKilled = false;
                player.Position = Vector3.Zero;
                player.Rotation = Vector3.Zero;
            }
        }

        private byte GetMaxPlayers()
        {
            return byte.Parse(configuration["MaxPlayers"]);
        }

        private string[] GetOccupiedSlots()
        {
            return _players.Where(x => x.Slot != null).Select(x => x.Slot).ToArray();
        }

        private byte[] GetOccupiedSlotsIDs()
        {
            return _players.Where(x => x.Slot != null).Select(x => x.GlobalID).ToArray();
        }

        private Player GetPlayer(byte globalID)
        {
            return _players.FirstOrDefault(x => x.GlobalID == globalID);
        }

        private Player GetPlayer(NetPeer peer)
        {
            return _players.FirstOrDefault(x => x.Peer == peer);
        }

        private void InAssembly(object peer, PlayerInAssembly packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player != null)
            {
                player.InAssembly = true;
            }
        }

        private void Infect(object peer, PlayerInfected packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player != null)
            {
                player.IsInfected = true;
            }
        }

        private void Kill(object peer, PlayerKilled packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player != null)
            {
                player.IsKilled = true;
            }
        }

        private void Login(object peer, LoginRequest packet)
        {
            if (_players.Count == GetMaxPlayers())
            {
                SendServerFull(peer);
                return;
            }

            var globalID = globalIDProvider.GetNextID();

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
            var occupiedSlots = GetOccupiedSlots();
            var occupiedSlotsIDs = GetOccupiedSlotsIDs();
            var isMaster = false;

            if (!isMasterPresent)
            {
                isMasterPresent = true;
                isMaster = true;
                player.IsMaster = true;
            }

            var packet = new LoginResponse
            {
                GlobalID = player.GlobalID,
                IsMaster = isMaster,
                OccupiedSlots = occupiedSlots,
                OccupiedSlotsIDs = occupiedSlotsIDs,
            };

            sender.Send(peer as NetPeer, packet);
        }

        private void Move(object peer, Move packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player != null)
            {
                player.Position = new Vector3(packet.X, packet.Y, packet.Z);
            }
        }

        private void OutAssembly(object peer, PlayerOutOfAssembly packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player != null)
            {
                player.InAssembly = false;
            }
        }

        private void PlayerJoined(object peer, Player player)
        {
            var packet = new PlayerJoined
            {
                GlobalID = player.GlobalID,
            };

            sender.Broadcast(peer as NetPeer, packet);
        }

        private void Poisoned(object peer, PlayerPoisoned packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player != null)
            {
                player.IsPoisoned = true;
            }
        }

        private void Rotate(object peer, Rotate packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player != null)
            {
                player.Rotation = new Vector3(packet.X, packet.Y, packet.Z);
            }
        }

        private void SelectSlot(object peer, SelectSlotRequest packet)
        {
            var player = GetPlayer(packet.GlobalID);

            if (player == null)
            {
                return;
            }

            if (_players.Any(x => x.Slot == packet.Slot))
            {
                throw new GameException("Slot already selected.", peer as NetPeer);
            }

            player.Slot = packet.Slot;

            var packetResponse = new SelectSlotResponse
            {
                Slot = player.Slot,
                Success = true,
            };

            sender.Send(player.Peer, packetResponse);
            BroadcastSlotSelected(peer as NetPeer, player);
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

        private void SlotsRequest(object peer, SlotsRequest packet)
        {
            var player = GetPlayer(packet.GlobalID);
            var occupiedSlots = GetOccupiedSlots();
            var occupiedSlotsIDs = GetOccupiedSlotsIDs();

            var responsePacket = new SlotsResponse
            {
                MySlot = player.Slot,
                OccupiedSlots = occupiedSlots,
                OccupiedSlotsIDs = occupiedSlotsIDs,
            };

            sender.Send(peer as NetPeer, responsePacket);
        }
    }
}