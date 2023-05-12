namespace PomodoroService.Models.Commands
{
    [System.AttributeUsage(AttributeTargets.Class)]
    internal class CommandStringAttribute : Attribute
    {
        private string Name;

        internal CommandStringAttribute(string name)
        {
            Name = name;
        }

        public string GetName() => Name;
    }
}
