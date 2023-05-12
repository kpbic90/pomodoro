using Newtonsoft.Json;
using Shared.Models;
using Shared.Models.Telegram;

namespace Shared.Services.Telegram
{
    public class AnswerSender : IAnswerSender
    {
        private readonly IMQService _iMQService;
        private readonly IConfig _iConfig;

        public AnswerSender(IMQService iMQService, IConfig iConfig)
        {
            _iMQService = iMQService;
            _iConfig = iConfig;
        }

        public void SendMessage(string exchange, string routingKey, Answer answer)
        {
            _iMQService.Send(exchange, routingKey, JsonConvert.SerializeObject(answer));
        }

        public void DeleteMessage(string exchange, string routingKey, long recieverId, int messageId)
        {
            var answer = new Answer
            {
                Type = Shared.Enums.Telegram.AnswerType.DeleteMessage,
                EditMessageId = messageId,
                Reciever = recieverId
            };

            _iMQService.Send(exchange, routingKey, JsonConvert.SerializeObject(answer));
        }

        public void SendMessage(Answer answer)
        {
            SendMessage(_iConfig.RabbitExchange, _iConfig.RabbitRoutingKey, answer);
        }

        public void DeleteMessage(long recieverId, int messageId)
        {
            DeleteMessage(_iConfig.RabbitExchange, _iConfig.RabbitRoutingKey, recieverId, messageId);
        }
    }
}
