namespace Services;

internal sealed class ConfigurationService : IConfigurationService
{
    private readonly string _instanceId = Guid.NewGuid().ToString();
    private readonly ILoggingService _loggingService;

    public ConfigurationService(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }

    public void PrintConfiguration()
    {
        _loggingService.LogMessage($"[{nameof(ConfigurationService)} - {_instanceId}] This configuration is XYZ!");
    }
}
