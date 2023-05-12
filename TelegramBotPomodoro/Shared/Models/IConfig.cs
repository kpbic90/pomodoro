namespace Shared.Models
{
    public interface IConfig
    {
        string RabbitHostName { get; }
        string RabbitUser { get; }
        string RabbitPassword { get; }
        int RabbitPort { get; }
        string RabbitExchange { get; }
        string RabbitRoutingKey { get; }
        string RabbitQueue { get; }
    }
}
