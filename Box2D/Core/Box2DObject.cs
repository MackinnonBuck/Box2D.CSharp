using System;

namespace Box2D.Core;

public abstract class Box2DObject : IDisposable
{
    internal IntPtr Native { get; private set; }

    internal bool IsDisposed { get; private set; }

    private protected void Initialize(IntPtr native)
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
        Box2DObjectTracker.Add(this);
    }

    public void Dispose()
    {
        if (!IsDisposed)
        {
            Dispose(true);
            HandleDispose();

            GC.SuppressFinalize(this);
        }
    }

    private protected abstract void Dispose(bool disposing);

    private void HandleDispose()
    {
        Native = IntPtr.Zero;
        IsDisposed = true;
        Box2DObjectTracker.Remove(this);
    }

    ~Box2DObject()
    {
        if (!IsDisposed)
        {
            Dispose(false);
            HandleDispose();
        }
    }
}
