using MediatR;
using PomodoroService.Models.Commands;
using Shared.Models.Telegram;
using Shared.Services.Telegram;

namespace PomodoroService.Services.Handlers
{
    internal class StartCommandHandler : INotificationHandler<StartCommand>
    {
        private readonly IAnswerSender _answerSender;

        public StartCommandHandler(IAnswerSender answerSender)
        {
            _answerSender = answerSender;
        }

        public Task Handle(StartCommand command, CancellationToken cancellationToken)
        {
            var answerClear = new Answer { Type = Shared.Enums.Telegram.AnswerType.DeleteMessage, EditMessageId = command.Message.Id, Reciever = command.Message.Author };
            _answerSender.SendMessage(answerClear);
            var keyboardButtons = new List<List<AnswerKeyboardButton>>
            {
                new List<AnswerKeyboardButton>
                {
                    new AnswerKeyboardButton
                    {
                        Text = "Settings"
                    }
                },
                new List<AnswerKeyboardButton>
                {
                    new AnswerKeyboardButton
                    {
                        Text = "Sponsor us", CallbackData = "https://kpbic90.github.io/pomodoro/WebApp/links.html"
                    }
                }
            };
            var answer1 = new Answer { Type = Shared.Enums.Telegram.AnswerType.NewMessage, Reciever = command.Message.Author, Text = "Welcome!", AnswerKeyboardButtons = keyboardButtons };
            _answerSender.SendMessage(answer1);

            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton> 
                { 
                    new AnswerInlineButton
                    {
                        Text = "Start Working!", CallbackData = "/play"
                    }                
                }
            };
            var answer2 = new Answer { Type = Shared.Enums.Telegram.AnswerType.NewMessage, Reciever = command.Message.Author, Text = "Let me help you concentrate on work! Work for next 25 minutes and then I'll message you for rest", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer2);
            return Task.CompletedTask;
        }
    }
}
