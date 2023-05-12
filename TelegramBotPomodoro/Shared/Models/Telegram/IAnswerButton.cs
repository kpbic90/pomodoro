namespace Shared.Models.Telegram
{
    public interface IAnswerButton
    {
        public string Text { get; set; }
        public string CallbackData { get; set; }
    }
}
