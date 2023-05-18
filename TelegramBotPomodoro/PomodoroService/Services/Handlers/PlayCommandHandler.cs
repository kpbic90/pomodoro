using MediatR;
using PomodoroService.Models;
using PomodoroService.Models.Commands;
using PomodoroService.Services.ButtonFactories;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class PlayCommandHandler : IRequestHandler<PlayCommand, bool>
    {
        private readonly IAnswerSender _answerSender;
        private readonly IIntervalController _intervalController;
        private readonly IPomodoroConfig _config;
        private readonly PauseButtonFactory _pauseButtonFactory;

        public PlayCommandHandler(IAnswerSender answerSender, IIntervalController intervalController, IPomodoroConfig config, PauseButtonFactory pauseButtonFactory)
        {
            _answerSender = answerSender;
            _intervalController = intervalController;
            _config = config;
            _pauseButtonFactory = pauseButtonFactory;
        }

        public Task<bool> Handle(PlayCommand request, CancellationToken cancellationToken)
        {
            _intervalController.StartInterval(request.Message.Author, request.Message.Id.Value, _config.DefaultIntervalLength, false);
            var timeleft = _intervalController.GetInterval(request.Message.Author)?.TimeSpan;
            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    (AnswerInlineButton)_pauseButtonFactory.CreateButton(timeleft)
                }
            };
            var answer = new Answer { Type = Shared.Enums.Telegram.AnswerType.EditMessage, EditMessageId = request.Message.Id, Reciever = request.Message.Author, Text = "Work until I say you to stop! Or you can pause for a while...", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer);
            return Task.FromResult( true );
        }
    }
}
