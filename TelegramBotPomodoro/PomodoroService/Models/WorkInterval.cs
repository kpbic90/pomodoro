using Shared.Models;

namespace PomodoroService.Models
{
    internal class WorkInterval : IInterval, IEntity
    {
        public int Id { get; set; }
        public long? User { get; set; }
        public int MessageId { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public bool IsInProgress { get; set; }
        public bool IsComplete { get; set; }
        public bool IsRest { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public event IInterval.IntervalEventHandler OnIntervalEnd;
        public event IInterval.IntervalEventHandler OnIntervalTimeUpdate;

        private Timer _timer;
        private Timer _timerUpdate;

        public void Start(TimeSpan timeSpan)
        {
            IsComplete = false;
            TimeSpan = timeSpan;
            IsInProgress = true;
            StartTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;

            _timer = new Timer(OnEndInterval, null, TimeSpan, TimeSpan);
            _timerUpdate = new Timer(OnUpdateInterval, null, IInterval.UpdateEventTime, IInterval.UpdateEventTime);
        }

        private void OnEndInterval(object? obj)
        {
            IsComplete = true;
            OnIntervalEnd?.Invoke(this);
        }

        private void OnUpdateInterval(object? obj)
        {
            UpdateTime();
            OnIntervalTimeUpdate?.Invoke(this);
        }

        public void Pause()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timerUpdate.Change(Timeout.Infinite, Timeout.Infinite);
            UpdateTime();
            IsInProgress = false;
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timerUpdate.Change(Timeout.Infinite, Timeout.Infinite);
            IsInProgress = false;
            IsComplete = true;
        }

        private void UpdateTime()
        {
            TimeSpan -= DateTime.Now - LastUpdateTime;
            LastUpdateTime = DateTime.Now;
        }
    }
}
