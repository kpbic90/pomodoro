namespace Shared.Models.Telegram
{
    public class AnswerInlineButton : IAnswerButton
    {
        public string Text { get; set; }
        public string CallbackData { get; set; }
    }
}
