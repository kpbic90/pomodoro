using MediatR;
using Newtonsoft.Json;
using Shared.Models.Requests;
using Shared.Models.Telegram;

namespace TelegramBotPomodoro.Services.Handlers
{
    internal class MQMessageHandler : IRequestHandler<MQMessageHandleRequest, bool>
    {
        private readonly IMediator _mediator;

        public MQMessageHandler(IMediator mediator)
        {
            _mediator = mediator; 
        }

        public Task<bool> Handle(MQMessageHandleRequest message, CancellationToken cancellationToken)
        {
            var body = JsonConvert.DeserializeObject<Answer>(message.Body);
            return _mediator.Send<bool>(body, cancellationToken);
        }
    }
}
