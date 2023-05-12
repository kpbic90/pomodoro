using Microsoft.Extensions.Configuration;

namespace Shared.Models
{
    public class Config : IConfig
    {
        private readonly IConfiguration _configuration;

        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RabbitHostName => _configuration.GetSection("RabbitMQ:HostName")?.Value;
        public string RabbitUser => _configuration.GetSection("RabbitMQ:UserName")?.Value;
        public string RabbitPassword => _configuration.GetSection("RabbitMQ:Password")?.Value;
        public int RabbitPort => int.Parse(_configuration.GetSection("RabbitMQ:Port")?.Value);
        public string RabbitExchange => _configuration.GetSection("RabbitMQ:Exchange")?.Value;
        public string RabbitRoutingKey => _configuration.GetSection("RabbitMQ:RoutingKey")?.Value;
        public string RabbitQueue => _configuration.GetSection("RabbitMQ:Queue")?.Value;
    }
}
