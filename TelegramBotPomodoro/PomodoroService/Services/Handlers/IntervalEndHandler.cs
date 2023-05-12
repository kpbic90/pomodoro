using MediatR;
using PomodoroService.Models.Notifications;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class IntervalEndHandler : INotificationHandler<IntervalEndNotification>
    {
        private readonly IAnswerSender _answerSender;

        public IntervalEndHandler(IAnswerSender answerSender)
        {
            _answerSender = answerSender;
        }

        public Task Handle(IntervalEndNotification notification, CancellationToken cancellationToken)
        {
            var textComplete = notification.IsRest ? "Rest complete!" : "Interval complete!";
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = notification.MessageId, Reciever = notification.User, Text = textComplete };
            _answerSender.SendMessage(answer);
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
            if(!notification.IsRest)
            {
                inlineButtons.Add(new List<AnswerInlineButton>
                {
                    new AnswerInlineButton
                    {
                        Text = "Rest", CallbackData = "/rest"
                    }
                });
            }
            var text = notification.IsRest ? "Time to get back to work!" : "Well done! Another round? Or rest for short period";
            var answerNew = new Answer { Type = Shared.Enums.Telegram.AnswerType.NewMessage, Reciever = notification.User, Text = text, AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answerNew);
            return Task.CompletedTask;
        }
    }
}
