using Microsoft.Extensions.DependencyInjection;
using MultiplayerServer.Abstractions;
using MultiplayerServer.Services;

namespace MultiplayerServer
{
    public class Startup
    {
        internal static void CondigureServices(IServiceCollection services)
        {
            services.AddHostedService<Worker>();

            services.AddSingleton<IUdpListener, UdpListener>();
            services.AddSingleton<IUdpProcessor, UdpProcessor>();
            services.AddSingleton<IUdpServer, UdpServer>();
        }
    }
}