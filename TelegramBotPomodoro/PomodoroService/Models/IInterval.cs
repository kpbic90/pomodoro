namespace PomodoroService.Models
{
    internal interface IInterval
    {
        public delegate void IntervalEventHandler(object sender);
        public event IntervalEventHandler OnIntervalEnd;
        public event IntervalEventHandler OnIntervalTimeUpdate;

        long? User { get; set; }
        int MessageId { get; set; }
        TimeSpan TimeSpan { get; set; }
        bool IsInProgress { get; set; }
        bool IsComplete { get; set; }
        bool IsRest { get; set; }

        void Start(TimeSpan timeSpan);
        void Pause();
        void Stop();

        public static TimeSpan UpdateEventTime = new TimeSpan(0,0,0,5,0);
    }
}
