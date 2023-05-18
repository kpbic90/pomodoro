using MediatR;
using PomodoroService.Models.Notifications;
using PomodoroService.Services.ButtonFactories;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class IntervalEndHandler : INotificationHandler<IntervalEndNotification>
    {
        private readonly IAnswerSender _answerSender;
        private readonly PlayButtonFactory _playButtonFactory;
        private readonly RestButtonFactory _restButtonFactory;

        public IntervalEndHandler(IAnswerSender answerSender, PlayButtonFactory playButtonFactory, RestButtonFactory restButtonFactory)
        {
            _answerSender = answerSender;
            _playButtonFactory = playButtonFactory;
            _restButtonFactory = restButtonFactory;
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
                    (AnswerInlineButton)_playButtonFactory.CreateButton()
                }
            };
            if(!notification.IsRest)
            {
                inlineButtons.Add(new List<AnswerInlineButton>
                {
                    (AnswerInlineButton)_restButtonFactory.CreateButton()
                });
            }
            var text = notification.IsRest ? "Time to get back to work!" : "Well done! Another round? Or rest for short period";
            var answerNew = new Answer { Type = Shared.Enums.Telegram.AnswerType.NewMessage, Reciever = notification.User, Text = text, AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answerNew);
            return Task.CompletedTask;
        }
    }
}
