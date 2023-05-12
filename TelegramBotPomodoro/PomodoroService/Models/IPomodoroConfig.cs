namespace PomodoroService.Models
{
    internal interface IPomodoroConfig
    {
        int DefaultIntervalLength { get; }
        int DefaultRestLength { get; }
    }
}
