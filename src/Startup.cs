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
            services.AddSingleton<IUdpServer, UdpServer>();
        }
    }
}