using System;

namespace Box2D.Core.Allocation;

internal static class Allocator
{
    public static IAllocator<T> Create<T>(Func<T> factory) where T : class, IBox2DRecyclableObject
    {
#if BOX2D_OBJECT_POOLING
        return new PooledAllocator<T>(factory);
#else
        return new SimpleAllocator<T>(factory);
#endif
    }
}
