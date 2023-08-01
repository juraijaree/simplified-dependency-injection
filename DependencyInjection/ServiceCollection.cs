namespace DependencyInjection;

internal sealed class ServiceCollection
{
    private Dictionary<string, Service> _services = new(); // keyed by class name (implementation)

    public void AddTransient<T>() where T : class
    {
        AddService<T>(ServiceLifetime.Transient);
    }

    public void AddTransient<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class
    {
        AddService<TInterface, TImplementation>(ServiceLifetime.Transient);
    }

    public void AddSingleton<T>() where T : class
    {
        AddService<T>(ServiceLifetime.Singleton);
    }

    public void AddSingleton<TInterface, TImplementation>()
        where TInterface : class
        where TImplementation : class
    {
        AddService<TInterface, TImplementation>(ServiceLifetime.Singleton);
    }

    public void AddService<T>(ServiceLifetime lifetime) where T : class
    {
        if (typeof(T).IsInterface || typeof(T).IsAbstract)
        {
            throw new Exception("Cannot register interface or abstract class");
        }

        var inf = typeof(T).GetInterfaces();

        var service = new Service(typeof(T), lifetime);

        AddService(service);
    }

    public void AddService<TInterface, TImplementation>(ServiceLifetime lifetime)
        where TInterface : class
        where TImplementation : class
    {
        if (typeof(TImplementation).IsInterface || typeof(TImplementation).IsAbstract)
        {
            throw new Exception("Cannot register interface or abstract class");
        }

        var service = new Service(typeof(TImplementation), lifetime)
        {
            InterfaceType = typeof(TInterface)
        };

        AddService(service);
    }

    public Service GetService(Type type) // could be interface or class
    {
        try
        {
            return _services[type.Name];
        }
        catch (KeyNotFoundException)
        {
            throw new Exception($"'{type.Name}' is not registered");
        }
    }

    private void AddService(Service service)
    {
        // keyed by class
        _services.Add(service.ImplementationType.Name, service);

        // keyed by interface
        if (!string.IsNullOrEmpty(service.InterfaceType?.Name))
        {
            _services.Add(service.InterfaceType.Name, service);
        }
    }
}
