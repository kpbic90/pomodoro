using MediatR;
using PomodoroService.Models;
using PomodoroService.Models.Commands;
using PomodoroService.Services.ButtonFactories;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class RestCommandHandler : IRequestHandler<RestCommand, bool>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;
        private readonly IPomodoroConfig _config;
        private readonly SkipButtonFactory _skipButtonFactory;

        public RestCommandHandler(IAnswerSender answerSender, IIntervalController intervalController, IPomodoroConfig config, SkipButtonFactory skipButtonFactory)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
            _config = config;
            _skipButtonFactory = skipButtonFactory;
        }

        public Task<bool> Handle(RestCommand request, CancellationToken cancellationToken)
        {
            _intervalController.StartInterval(request.Message.Author, request.Message.Id.Value, _config.DefaultRestLength, true);
            var timeleft = _intervalController.GetInterval(request.Message.Author)?.TimeSpan;
            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    (AnswerInlineButton)_skipButtonFactory.CreateButton(timeleft)
                }
            };
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = request.Message.Id, Reciever = request.Message.Author, Text = "Rest for a while, or skip rest interval instantly", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.FromResult(true);
        }
    }
}
