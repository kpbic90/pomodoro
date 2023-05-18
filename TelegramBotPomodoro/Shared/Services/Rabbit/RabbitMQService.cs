using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Models;
using Shared.Models.Requests;
using System.Text;

namespace Shared.Services.Rabbit
{
    public class RabbitMQService : IMQService
    {
        private readonly IModel _channel;
        private readonly IConfig _configurationService;
        private readonly IMediator _mediator;

        private bool autoAck;

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
            StartConsuming(queue, true);
        }

        public void StartConsuming(string queue, bool autoAck)
        {
            QueueDeclare(queue);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnMessageRecieved;
            _channel.BasicConsume(queue,
                autoAck,
                consumer);
            this.autoAck = autoAck;
        }

        public void Ack(ulong Tag)
        {
            _channel.BasicAck(Tag, false);
        }

        public void Nack(ulong Tag, bool requeue)
        {
            _channel.BasicNack(Tag, false, requeue);
        }

        private void QueueDeclare(string queue)
        {
            _channel.QueueDeclare(queue,
                true,
                false,
                false,
                null);
        }

        private async void OnMessageRecieved(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Received [{1:dd.MM.yyyy HH:mm:ss}] {0}", message, DateTime.Now);
            var result = await _mediator.Send(new MQMessageHandleRequest { Body = message, DeliveryTag = ea.DeliveryTag });
            if (autoAck)
                return;

            if(result == true )
            {
                Ack(ea.DeliveryTag);
            }
            else
            {
                Nack(ea.DeliveryTag, true);
            }
        }
    }
}
