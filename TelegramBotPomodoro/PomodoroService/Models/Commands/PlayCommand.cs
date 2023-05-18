using Shared.Models;
using TelegramCommon.Models;

namespace PomodoroService.Models.Commands
{
    [CommandString("/play")]
    internal class PlayCommand : CommandBase
    {
        public PlayCommand(IMessage message) : base(message)
        {
        }
    }
}
