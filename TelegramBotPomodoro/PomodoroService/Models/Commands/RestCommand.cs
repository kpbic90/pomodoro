using Shared.Models;
using TelegramCommon.Models;

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
