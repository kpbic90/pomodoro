using MediatR;
using Newtonsoft.Json;
using Shared.Models;
using Shared.Models.Telegram;

namespace TelegramBotPomodoro.Services.Handlers
{
    internal class MQMessageHandler : INotificationHandler<MQMessage>
    {
        private readonly IPublisher _publisher;

        public MQMessageHandler(IPublisher publisher)
        {
            _publisher = publisher; 
        }

        public Task Handle(MQMessage message, CancellationToken cancellationToken)
        {
            var body = JsonConvert.DeserializeObject<Answer>(message.Body);
            _publisher.Publish(body, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
