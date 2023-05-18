using MediatR;
using System.Globalization;
using TelegramCommon.Models;

namespace PomodoroService.Services.Handlers
{
    public class SetLanguageCommandHandler : IRequestHandler<SetLanguageCommand, bool>
    {
        public Task<bool> Handle(SetLanguageCommand request, CancellationToken cancellationToken)
        {
            // TODO
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(request.Message.Text.Split(' ').Last());
            return Task.FromResult(true);
        }
    }
}
