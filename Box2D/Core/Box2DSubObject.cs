using System;
using System.Runtime.InteropServices;

namespace Box2D;

public abstract class Box2DSubObject : Box2DObject
{
    private protected IntPtr Handle { get; private set; }

    private protected Box2DSubObject()
    {
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Normal));
    }

    internal void FreeHandle()
    {
        OnFreeingHandle();
        Uninitialize();

        GCHandle.FromIntPtr(Handle).Free();
        Handle = IntPtr.Zero;
    }

    private protected virtual void OnFreeingHandle()
    {
    }
}
