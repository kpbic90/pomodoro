using MediatR;
using Shared.Models;

namespace TelegramBotPomodoro.Services.Handlers
{
    internal class MessageHandler : INotificationHandler<Message>
    {
        private readonly IMessageProccessor _messageProccessor;

        public MessageHandler(IMessageProccessor messageProccessor)
        {
            _messageProccessor = messageProccessor;
        }

        public Task Handle(Message message, CancellationToken cancellationToken)
        {
            _messageProccessor.Proccess(message, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
