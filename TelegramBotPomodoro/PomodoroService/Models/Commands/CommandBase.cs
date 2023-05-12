using Shared.Models;

namespace PomodoroService.Models.Commands
{
    internal abstract class CommandBase : ICommand
    {
        public IMessage Message { get; }

        public CommandBase(IMessage message) 
        { 
            this.Message = message;
        }
    }
}
