using MediatR;
using PomodoroService.Models.Commands;
using Shared.Models.Telegram;
using Shared.Services.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroService.Services.Handlers
{
    internal class ResumeCommandHandler : INotificationHandler<ResumeCommand>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;

        public ResumeCommandHandler(IAnswerSender answerSender, IIntervalController intervalController)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
        }

        public Task Handle(ResumeCommand notification, CancellationToken cancellationToken)
        {
            _intervalController.ResumeInterval(notification.Message.Author);
            var timeleft = _intervalController.GetInterval(notification.Message.Author).TimeSpan;
            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    new AnswerInlineButton
                    {
                        Text = string.Format("Pause [{0:mm\\:ss}]", timeleft), CallbackData = "/pause"
                    }
                }
            };
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = notification.Message.Id, Reciever = notification.Message.Author, Text = "Work until I say you to stop! Or you can pause for a while...", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.CompletedTask;
        }
    }
}
