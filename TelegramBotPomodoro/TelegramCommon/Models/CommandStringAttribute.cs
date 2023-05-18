namespace TelegramCommon.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandStringAttribute : Attribute
    {
        private string Name;

        public CommandStringAttribute(string name)
        {
            Name = name;
        }

        public string GetName() => Name;
    }
}
