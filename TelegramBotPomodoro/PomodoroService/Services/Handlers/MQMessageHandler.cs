using MediatR;
using Newtonsoft.Json;
using Shared.Models;
using Shared.Models.Requests;

namespace PomodoroService.Services.Handlers
{
    internal class MQMessageHandler : IRequestHandler<MQMessageHandleRequest, bool>
    {
        private readonly IMediator _mediator;

        public MQMessageHandler(IMediator mediator)
        {
            _mediator = mediator; 
        }

        public Task<bool> Handle(MQMessageHandleRequest request, CancellationToken cancellationToken)
        {
            var body = JsonConvert.DeserializeObject<Message>(request.Body);
            return _mediator.Send<bool>(new MessageHandleRequest { Message = body }, cancellationToken);
        }
    }
}
