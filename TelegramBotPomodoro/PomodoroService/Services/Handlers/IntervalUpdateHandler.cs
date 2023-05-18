using MediatR;
using PomodoroService.Models.Notifications;
using PomodoroService.Services.ButtonFactories;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class IntervalUpdateHandler : INotificationHandler<IntervalUpdateNotification>
    {
        private readonly IAnswerSender _answerSender;
        private readonly PauseButtonFactory _pauseButtonFactory;
        private readonly RestButtonFactory _restButtonFactory;

        public IntervalUpdateHandler(IAnswerSender answerSender, PauseButtonFactory pauseButtonFactory, RestButtonFactory restButtonFactory)
        {
            _answerSender = answerSender;
            _pauseButtonFactory = pauseButtonFactory;
            _restButtonFactory = restButtonFactory;
        }

        public Task Handle(IntervalUpdateNotification notification, CancellationToken cancellationToken)
        {
            var button = notification.IsRest ? (AnswerInlineButton)_restButtonFactory.CreateButton(notification.TimeLeft) :
                (AnswerInlineButton)_pauseButtonFactory.CreateButton(notification.TimeLeft);

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
