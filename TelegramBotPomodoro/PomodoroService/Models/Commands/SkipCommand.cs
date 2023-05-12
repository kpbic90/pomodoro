using Shared.Models;

namespace PomodoroService.Models.Commands
{
    [CommandString("/skip")]
    internal class SkipCommand : CommandBase
    {
        public SkipCommand(IMessage message) : base(message)
        {
        }
    }
}
