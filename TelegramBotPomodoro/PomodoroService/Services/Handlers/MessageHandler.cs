using MediatR;
using PomodoroService.Extensions;
using Shared.Models.Requests;

namespace PomodoroService.Services.Handlers
{
    internal class MessageHandler : IRequestHandler<MessageHandleRequest, bool>
    {
        private readonly IMediator _mediator;

        public MessageHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<bool> Handle(MessageHandleRequest request, CancellationToken cancellationToken)
        {
            var commandString = request.Message.GetCommandString();
            var command = commandString.GetCommand(request.Message);
            if (command != null)
                return _mediator.Send(command, cancellationToken);

            // cant find command - drop it from queue
            return Task.FromResult( true );
        }
    }
}
