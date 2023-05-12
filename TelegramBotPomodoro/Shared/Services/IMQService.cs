namespace Shared.Services
{
    public interface IMQService
    {
        void ExchangeDeclare(string exchangeName);
        void BindQueue(string exchange, string routingKey, string queueName);
        void StartConsuming(string queue);
        void Send(string exchange, string routingKey, string message);
    }
}
