using Shared.Models;

namespace TelegramCommon.Models
{
    [CommandString("/set_lang")]
    public class SetLanguageCommand : CommandBase
    {
        public SetLanguageCommand(IMessage message) : base(message)
        {
        }
    }
}
