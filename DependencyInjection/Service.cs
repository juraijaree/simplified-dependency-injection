using System.Diagnostics.CodeAnalysis;

namespace DependencyInjection;

internal sealed class Service
{
    private readonly ServiceLifetime _lifetime;
    private object? _instance;
    private bool _isInstantiated = false;

    public Service(Type implementationType, ServiceLifetime lifetime)
    {
        ImplementationType = implementationType;
        _lifetime = lifetime;
    }

    public Type? InterfaceType { get; init; }
    public Type ImplementationType { get; init; }
    public ServiceLifetime Lifetime => _lifetime;
    public object? Instance => _instance;

    [MemberNotNullWhen(returnValue: true, member: nameof(Instance))]
    public bool IsInstantiated => _isInstantiated;

    public void SetInstance(object instance)
    {
        _instance = instance;
        _isInstantiated = true;
    }
}
