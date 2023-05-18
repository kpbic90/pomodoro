using Microsoft.Extensions.Localization;
using Shared.Models.Telegram;

namespace TelegramCommon.Services.ButtonFactories
{
    public class SponsorButtonFactory : IButtonFactory
    {
        private readonly IStringLocalizer<SponsorButtonFactory> _localizer = null!;

        public SponsorButtonFactory(IStringLocalizer<SponsorButtonFactory> localizer)
        {
            _localizer = localizer;
        }

        public IAnswerButton CreateButton(params object[] args)
        {
            return new AnswerKeyboardButton
            {
                Text = _localizer["ButtonText"],
                CallbackData = "https://kpbic90.github.io/pomodoro/WebApp/links.html"
            };
        }
    }
}
