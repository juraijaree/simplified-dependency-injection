namespace DependencyInjection;

internal sealed class ServiceProvider
{
    private readonly ServiceCollection _serviceCollection;

    public ServiceProvider(ServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public T GetService<T>() where T : class
    {
        if (typeof(T).IsInterface || typeof(T).IsAbstract)
        {
            throw new Exception("Cannot resolve interface or abstract class");
        }

        return (T)ResolveService(typeof(T));
    }

    private object ResolveService(Type type) // could be interface or class
    {
        var service = _serviceCollection.GetService(type);
        var serviceType = service.ImplementationType;

        // check if it has dependencies
        var ctorParams = serviceType.GetConstructors().Single().GetParameters(); // TODO: handle multiple ctors
        var ctorParamCount = ctorParams.Length;

        if (ctorParamCount == 0) // parameterless ctor class / base case
        {
            return CreateInstance(service, t => Activator.CreateInstance(t)!);
        }

        // instantiate all dependencies
        var paramImplementations = new object[ctorParamCount];

        for (var i = 0; i < ctorParamCount; i++)
        {
            // recursively instantiates dependencies of dependencies until reaches parameterless dependencies
            paramImplementations[i] = ResolveService(ctorParams[i].ParameterType);
        }

        return CreateInstance(service, t => Activator.CreateInstance(t, paramImplementations)!);
    }

    private object CreateInstance(Service service, Func<Type, object> factory)
    {
        if (service.Lifetime == ServiceLifetime.Singleton && service.IsInstantiated)
        {
            return service.Instance;
        }

        var instance = factory(service.ImplementationType);
        service.SetInstance(instance);

        return instance;
    }
}
