using System;

namespace Box2D;

using static NativeMethods;
using static Errors;

public readonly ref struct Contact
{
    internal IntPtr Native { get; }

    public bool IsValid => Native != IntPtr.Zero;

    public Manifold Manifold
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return Manifold.Create(b2Contact_GetManifold(Native));
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

    public Contact Next
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return new(b2Contact_GetNext(Native));
        }
    }

    public Fixture FixtureA
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return Fixture.FromIntPtr.Get(b2Contact_GetFixtureA(Native))!;
        }
    }

    public int ChildIndexA
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_GetChildIndexA(Native);
        }
    }

    public Fixture FixtureB
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return Fixture.FromIntPtr.Get(b2Contact_GetFixtureB(Native))!;
        }
    }

    public int ChildIndexB
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_GetChildIndexB(Native);
        }
    }

    public float Friction
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_GetFriction(Native);
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Contact_SetFriction(Native, value);
        }
    }

    public float Restitution
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_GetRestitution(Native);
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Contact_SetRestitution(Native, value);
        }
    }

    public float RestitutionThreshold
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_GetRestitutionThreshold(Native);
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Contact_SetRestitutionThreshold(Native, value);
        }
    }

    public float TangentSpeed
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Contact_GetTangentSpeed(Native);
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Contact_SetTangentSpeed(Native, value);
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

    public void ResetFriction()
    {
        ThrowIfInvalidAccess(Native);
        b2Contact_ResetFriction(Native);
    }

    public void ResetRestitution()
    {
        ThrowIfInvalidAccess(Native);
        b2Contact_ResetRestitution(Native);
    }

    public void ResetRestitutionThreshold()
    {
        ThrowIfInvalidAccess(Native);
        b2Contact_ResetRestitutionThreshold(Native);
    }

    public void Evaluate(in Manifold manifold, Transform xfA, Transform xfB)
    {
        ThrowIfInvalidAccess(Native);
        b2Contact_Evaluate(Native, manifold.Native, ref xfA, ref xfB);
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
