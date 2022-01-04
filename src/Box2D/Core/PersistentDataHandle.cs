using System;
using System.Runtime.InteropServices;

namespace Box2D.Core;

// This type represents a pointer to a managed object that
// persists with the lifetime of its corresponding native resource.
// When access validity checking is enabled, the underlying pointer
// represents a PersistentData object that contains both the managed
// user data and a flag indicating whether the instance is valid.
// When access validity checking is disabled, the underlying pointer
// represents the managed user data, or null if no user data was provided.
internal readonly partial struct PersistentDataHandle
{
}

#if BOX2D_VALID_ACCESS_CHECKING
partial struct PersistentDataHandle
{
    public IntPtr Ptr { get; private init; }

    public PersistentData Data { get; private init; }

    public static PersistentDataHandle Create(object? userData)
    {
        var data = new PersistentData(userData);
        var ptr = GCHandle.ToIntPtr(GCHandle.Alloc(data));

        return new()
        {
            Data = data,
            Ptr = ptr,
        };
    }
}
#else
partial struct PersistentDataHandle
{
    public IntPtr Ptr { get; private init; }

    public static PersistentDataHandle Create(object? userData)
    {
        var ptr = userData is null ? IntPtr.Zero : GCHandle.ToIntPtr(GCHandle.Alloc(userData));

        return new()
        {
            Ptr = ptr,
        };
    }
}
#endif
