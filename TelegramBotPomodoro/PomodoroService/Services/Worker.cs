using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Models;
using Shared.Services;

namespace PomodoroService.Services
{
    internal class Worker : IHostedService, IDisposable
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfig _iConfig;
        private readonly IMQService _iMQService;

        public Worker(ILogger<Worker> logger, IMQService iMQService, IConfig iConfig)
        {
            _logger = logger;
            _iMQService = iMQService;
            _iConfig = iConfig;
        }

        public void Dispose()
        {
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _iMQService.StartConsuming(_iConfig.RabbitQueue);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
