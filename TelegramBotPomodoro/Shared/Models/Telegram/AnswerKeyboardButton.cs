namespace Shared.Models.Telegram
{
    public class AnswerKeyboardButton : IAnswerButton
    {
        public string Text { get; set; }
        public string CallbackData { get; set; }
    }
}
