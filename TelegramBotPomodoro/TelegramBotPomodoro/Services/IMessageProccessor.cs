using Shared.Models;

namespace TelegramBotPomodoro.Services
{
    internal interface IMessageProccessor
    {
        Task Proccess(IMessage message, CancellationToken cancellationToken);
    }
}
