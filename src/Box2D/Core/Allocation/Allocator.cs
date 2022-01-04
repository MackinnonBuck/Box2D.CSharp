using System;

namespace Box2D.Core.Allocation;

internal static class Allocator
{
    public static IAllocator<T> Create<T>(Func<T> factory) where T : class, IBox2DRecyclableObject
    {
#if BOX2D_NO_POOLING
        return new SimpleAllocator<T>(factory);
#else
        return new PooledAllocator<T>(factory);
#endif
    }
}
