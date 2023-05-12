using Microsoft.Extensions.Configuration;

namespace TelegramBotPomodoro.Services.Telegram
{
    internal class TelegramConfigurationService : IMessengerConfigurationService
    {
        private readonly IConfiguration _configuration;

        public TelegramConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Token => _configuration.GetValue<string>("TelegramBot:Token");
    }
}
