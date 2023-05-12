using Shared.Models;

namespace PomodoroService.Models.Commands
{
    [CommandString("/resume")]
    internal class ResumeCommand : CommandBase
    {
        public ResumeCommand(IMessage message) : base(message)
        {
        }
    }
}
