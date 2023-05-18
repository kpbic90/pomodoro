using Shared.Models.Telegram;
using TelegramCommon.Services.ButtonFactories;

namespace PomodoroService.Services.ButtonFactories
{
    internal class PauseButtonFactory : IButtonFactory
    {
        public IAnswerButton CreateButton(params object[] args)
        {
            return new AnswerInlineButton
            {
                Text = string.Format("Pause [{0:mm\\:ss}]", args),
                CallbackData = "/pause"
            };
        }
    }
}
