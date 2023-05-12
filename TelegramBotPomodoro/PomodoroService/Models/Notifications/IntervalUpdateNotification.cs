using MediatR;

namespace PomodoroService.Models.Notifications
{
    internal class IntervalUpdateNotification : INotification
    {
        public long? User { get; set; }
        public int MessageId { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public bool IsRest { get; set; }
    }
}
