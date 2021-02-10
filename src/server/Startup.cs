using Microsoft.Extensions.DependencyInjection;
using MultiplayerServer.Abstractions;
using MultiplayerServer.Game;
using MultiplayerServer.Services;

namespace MultiplayerServer
{
    public class Startup
    {
        internal static void CondigureServices(IServiceCollection services)
        {
            services.AddHostedService<Worker>();

            services.AddSingleton<IGameManager, GameManager>();
            services.AddSingleton<IPacketBroadcaster, PacketBroadcaster>();
            services.AddSingleton<IPlayerManager, PlayerManager>();
            services.AddSingleton<ISyncManager, SyncManager>();
            services.AddSingleton<IUdpListener, UdpListener>();
            services.AddSingleton<IUdpProcessor, UdpProcessor>();
            services.AddSingleton<IUdpServer, UdpServer>();

            services.AddTransient<ISender, Sender>();
        }
    }
}