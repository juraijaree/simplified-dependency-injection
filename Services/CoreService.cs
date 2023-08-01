namespace Services;

internal sealed class CoreService
{
    private readonly IEmailService _emailService;
    private readonly IConfigurationService _configurationService;

    public CoreService(IEmailService emailService, IConfigurationService configurationService)
    {
        _emailService = emailService;
        _configurationService = configurationService;
    }

    public void Run()
    {
        _emailService.Send("hello");
        _configurationService.PrintConfiguration();
    }
}
