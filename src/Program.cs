using Microsoft.Extensions.Hosting;

namespace MultiplayerServer
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => Startup.CondigureServices(services));

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}