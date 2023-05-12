using MediatR;

namespace PomodoroService.Models.Notifications
{
    internal class IntervalEndNotification : INotification
    {
        public long? User { get; set; }
        public int MessageId { get; set; }
        public bool IsRest { get; set; }
    }
}
