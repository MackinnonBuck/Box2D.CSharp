using System;

namespace Box2D.Core;

/// <summary>
/// Represents a managed reference to an unmanaged Box2D resource.
/// </summary>
public abstract class Box2DObject
{
    private IntPtr _native;

    internal IntPtr Native
    {
        get
        {
            Errors.ThrowIfNull(_native, this);
            return _native;
        }
    }

    internal bool IsValid => _native != IntPtr.Zero;

    private protected void Initialize(IntPtr native)
    {
        if (_native != IntPtr.Zero)
        {
            throw new InvalidOperationException($"Cannot initialize an already-initialized {GetType()}.");
        }

        if (native == IntPtr.Zero)
        {
            throw new ArgumentException($"Cannot initialize {nameof(Box2DObject)} instances with null.", nameof(native));
        }

        _native = native;
        Box2DObjectTracker.Add(this);
    }

    private protected void Uninitialize()
    {
        if (Native == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Cannot uninitialize an already-uinitialized {GetType()}.");
        }

        Box2DObjectTracker.Remove(this);
        _native = IntPtr.Zero;
    }
}
