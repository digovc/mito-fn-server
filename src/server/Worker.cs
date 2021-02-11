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
        private readonly IGameManager gameManager;
        private readonly IUdpListener listener;
        private readonly ILogger<Worker> logger;
        private readonly IPlayerManager playerManager;
        private readonly IUdpProcessor processor;
        private readonly IUdpServer server;
        private readonly ISyncronizer syncronizer;
        private List<ITicker> tickers;

        public Worker(
            IGameManager gameManager,
            ILogger<Worker> logger,
            IUdpListener listener,
            IPlayerManager playerManager,
            IUdpProcessor processor,
            IUdpServer server,
            ISyncronizer syncronizer
            )
        {
            this.gameManager = gameManager;
            this.logger = logger;
            this.listener = listener;
            this.playerManager = playerManager;
            this.processor = processor;
            this.server = server;
            this.syncronizer = syncronizer;
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
            await Task.Delay(65, stoppingToken);
        }

        private void Init()
        {
            listener.Init();
            server.Init();
            processor.Init();
            gameManager.Init();
            playerManager.Init();
            syncronizer.Init();

            InitTickers();
        }

        private void InitTickers()
        {
            tickers = new List<ITicker>
            {
                server
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