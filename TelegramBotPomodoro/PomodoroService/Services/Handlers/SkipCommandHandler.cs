using MediatR;
using PomodoroService.Models.Commands;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class SkipCommandHandler : IRequestHandler<SkipCommand, bool>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;

        public SkipCommandHandler(IAnswerSender answerSender, IIntervalController intervalController)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
        }

        public Task<bool> Handle(SkipCommand request, CancellationToken cancellationToken)
        {
            _intervalController.SkipInterval(request.Message.Author);
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
            var answerNew = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = request.Message.Id, Reciever = request.Message.Author, Text = "Rest skipped. Another work round?", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answerNew);
            return Task.FromResult(true);
        }
    }
}
