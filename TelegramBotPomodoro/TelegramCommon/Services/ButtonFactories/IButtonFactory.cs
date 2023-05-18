using Shared.Models.Telegram;

namespace TelegramCommon.Services.ButtonFactories
{
    public interface IButtonFactory
    {
        IAnswerButton CreateButton(params object[] args);
    }
}
