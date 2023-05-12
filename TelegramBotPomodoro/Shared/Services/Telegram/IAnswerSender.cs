using Shared.Models.Telegram;

namespace Shared.Services.Telegram
{
    public interface IAnswerSender
    {
        void SendMessage(Answer answer);
        void DeleteMessage(long recieverId, int messageId);
        void SendMessage(string exchange, string routingKey, Answer answer);
        void DeleteMessage(string exchange, string routingKey, long recieverId, int messageId);
    }
}
