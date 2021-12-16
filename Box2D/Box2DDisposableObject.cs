using System;

namespace Box2D;

public abstract class Box2DDisposableObject : Box2DObject, IDisposable
{
    internal bool IsUserOwned { get; }

    internal bool IsDisposed { get; private set; }

    private protected Box2DDisposableObject(bool isUserOwned)
    {
        IsDisposed = false;
        IsUserOwned = isUserOwned;
    }

    private protected override void Initialize(IntPtr native)
    {
        base.Initialize(native);

        if (!IsUserOwned)
        {
            GC.SuppressFinalize(this);
        }
    }

    public void Dispose()
    {
        if (!IsDisposed)
        {
            Dispose(true);

            Invalidate();

            IsDisposed = true;

            GC.SuppressFinalize(this);
        }
    }

    private protected abstract void Dispose(bool disposing);

    ~Box2DDisposableObject()
    {
        if (!IsDisposed)
        {
            Dispose(false);

            Invalidate();

            IsDisposed = true;
        }
    }
}
