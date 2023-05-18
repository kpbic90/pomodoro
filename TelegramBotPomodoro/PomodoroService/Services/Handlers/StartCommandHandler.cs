using MediatR;
using PomodoroService.Models.Commands;
using PomodoroService.Services.ButtonFactories;
using Shared.Models.Telegram;
using TelegramCommon.Services.ButtonFactories;
using Shared.Services.Telegram;
using Microsoft.Extensions.Localization;

namespace PomodoroService.Services.Handlers
{
    internal class StartCommandHandler : IRequestHandler<StartCommand, bool>
    {
        private readonly IAnswerSender _answerSender;
        private readonly PlayButtonFactory _playButtonFactory;
        private readonly SettingsButtonFactory _settingsButtonFactory;
        private readonly SponsorButtonFactory _sponsorButtonFactory;
        private readonly IStringLocalizer<StartCommandHandler> _localizer = null!;

        public StartCommandHandler(IAnswerSender answerSender, PlayButtonFactory playButtonFactory, SettingsButtonFactory settingsButtonFactory, SponsorButtonFactory sponsorButtonFactory, IStringLocalizer<StartCommandHandler> localizer)
        {
            _answerSender = answerSender;
            _playButtonFactory = playButtonFactory;
            _settingsButtonFactory = settingsButtonFactory;
            _sponsorButtonFactory = sponsorButtonFactory;
            _localizer = localizer;
        }

        public Task<bool> Handle(StartCommand request, CancellationToken cancellationToken)
        {
            var answerClear = new Answer { Type = Shared.Enums.Telegram.AnswerType.DeleteMessage, EditMessageId = request.Message.Id, Reciever = request.Message.Author };
            _answerSender.SendMessage(answerClear);
            var keyboardButtons = new List<List<AnswerKeyboardButton>>
            {
                new List<AnswerKeyboardButton>
                {
                    (AnswerKeyboardButton)_settingsButtonFactory.CreateButton(),
                    (AnswerKeyboardButton)_sponsorButtonFactory.CreateButton()
                }
            };
            var answer1 = new Answer { Type = Shared.Enums.Telegram.AnswerType.NewMessage, Reciever = request.Message.Author, Text = _localizer["AnswerText1"], AnswerKeyboardButtons = keyboardButtons };
            _answerSender.SendMessage(answer1);

            var inlineButtons = new List<List<AnswerInlineButton>>
            {
                new List<AnswerInlineButton>
                {
                    (AnswerInlineButton)_playButtonFactory.CreateButton()
                }
            };
            var answer2 = new Answer { Type = Shared.Enums.Telegram.AnswerType.NewMessage, Reciever = request.Message.Author, Text = "Let me help you concentrate on work! Work for next 25 minutes and then I'll message you for rest", AnswerInlineButtons = inlineButtons };
            _answerSender.SendMessage(answer2);
            return Task.FromResult(true);
        }
    }
}
