using MediatR;
using PomodoroService.Models.Notifications;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class IntervalUpdateHandler : INotificationHandler<IntervalUpdateNotification>
    {
        private readonly IAnswerSender _answerSender;

        public IntervalUpdateHandler(IAnswerSender answerSender)
        {
            _answerSender = answerSender;
        }

        public Task Handle(IntervalUpdateNotification notification, CancellationToken cancellationToken)
        {
            var button = notification.IsRest ? new AnswerInlineButton { Text = string.Format("Skip [{0:mm\\:ss}]", notification.TimeLeft), CallbackData = "/skip" } :
                new AnswerInlineButton { Text = string.Format("Pause [{0:mm\\:ss}]", notification.TimeLeft), CallbackData = "/pause" };

            var text = notification.IsRest ? "Rest for a while, or skip rest interval instantly" : "Work until I say you to stop! Or you can pause for a while...";

            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    button
                }
            };
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = notification.MessageId, Reciever = notification.User, Text = text, AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.CompletedTask;
        }
    }
}
