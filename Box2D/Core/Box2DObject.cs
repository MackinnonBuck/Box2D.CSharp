using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Box2D;

using static Config.Conditionals;

public abstract class Box2DObject : IDisposable
{
    internal IntPtr Native { get; private set; }

    internal bool IsDisposed { get; private set; }

    internal bool IsUserOwned { get; }

    internal Box2DObject(bool isUserOwned)
    {
        IsUserOwned = isUserOwned;

        if (!IsUserOwned)
        {
            GC.SuppressFinalize(this);
        }
    }

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void ThrowIfDisposed()
    {
        if (IsDisposed)
        {
            throw new ObjectDisposedException(null, $"Cannot access a disposed object of type '{GetType()}'.");
        }
    }

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

    ~Box2DObject()
    {
        if (!IsDisposed)
        {
            Dispose(false);
            HandleDispose();
        }
    }

    protected abstract void Dispose(bool disposing);

    private void HandleDispose()
    {
        Native = IntPtr.Zero;
        IsDisposed = true;
        Box2DObjectTracker.Remove(this);
    }
}
