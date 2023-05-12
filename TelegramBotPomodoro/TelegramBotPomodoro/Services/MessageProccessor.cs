using Newtonsoft.Json;
using Shared.Models;
using Shared.Services;

namespace TelegramBotPomodoro.Services
{
    internal class MessageProccessor : IMessageProccessor
    {
        private readonly IMQService _iMQService;
        private readonly IConfig _config;

        public MessageProccessor(IMQService iMQService, IConfig config)
        {
            _iMQService = iMQService;
            _config = config;
        }

        public async Task Proccess(IMessage message, CancellationToken cancellationToken)
        {
            _iMQService.Send(_config.RabbitExchange, _config.RabbitRoutingKey, JsonConvert.SerializeObject(message));
        }       
    }
}
