using MediatR;
using PomodoroService.Extensions;
using Shared.Models;

namespace PomodoroService.Services.Handlers
{
    internal class MessageHandler : INotificationHandler<Message>
    {
        private readonly IPublisher _publisher;

        public MessageHandler(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Handle(Message message, CancellationToken cancellationToken)
        {
            var commandString = message.GetCommandString();
            var command = commandString.GetCommand(message);
            if (command != null)
                await _publisher.Publish(command, cancellationToken);
        }
    }
}
