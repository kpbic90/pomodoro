using MediatR;
using Shared.Models;

namespace PomodoroService.Models.Commands
{
    internal interface ICommand : INotification
    {
        IMessage Message { get; }
    }
}
