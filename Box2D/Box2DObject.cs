using System;

namespace Box2D;

public abstract class Box2DObject
{
    internal IntPtr Native { get; private set; }

    private protected virtual void Initialize(IntPtr native)
    {
        if (Native != IntPtr.Zero)
        {
            throw new InvalidOperationException($"The {nameof(Box2DObject)} was already initialized.");
        }

        if (native == IntPtr.Zero)
        {
            throw new ArgumentException($"Cannot initialize {nameof(Box2DObject)} instances with null.", nameof(native));
        }

        Native = native;
    }

    private protected void Invalidate()
    {
        if (Native == IntPtr.Zero)
        {
            throw new InvalidOperationException($"The {nameof(Box2DObject)} was already invalidated.");
        }

        Native = IntPtr.Zero;
    }
}
