namespace Shared.Models
{
    public class User
    {
        public Guid Oid { get; set; }
        string Name { get; set; }
        public long? TelegramId { get; set; }
    }
}
