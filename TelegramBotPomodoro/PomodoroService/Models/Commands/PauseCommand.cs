using Shared.Models;
using TelegramCommon.Models;

namespace PomodoroService.Models.Commands
{
    [CommandString("/pause")]
    internal class PauseCommand : CommandBase
    {
        public PauseCommand(IMessage message) : base(message)
        {
        }
    }
}
