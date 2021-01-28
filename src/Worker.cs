using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MultiplayerServer.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiplayerServer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IUdpServer udpServer;
        private List<ITicker> tickers;

        public Worker(
            ILogger<Worker> logger,
            IUdpServer udpServer
            )
        {
            this.logger = logger;
            this.udpServer = udpServer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Init();

            while (!stoppingToken.IsCancellationRequested)
            {
                Tick();
                await Delay(stoppingToken);
            }
        }

        private static async Task Delay(CancellationToken stoppingToken)
        {
            await Task.Delay(15, stoppingToken);
        }

        private void Init()
        {
            udpServer.Init();

            InitTickers();
        }

        private void InitTickers()
        {
            tickers = new List<ITicker>
            {
                udpServer
            };
        }

        private void Tick()
        {
            tickers.ForEach(x => TryTick(x));
        }

        private void TryTick(ITicker ticker)
        {
            try
            {
                ticker.Tick();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error on tick from {ticker.GetType().Name}.", ex);
            }
        }
    }
}