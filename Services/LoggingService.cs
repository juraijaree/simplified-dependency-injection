namespace Services;

internal sealed class LoggingService : ILoggingService
{
    private readonly string _instanceId = Guid.NewGuid().ToString();

    public void LogMessage(string message)
    {
        Console.WriteLine($"{message}\n--- by {nameof(LoggingService)} - {_instanceId}\n\n");
    }
}
