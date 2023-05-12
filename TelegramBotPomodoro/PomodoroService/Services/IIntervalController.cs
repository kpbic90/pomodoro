using PomodoroService.Models;

namespace PomodoroService.Services
{
    internal interface IIntervalController
    {
        void StartInterval(long userId, int messageId, int length, bool isRest);
        void PauseInterval(long userId);
        void ResumeInterval(long userId);
        void SkipInterval(long userId);
        IInterval GetInterval(long userId);
    }
}
