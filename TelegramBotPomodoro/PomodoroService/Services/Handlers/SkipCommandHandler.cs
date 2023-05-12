using MediatR;
using PomodoroService.Models.Commands;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class SkipCommandHandler : INotificationHandler<SkipCommand>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;

        public SkipCommandHandler(IAnswerSender answerSender, IIntervalController intervalController)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
        }

        public Task Handle(SkipCommand notification, CancellationToken cancellationToken)
        {
            _intervalController.SkipInterval(notification.Message.Author);
            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    new AnswerInlineButton
                    {
                        Text = "Start Working!", CallbackData = "/play"
                    }
                }
            };
            var answerNew = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = notification.Message.Id, Reciever = notification.Message.Author, Text = "Rest skipped. Another work round?", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answerNew);
            return Task.CompletedTask;
        }
    }
}
