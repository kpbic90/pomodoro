using MediatR;
using PomodoroService.Models.Commands;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class ResumeCommandHandler : IRequestHandler<ResumeCommand, bool>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;

        public ResumeCommandHandler(IAnswerSender answerSender, IIntervalController intervalController)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
        }

        public Task<bool> Handle(ResumeCommand request, CancellationToken cancellationToken)
        {
            _intervalController.ResumeInterval(request.Message.Author);
            var timeleft = _intervalController.GetInterval(request.Message.Author).TimeSpan;
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
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = request.Message.Id, Reciever = request.Message.Author, Text = "Work until I say you to stop! Or you can pause for a while...", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.FromResult(true);
        }
    }
}
