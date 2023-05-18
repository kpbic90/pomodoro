using MediatR;
using Shared.Enums.Telegram;

namespace Shared.Models.Telegram
{
    public class Answer : IRequest<bool>
    {
        public AnswerType Type { get; set; }
        public int? EditMessageId { get; set; }
        public string Text { get; set; }
        public long? Reciever { get; set; }
        public IEnumerable<IEnumerable<AnswerKeyboardButton>> AnswerKeyboardButtons { get; set; }
        public IEnumerable<IEnumerable<AnswerInlineButton>> AnswerInlineButtons { get; set; }
    }
}
