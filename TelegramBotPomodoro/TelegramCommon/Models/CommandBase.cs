using Shared.Models;

namespace TelegramCommon.Models
{
    public abstract class CommandBase : ICommand
    {
        public IMessage Message { get; }

        public CommandBase(IMessage message)
        {
            this.Message = message;
        }
    }
}
