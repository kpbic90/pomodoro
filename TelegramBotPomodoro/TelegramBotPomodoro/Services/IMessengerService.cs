using Shared.Models;
using Shared.Models.Telegram;

namespace TelegramBotPomodoro.Services
{
    internal interface IMessengerService
    {
        public delegate Task MessageRecivedHandler(IMessage message);

        public event MessageRecivedHandler OnMessageRecieved;

        Task Init();
        Task SendMessage(Answer message);
        Task EditMessage(Answer message);
        Task DeleteMessage(long authorId, int messageId);
    }
}
