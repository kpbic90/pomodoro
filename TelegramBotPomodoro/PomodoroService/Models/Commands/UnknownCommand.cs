using Shared.Models;

namespace PomodoroService.Models.Commands
{
    internal class UnknownCommand : CommandBase
    {
        public UnknownCommand(IMessage message) : base(message)
        {
        }
    }
}
