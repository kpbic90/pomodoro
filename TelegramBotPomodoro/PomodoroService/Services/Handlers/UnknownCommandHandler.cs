using MediatR;
using PomodoroService.Models.Commands;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class UnknownCommandHandler : IRequestHandler<UnknownCommand, bool>
    {
        private readonly IAnswerSender _answerSender;

        public UnknownCommandHandler(IAnswerSender answerSender)
        {
            _answerSender = answerSender;
        }

        public Task<bool> Handle(UnknownCommand request, CancellationToken cancellationToken)
        {
            if (request.Message.Id.HasValue)
                _answerSender.DeleteMessage(request.Message.Author, request.Message.Id.Value);
            return Task.FromResult(true);
        }
    }
}
