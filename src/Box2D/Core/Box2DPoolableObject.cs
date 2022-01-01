using Microsoft.Extensions.ObjectPool;
using System;

namespace Box2D.Core;

public abstract partial class Box2DPoolableObject<T, TFactory> : Box2DDisposableObject, IDisposable
    where T : notnull, Box2DPoolableObject<T, TFactory>
    where TFactory : IBox2DPoolableObjectFactory<T>, new()
{
    private static readonly TFactory _factory = new();

    internal Box2DPoolableObject(bool isUserOwned) : base(isUserOwned)
    {
    }

    private protected abstract void Reset();
}

#if BOX2D_OBJECT_POOLING
partial class Box2DPoolableObject<T, TFactory>
{
    private class PooledObjectPolicy : PooledObjectPolicy<T>
    {
        public override T Create()
            => _factory.Create();

        public override bool Return(T obj)
        {
            obj.Reset();
            return true;
        }
    }

    private static ObjectPool<T> _objectPool = default!;

    private bool _isDisposing;

    public static T Create()
    {
        if (_objectPool is null)
        {
            var provider = new DefaultObjectPoolProvider();
            var policy = new PooledObjectPolicy();
            _objectPool = provider.Create(policy);
        }

        return _objectPool.Get();
    }

    public override void Dispose()
    {
        if (_isDisposing)
        {
            DisposeCore();
        }
        else
        {
            _isDisposing = true;
            _objectPool.Return((T)this);
            _isDisposing = false;
        }
    }
}
#else
partial class Box2DPoolableObject<T, TFactory>
{
    public static void Create()
        => _factory.Create();
}
#endif
