using MediatR;
using PomodoroService.Models;
using PomodoroService.Models.Commands;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class RestCommandHandler : INotificationHandler<RestCommand>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;
        private readonly IPomodoroConfig _config;

        public RestCommandHandler(IAnswerSender answerSender, IIntervalController intervalController, IPomodoroConfig config)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
            _config = config;
        }

        public Task Handle(RestCommand notification, CancellationToken cancellationToken)
        {
            _intervalController.StartInterval(notification.Message.Author, notification.Message.Id.Value, _config.DefaultRestLength, true);
            var timeleft = _intervalController.GetInterval(notification.Message.Author)?.TimeSpan;
            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    new AnswerInlineButton
                    {
                        Text = string.Format("Skip [{0:mm\\:ss}]", timeleft), CallbackData = "/skip"
                    }
                }
            };
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = notification.Message.Id, Reciever = notification.Message.Author, Text = "Rest for a while, or skip rest interval instantly", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.CompletedTask;
        }
    }
}
