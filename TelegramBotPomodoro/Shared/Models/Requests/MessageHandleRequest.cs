using MediatR;

namespace Shared.Models.Requests
{
    public class MessageHandleRequest : IRequest<bool>
    {
        public IMessage Message { get; set; }
    }
}
