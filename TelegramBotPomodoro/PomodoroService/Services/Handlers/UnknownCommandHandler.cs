using MediatR;
using PomodoroService.Models.Commands;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class UnknownCommandHandler : INotificationHandler<UnknownCommand>
    {
        private readonly IAnswerSender _answerSender;

        public UnknownCommandHandler(IAnswerSender answerSender)
        {
            _answerSender = answerSender;
        }

        public Task Handle(UnknownCommand command, CancellationToken cancellationToken)
        {
            if(command.Message.Id.HasValue)
                _answerSender.DeleteMessage(command.Message.Author, command.Message.Id.Value);
            return Task.CompletedTask;
        }
    }
}
