using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Box2D.Core;

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
