using MediatR;
using PomodoroService.Models.Commands;
using PomodoroService.Services.ButtonFactories;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class PauseCommandHandler : IRequestHandler<PauseCommand, bool>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;
        private readonly ResumeButtonFactory _resumeButtonFactory;

        public PauseCommandHandler(IAnswerSender answerSender, IIntervalController intervalController, ResumeButtonFactory resumeButtonFactory)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
            _resumeButtonFactory = resumeButtonFactory;
        }

        public Task<bool> Handle(PauseCommand request, CancellationToken cancellationToken)
        {
            _intervalController.PauseInterval(request.Message.Author);
            var timeleft = _intervalController.GetInterval(request.Message.Author).TimeSpan;
            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    (AnswerInlineButton)_resumeButtonFactory.CreateButton(timeleft)
                }
            };
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = request.Message.Id, Reciever = request.Message.Author, Text = "Timer stopped. Resume when you are ready", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.FromResult(true);
        }
    }
}
