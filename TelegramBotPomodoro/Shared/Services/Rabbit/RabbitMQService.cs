using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Models;
using System.Text;

namespace Shared.Services.Rabbit
{
    public class RabbitMQService : IMQService
    {
        private readonly IModel _channel;
        private readonly IConfig _configurationService;
        private readonly IMediator _mediator;

        public RabbitMQService(IConfig configurationService, IMediator mediator)
        {
            _configurationService = configurationService;
            _mediator = mediator;
            var factory = new ConnectionFactory
            {
                HostName = configurationService.RabbitHostName,
                UserName = configurationService.RabbitUser,
                Password = configurationService.RabbitPassword,
                Port = configurationService.RabbitPort
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void ExchangeDeclare(string exchangeName)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
        }

        public void BindQueue(string exchange, string routingKey, string queueName)
        {
            _channel.QueueBind(queue: queueName,
                      exchange: exchange,
                      routingKey: routingKey);
        }

        public void Send(string exchange, string routingKey, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange, routingKey, null, body);
        }

        public void StartConsuming(string queue)
        {
            QueueDeclare(queue);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnMessageRecieved;
            _channel.BasicConsume(queue,
                true,
                consumer);
        }

        private void QueueDeclare(string queue)
        {
            _channel.QueueDeclare(queue,
                true,
                false,
                false,
                null);
        }

        private void OnMessageRecieved(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Received [{1:dd.MM.yyyy HH:mm:ss}] {0}", message, DateTime.Now);
            _mediator.Publish(new MQMessage { Body = message });
        }
    }
}
