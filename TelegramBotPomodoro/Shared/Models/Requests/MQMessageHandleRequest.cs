using MediatR;

namespace Shared.Models.Requests
{
    public class MQMessageHandleRequest : IRequest<bool>
    {
        public string Body { get; set; }
        public ulong DeliveryTag { get; set; }
    }
}
