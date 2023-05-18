using Shared.Models.Telegram;
using TelegramCommon.Services.ButtonFactories;

namespace PomodoroService.Services.ButtonFactories
{
    internal class RestButtonFactory : IButtonFactory
    {
        public IAnswerButton CreateButton(params object[] args)
        {
            return new AnswerInlineButton
            {
                Text = "Rest",
                CallbackData = "/rest"
            };
        }
    }
}
