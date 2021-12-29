using System;
using System.Runtime.InteropServices;

namespace Box2D.Core;

/// <summary>
/// Represents a managed reference to an unmanaged Box2D resource whose
/// lifetime is controlled by another <see cref="Box2DObject"/>. These
/// instances are implicitly destroyed in a manner consistent with the
/// official Box2D documentation:
/// https://github.com/erincatto/box2d/blob/master/docs/loose_ends.md#implicit-destruction
/// </summary>
public abstract class Box2DSubObject : Box2DObject
{
    private protected IntPtr Handle { get; private set; }

    private protected Box2DSubObject()
    {
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Normal));
    }

    internal virtual void Invalidate()
    {
        Uninitialize();

        GCHandle.FromIntPtr(Handle).Free();
        Handle = IntPtr.Zero;
    }
}
