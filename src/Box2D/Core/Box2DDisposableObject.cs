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
    private bool _isTryingToRecycle;

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
    public void Dispose()
    {
        if (_isTryingToRecycle)
        {
            // We're getting disposed while attempting to be recycled.
            // This probably means that the recycling mechanism decided not
            // to recycle this instance, so we'll just perform a normal dispose.
            DisposeCore();
            return;
        }

        if (this is not IBox2DRecyclableObject recyclable)
        {
            // This type cannot be recycled, so we'll dispose now.
            DisposeCore();
            return;
        }

        // At this point, we should attempt to recycle this instance.

        _isTryingToRecycle = true;

        if (!recyclable.TryRecycle())
        {
            // We did not attempt to recycle, so we'll dispose immediately.
            DisposeCore();
        }

        _isTryingToRecycle = false;
    }

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

            Box2DObjectTracker.IncrementFinalizerCallCount();
        }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting
    /// unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether the method call comes from a <see cref="IDisposable.Dispose"/>
    /// method (its value is <see langword="true"/>) or from a finalizer (its value is <see langword="false"/>).
    /// </param>
    protected abstract void Dispose(bool disposing);
}
