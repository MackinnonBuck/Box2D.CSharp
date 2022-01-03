using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Box2D.Core;

internal readonly partial struct ManagedHandle
{
}

#if BOX2D_VALID_ACCESS_CHECKING
partial struct ManagedHandle
{
    public IntPtr Ptr { get; private init; }

    public NativeHandleValidationToken ValidationToken { get; private init; }

    public static ManagedHandle Create(object? userData)
    {
        var token = new NativeHandleValidationToken(userData);
        var ptr = GCHandle.ToIntPtr(GCHandle.Alloc(token));

        return new()
        {
            ValidationToken = token,
            Ptr = ptr,
        };
    }
}
#else
partial struct ManagedHandle
{
    public IntPtr Ptr { get; private init; }

    public static ManagedHandle Create(object? userData)
    {
        var ptr = userData is null ? IntPtr.Zero : GCHandle.ToIntPtr(GCHandle.Alloc(userData));

        return new()
        {
            Ptr = ptr,
        };
    }
}
#endif
