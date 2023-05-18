using MediatR;
using Shared.Models;

namespace TelegramCommon.Models
{
    public interface ICommand : IRequest<bool>
    {
        IMessage Message { get; }
    }
}
