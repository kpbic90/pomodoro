using MediatR;
using Shared.Models.Telegram;

namespace TelegramBotPomodoro.Services.Handlers
{
    internal class AnswerHandler : IRequestHandler<Answer, bool>
    {
        private readonly IMessengerService _messangerService;

        public AnswerHandler(IMessengerService messangerService)
        {
            _messangerService = messangerService;
        }

        public async Task<bool> Handle(Answer answer, CancellationToken cancellationToken)
        {
            var result = false;
            switch(answer.Type)
            {
                case Shared.Enums.Telegram.AnswerType.DeleteMessage:
                    result = await _messangerService.DeleteMessage(answer.Reciever.Value, answer.EditMessageId.Value);
                    break;
                case Shared.Enums.Telegram.AnswerType.NewMessage:
                    result = await _messangerService.SendMessage(answer);
                    break;
                case Shared.Enums.Telegram.AnswerType.EditMessage:
                    result = await _messangerService.EditMessage(answer);
                    break;
            }

            return result;
        }
    }
}
