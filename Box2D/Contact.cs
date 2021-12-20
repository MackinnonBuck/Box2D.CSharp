using System;

namespace Box2D;

using static Interop.NativeMethods;
using static Core.Errors;

public readonly ref struct Contact
{
    internal IntPtr Native { get; }

    public bool IsValid => Native != IntPtr.Zero;

    public Manifold Manifold
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return new(b2Contact_GetManifold(Native));
        }
    }

    public bool IsTouching
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_IsTouching(Native);
        }
    }

    public bool Enabled
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_IsEnabled(Native);
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Contact_SetEnabled(Native, value);
        }
    }

    internal Contact(IntPtr native)
    {
        Native = native;
    }

    public void GetWorldManifold(WorldManifold worldManifold)
    {
        ThrowIfInvalidAccess(Native);

        if (worldManifold is null)
        {
            throw new ArgumentNullException(nameof(worldManifold));
        }

        worldManifold.ThrowIfDisposed();

        b2Contact_GetWorldManifold(Native, worldManifold.Native);
    }

    public Enumerator GetEnumerator()
        => new(Native);

    public struct Enumerator
    {
        private IntPtr _current;
        private IntPtr _next;

        public Contact Current => new(_current);

        public Enumerator(IntPtr native)
        {
            _current = IntPtr.Zero;
            _next = native;
        }

        public bool MoveNext()
        {
            if (_next == IntPtr.Zero)
            {
                return false;
            }

            _current = _next;
            _next = b2Contact_GetNext(_next);

            return true;
        }
    }
}
