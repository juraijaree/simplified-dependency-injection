namespace Services;

internal sealed class EmailService : IEmailService
{
    private readonly string _instanceId = Guid.NewGuid().ToString();
    private readonly ILoggingService _loggingService;

    public EmailService(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }

    public void Send(string message)
    {
        _loggingService.LogMessage($"[{nameof(EmailService)} - {_instanceId}] - Successfully sent email with message = {message}");
    }
}
