using System;

namespace Box2D.Core.Allocation;

internal class SimpleAllocator<T> : IAllocator<T> where T : class, IBox2DRecyclableObject
{
    private readonly Func<T> _factory;

    public SimpleAllocator(Func<T> factory)
    {
        _factory = factory;
    }

    public T Allocate()
        => _factory();

    public bool TryRecycle(T obj)
        => false;
}
