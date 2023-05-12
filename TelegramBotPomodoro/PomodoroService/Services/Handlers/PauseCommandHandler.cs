using MediatR;
using PomodoroService.Models.Commands;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class PauseCommandHandler : INotificationHandler<PauseCommand>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;

        public PauseCommandHandler(IAnswerSender answerSender, IIntervalController intervalController)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
        }

        public Task Handle(PauseCommand notification, CancellationToken cancellationToken)
        {
            _intervalController.PauseInterval(notification.Message.Author);
            var timeleft = _intervalController.GetInterval(notification.Message.Author).TimeSpan;
            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    new AnswerInlineButton
                    {
                        Text = string.Format("Resume [{0:mm\\:ss}]", timeleft), CallbackData = "/resume"
                    }
                }
            };
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = notification.Message.Id, Reciever = notification.Message.Author, Text = "Timer stopped. Resume when you are ready", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.CompletedTask;
        }
    }
}
