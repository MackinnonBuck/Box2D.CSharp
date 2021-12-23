using System;
using System.Runtime.InteropServices;

namespace Box2D;

public abstract class Box2DSubObject : Box2DObject
{
    private protected IntPtr Handle { get; private set; }

    private protected Box2DSubObject() : base(isUserOwned: false)
    {
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Normal));
    }

    protected override void Dispose(bool disposing)
    {
        // TODO: We should find a way to discourage/prevent users from disposing these objects,
        // since they should only be disposed by their owners.
        // Maybe bring Box2DObject back into its own simplified type that doesn't have a Dispose() method?
        // Then have Box2DRootObject that implements IDisposable and Box2DSubObject that does not (at least not publicly)?
        GCHandle.FromIntPtr(Handle).Free();
        Handle = IntPtr.Zero;
    }
}
