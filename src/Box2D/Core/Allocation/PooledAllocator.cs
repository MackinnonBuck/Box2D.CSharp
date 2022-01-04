using Microsoft.Extensions.ObjectPool;
using System;

namespace Box2D.Core.Allocation;

internal class StaticPoolingResources
{
    public static readonly ObjectPoolProvider PoolProvider = new DefaultObjectPoolProvider();
}

internal class PooledAllocator<T> : IAllocator<T> where T : class, IBox2DRecyclableObject
{
    private class PooledObjectPolicy : PooledObjectPolicy<T>
    {
        private readonly Func<T> _factory;

        public PooledObjectPolicy(Func<T> factory)
        {
            _factory = factory;
        }

        public override T Create()
            => _factory();

        public override bool Return(T obj)
        {
            obj.Reset();
            return true;
        }
    }

    private readonly ObjectPool<T> _pool;

    public PooledAllocator(Func<T> factory)
    {
        var policy = new PooledObjectPolicy(factory);
        _pool = StaticPoolingResources.PoolProvider.Create(policy);
    }

    public T Allocate()
        => _pool.Get();

    public bool TryRecycle(T obj)
    {
        _pool.Return(obj);
        return true;
    }
}
