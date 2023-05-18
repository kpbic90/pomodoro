using MediatR;

namespace Shared.Models
{
    public class MQMessage : INotification
    {
        public string Body { get; set; }
        public ulong DeliveryTag { get; set; }
    }
}
