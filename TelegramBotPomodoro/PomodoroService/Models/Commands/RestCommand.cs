using Shared.Models;

namespace PomodoroService.Models.Commands
{
    [CommandString("/rest")]
    internal class RestCommand : CommandBase
    {
        public RestCommand(IMessage message) : base(message)
        {
        }
    }
}
