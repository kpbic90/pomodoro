using Shared.Models;
using Shared.Models.Telegram;

namespace TelegramBotPomodoro.Services
{
    internal interface IMessengerService
    {
        public delegate Task MessageRecivedHandler(IMessage message);

        public event MessageRecivedHandler OnMessageRecieved;

        Task Init();
        Task<bool> SendMessage(Answer message);
        Task<bool> EditMessage(Answer message);
        Task<bool> DeleteMessage(long authorId, int messageId);
    }
}
