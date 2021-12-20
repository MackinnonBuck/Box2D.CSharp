using System;
using System.Runtime.InteropServices;

namespace Box2D.Core;

public abstract class Box2DSubObject : Box2DObject
{
    private protected IntPtr Handle { get; private set; }

    private protected Box2DSubObject() : base(isUserOwned: false)
    {
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak));
    }

    private protected override void Dispose(bool disposing)
    {
        GCHandle.FromIntPtr(Handle).Free();
        Handle = IntPtr.Zero;
    }
}
