using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Box2D.Core;

public abstract class Box2DSubObject : Box2DObject
{
    private protected IntPtr Handle { get; }

    private protected Box2DSubObject()
    {
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak));

        GC.SuppressFinalize(this);
    }

    private protected override void Dispose(bool disposing)
    {
        Debug.Assert(disposing);

        GCHandle.FromIntPtr(Handle).Free();
    }
}
