using MediatR;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Shared.Models;
using Shared.Models.Telegram;

namespace TelegramBotPomodoro.Services.Telegram
{
    internal class TelegramService : IMessengerService
    {
        private static TelegramBotClient? Bot;
        private readonly IMessengerConfigurationService _configurationService;
        private readonly ILogger<Worker> _logger;
        private readonly IPublisher _publisher;

        public TelegramService(ILogger<Worker> logger, IMessengerConfigurationService configurationService, IPublisher publisher)
        {
            _logger = logger;
            _configurationService = configurationService;
            _publisher = publisher;
        }


        public event IMessengerService.MessageRecivedHandler OnMessageRecieved;

        public async Task Init()
        {
            Bot = new TelegramBotClient(_configurationService.Token);
            using var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions();
            Bot.StartReceiving(HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cts.Token);
        }

        public async Task SendMessage(Answer message)
        {
            IReplyMarkup replyMarkup = null;
            if(message.AnswerInlineButtons?.Any() == true)
            {
                var rows = new List<List<InlineKeyboardButton>>();
                foreach(var irow in message.AnswerInlineButtons)
                {
                    var row = new List<InlineKeyboardButton>();
                    foreach(var item in irow)
                    {
                        var button = new InlineKeyboardButton(item.Text);
                        button.CallbackData = item.CallbackData;
                        row.Add(button);
                    }
                    rows.Add(row);
                }
                replyMarkup = new InlineKeyboardMarkup(rows);
            }
            else if(message.AnswerKeyboardButtons?.Any() == true)
            {
                var rows = new List<List<KeyboardButton>>();
                foreach (var irow in message.AnswerKeyboardButtons)
                {
                    var row = new List<KeyboardButton>();
                    foreach (var item in irow)
                    {
                        var button = new KeyboardButton(item.Text);
                        WebAppInfo webAppInfo = new WebAppInfo();
                        webAppInfo.Url = "https://revenkroz.github.io/telegram-web-app-bot-example/index.html";
                        button.WebApp = webAppInfo;
                        row.Add(button);
                    }
                    rows.Add(row);
                }
                replyMarkup = new ReplyKeyboardMarkup(rows);
            }

            await Bot.SendTextMessageAsync(message.Reciever, message.Text, replyMarkup: replyMarkup);
        }

        public async Task EditMessage(Answer message)
        {
            InlineKeyboardMarkup replyMarkup = null;
            if (message.AnswerInlineButtons?.Any() == true)
            {
                var rows = new List<List<InlineKeyboardButton>>();
                foreach (var irow in message.AnswerInlineButtons)
                {
                    var row = new List<InlineKeyboardButton>();
                    foreach (var item in irow)
                    {
                        var button = new InlineKeyboardButton(item.Text);
                        button.CallbackData = item.CallbackData;
                        row.Add(button);
                    }
                    rows.Add(row);
                }
                replyMarkup = new InlineKeyboardMarkup(rows);
            }

            await Bot.EditMessageTextAsync(message.Reciever, message.EditMessageId.Value, message.Text, replyMarkup: replyMarkup);
        }

        public async Task DeleteMessage(long authorId, int messageId)
        {
            await Bot.DeleteMessageAsync(authorId, messageId);
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogError(errorMessage);
            return Task.CompletedTask;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await _publisher.Publish(new Shared.Models.Message { Id = update.Message?.MessageId, Text = update.Message?.Text, Author = update.Message.From.Id, Receiver = update.Message.Chat.Id }, cancellationToken);
                    break;
                case UpdateType.EditedMessage:
                    await _publisher.Publish(new Shared.Models.Message { Id = update.Message?.MessageId, Text = update.EditedMessage?.Text, Author = update.Message.From.Id, Receiver = update.Message.Chat.Id }, cancellationToken);
                    break;
                case UpdateType.Unknown:
                    break;
                case UpdateType.InlineQuery:
                    break;
                case UpdateType.ChosenInlineResult:
                    break;
                case UpdateType.CallbackQuery:
                    await _publisher.Publish(new Shared.Models.Message { Id = update.CallbackQuery.Message.MessageId, Text = update.CallbackQuery?.Data, Author = update.CallbackQuery.From.Id }, cancellationToken);
                    break;
                case UpdateType.ChannelPost:
                    break;
                case UpdateType.EditedChannelPost:
                    break;
                case UpdateType.ShippingQuery:
                    break;
                case UpdateType.PreCheckoutQuery:
                    break;
                case UpdateType.Poll:
                    break;
                case UpdateType.PollAnswer:
                    break;
                case UpdateType.MyChatMember:
                    break;
                case UpdateType.ChatMember:
                    break;
                case UpdateType.ChatJoinRequest:
                    break;
                default:
                    _logger.LogWarning($"Unknown update type: {update.Type}");
                    break;
            }
        }
    }
}
