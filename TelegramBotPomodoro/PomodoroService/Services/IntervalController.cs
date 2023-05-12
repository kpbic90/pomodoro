using MediatR;
using PomodoroService.Models;
using PomodoroService.Models.Notifications;
using Shared.Models;

namespace PomodoroService.Services
{
    internal class IntervalController : IIntervalController
    {
        private readonly IPublisher _publisher;        
        private readonly IRepository<WorkInterval> _repository;

        public IntervalController(IPublisher publisher, IRepository<WorkInterval> repository)
        {
            _publisher = publisher;
            _repository = repository;
        }

        public IInterval GetInterval(long userId) => _repository.Items.FirstOrDefault(s => s.User == userId && !s.IsComplete);

        public void PauseInterval(long userId)
        {
            var interval = _repository.Items.FirstOrDefault(s => s.IsInProgress && s.User == userId && !s.IsComplete);
            if (interval == null)
                return;

            interval.Pause();
        }

        public void ResumeInterval(long userId)
        {
            var interval = _repository.Items.FirstOrDefault(s => !s.IsInProgress && s.User == userId && !s.IsComplete);
            if (interval == null)
                return;

            interval.Start(interval.TimeSpan);
        }

        public void SkipInterval(long userId)
        {
            var interval = _repository.Items.FirstOrDefault(s => s.IsInProgress && s.User == userId && !s.IsComplete);
            if (interval == null)
                return;

            interval.Stop();
        }

        public void StartInterval(long userId, int messageId, int length, bool isRest)
        {
            var interval = new WorkInterval { MessageId = messageId, TimeSpan = new TimeSpan(0, length, 0), User = userId, IsRest = isRest };
            _repository.Add(interval);
            interval.OnIntervalTimeUpdate += Interval_OnIntervalTimeUpdate;
            interval.OnIntervalEnd += Interval_OnIntervalEnd;
            interval.Start(interval.TimeSpan);
        }

        private void Interval_OnIntervalEnd(object sender)
        {
            var interval = (IInterval)sender;
            interval.OnIntervalTimeUpdate -= Interval_OnIntervalTimeUpdate;
            interval.OnIntervalEnd -= Interval_OnIntervalEnd;

            var notification = new IntervalEndNotification { MessageId = interval.MessageId, User = interval.User, IsRest = interval.IsRest };
            _publisher.Publish(notification);
        }

        private void Interval_OnIntervalTimeUpdate(object sender)
        {
            var interval = (IInterval)sender;
            var notification = new IntervalUpdateNotification { MessageId = interval.MessageId, TimeLeft = interval.TimeSpan, User = interval.User, IsRest = interval.IsRest };
            _publisher.Publish(notification);
        }
    }
}
