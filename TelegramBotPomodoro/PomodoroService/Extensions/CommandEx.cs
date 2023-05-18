using PomodoroService.Models.Commands;
using Shared.Models;
using TelegramCommon.Models;

namespace PomodoroService.Extensions
{
    internal static class CommandEx
    {
        internal static string GetCommandString(this IMessage message)
        {
            if(message == null)
                return string.Empty;

            if(!message.Text.StartsWith('/'))
                return string.Empty;

            return message.Text.Split(' ').FirstOrDefault();
        }

        internal static ICommand? GetCommand(this string commandString, IMessage message)
        {
            if (string.IsNullOrEmpty(commandString))
                return new UnknownCommand(message);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(mytype => mytype.GetInterfaces().Contains(typeof(ICommand)));

            foreach (var type in types)
            {
                var commandNameAttr = Attribute.GetCustomAttribute(type, typeof(CommandStringAttribute));
                if (commandNameAttr == null)
                    continue;

                if (((CommandStringAttribute)commandNameAttr).GetName() == commandString)
                    return (ICommand?)Activator.CreateInstance(type, message);
            }

            return new UnknownCommand(message);
        }
    }
}
