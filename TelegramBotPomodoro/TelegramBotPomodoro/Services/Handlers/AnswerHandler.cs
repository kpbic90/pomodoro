using MediatR;
using Shared.Models.Telegram;

namespace TelegramBotPomodoro.Services.Handlers
{
    internal class AnswerHandler : INotificationHandler<Answer>
    {
        private readonly IMessengerService _messangerService;

        public AnswerHandler(IMessengerService messangerService)
        {
            _messangerService = messangerService;
        }

        public Task Handle(Answer answer, CancellationToken cancellationToken)
        {
            switch(answer.Type)
            {
                case Shared.Enums.Telegram.AnswerType.DeleteMessage:
                    _messangerService.DeleteMessage(answer.Reciever.Value, answer.EditMessageId.Value);
                    break;
                case Shared.Enums.Telegram.AnswerType.NewMessage:
                    _messangerService.SendMessage(answer);
                    break;
                case Shared.Enums.Telegram.AnswerType.EditMessage:
                    _messangerService.EditMessage(answer);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
