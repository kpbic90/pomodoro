using Microsoft.Extensions.Localization;
using Shared.Models.Telegram;

namespace TelegramCommon.Services.ButtonFactories
{
    public class SettingsButtonFactory : IButtonFactory
    {
        private readonly IStringLocalizer<SettingsButtonFactory> _localizer = null!;

        public SettingsButtonFactory(IStringLocalizer<SettingsButtonFactory> localizer)
        {
            _localizer = localizer;
        }

        public IAnswerButton CreateButton(params object[] args)
        {
            return new AnswerKeyboardButton
            {
                Text = _localizer["ButtonText"],
                CallbackData = "https://kpbic90.github.io/pomodoro/WebApp/settings.html"
            };
        }
    }
}
