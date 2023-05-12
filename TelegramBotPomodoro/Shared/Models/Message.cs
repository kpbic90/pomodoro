using Shared.Models.Telegram;

namespace Shared.Models
{
    public class Message : IMessage
    {
        public int? Id { get; set; }
        public long Author { get; set; }
        public long Receiver { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Author};{Receiver};{Text}";
        }
    }
}
