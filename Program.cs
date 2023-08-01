using DependencyInjection;
using Services;

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<ILoggingService, LoggingService>();
serviceCollection.AddTransient<IEmailService, EmailService>();
serviceCollection.AddSingleton<IConfigurationService, ConfigurationService>();
serviceCollection.AddTransient<CoreService>();

var serviceProvider = new ServiceProvider(serviceCollection);

for (var i = 0; i < 3; i++)
{
    var coreService = serviceProvider.GetService<CoreService>();
    coreService.Run();
    Console.WriteLine(" ");
}

Console.ReadLine();

/*  Dependency tree

        core
        /   \
   email    config
    |           |
  logging    logging

*/
