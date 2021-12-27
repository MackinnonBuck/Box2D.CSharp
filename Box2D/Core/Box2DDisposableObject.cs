using System;

namespace Box2D.Core;

public abstract class Box2DDisposableObject : Box2DObject, IDisposable
{
    internal bool IsUserOwned { get; }

    internal Box2DDisposableObject(bool isUserOwned)
    {
        IsUserOwned = isUserOwned;

        if (!IsUserOwned)
        {
            GC.SuppressFinalize(this);
        }
    }

    public void Dispose()
    {
        if (IsValid)
        {
            Dispose(true);
            Uninitialize();

            GC.SuppressFinalize(this);
        }
    }

    ~Box2DDisposableObject()
    {
        if (IsValid)
        {
            Dispose(false);
            Uninitialize();
        }
    }

    protected abstract void Dispose(bool disposing);
}
