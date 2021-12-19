using System;

namespace Box2D;

// TODO: Should this be a ref struct so we don't persist the information longer than we should?
public readonly struct Contact
{
    private readonly IntPtr _native;

    internal Contact(IntPtr native)
    {
        _native = native;
    }

    // TODO: Implement.
}
