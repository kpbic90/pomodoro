using MediatR;
using Shared.Models.Requests;

namespace TelegramBotPomodoro.Services.Handlers
{
    internal class MessageHandler : IRequestHandler<MessageHandleRequest, bool>
    {
        private readonly IMessageProccessor _messageProccessor;

        public MessageHandler(IMessageProccessor messageProccessor)
        {
            _messageProccessor = messageProccessor;
        }

        public Task<bool> Handle(MessageHandleRequest request, CancellationToken cancellationToken)
        {
            _messageProccessor.Proccess(request.Message, cancellationToken);
            return Task.FromResult(true);
        }
    }
}
