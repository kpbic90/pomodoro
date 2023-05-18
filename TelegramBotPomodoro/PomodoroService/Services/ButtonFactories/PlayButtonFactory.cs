using Shared.Models.Telegram;
using TelegramCommon.Services.ButtonFactories;

namespace PomodoroService.Services.ButtonFactories
{
    internal class PlayButtonFactory : IButtonFactory
    {
        public IAnswerButton CreateButton(params object[] args)
        {
            return new AnswerInlineButton
            {
                Text = "Start Working!",
                CallbackData = "/play"
            };
        }
    }
}
