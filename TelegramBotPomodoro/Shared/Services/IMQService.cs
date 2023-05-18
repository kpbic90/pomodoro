namespace Shared.Services
{
    public interface IMQService
    {
        void ExchangeDeclare(string exchangeName);
        void BindQueue(string exchange, string routingKey, string queueName);
        void StartConsuming(string queue);
        void StartConsuming(string queue, bool autoAck);
        void Send(string exchange, string routingKey, string message);
        void Ack(ulong Tag);
        void Nack(ulong Tag, bool requeue);
    }
}
