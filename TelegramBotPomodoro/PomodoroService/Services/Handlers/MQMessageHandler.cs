using MediatR;
using Newtonsoft.Json;
using Shared.Models;

namespace PomodoroService.Services.Handlers
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
            var body = JsonConvert.DeserializeObject<Message>(message.Body);
            _publisher.Publish(body, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
