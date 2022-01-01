using System;

namespace Box2D.Core;

/// <summary>
/// Represents a managed reference to an unmanaged Box2D resource that
/// may or may not be owned by the "user" (managed code). The creator
/// of instances deriving <see cref="Box2DDisposableObject"/> should call
/// <see cref="Dispose"/> when the object is no longer in use.
/// </summary>
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

    /// <inheritdoc/>
    public virtual void Dispose()
        => DisposeCore();

    private protected void DisposeCore()
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

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting
    /// unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method call comes from a <see cref="IDisposable.Dispose"/>
    /// method (its value is <c>true</c>) or from a finalizer (its value is <c>false</c>).
    /// </param>
    protected abstract void Dispose(bool disposing);
}
