using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Models;
using Shared.Services;

namespace TelegramBotPomodoro.Services
{
    internal class Worker : IHostedService, IDisposable
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessengerService _messangerService;
        private readonly IConfig _iConfig;
        private readonly IMQService _iMQService;

        public Worker(ILogger<Worker> logger, IMessengerService messangerService, IMQService iMQService, IConfig iConfig)
        {
            _logger = logger;
            _messangerService = messangerService;
            _iConfig = iConfig;
            _iMQService = iMQService;
        }

        public void Dispose()
        {
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messangerService.Init();
            _iMQService.StartConsuming(_iConfig.RabbitQueue, false);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
