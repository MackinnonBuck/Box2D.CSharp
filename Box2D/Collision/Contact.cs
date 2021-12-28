using Box2D.Core;
using Box2D.Dynamics;
using Box2D.Math;
using System;

namespace Box2D.Collision;

using static Interop.NativeMethods;

public readonly ref struct Contact
{
    private readonly IntPtr _native;

    internal IntPtr Native
    {
        get
        {
            Errors.ThrowIfInvalidAccess(_native);
            return _native;
        }
    }

    public bool IsValid => _native != IntPtr.Zero;

    public Manifold Manifold => Manifold.Create(b2Contact_GetManifold(Native));

    public bool IsTouching => b2Contact_IsTouching(Native);

    public bool Enabled
    {
        get => b2Contact_IsEnabled(Native);
        set => b2Contact_SetEnabled(Native, value);
    }

    public Contact Next => new(b2Contact_GetNext(Native));

    public Fixture FixtureA => Fixture.FromIntPtr.Get(b2Contact_GetFixtureA(Native))!;

    public int ChildIndexA => b2Contact_GetChildIndexA(Native);

    public Fixture FixtureB => Fixture.FromIntPtr.Get(b2Contact_GetFixtureB(Native))!;

    public int ChildIndexB => b2Contact_GetChildIndexB(Native);

    public float Friction
    {
        get => b2Contact_GetFriction(Native);
        set => b2Contact_SetFriction(Native, value);
    }

    public float Restitution
    {
        get => b2Contact_GetRestitution(Native);
        set => b2Contact_SetRestitution(Native, value);
    }

    public float RestitutionThreshold
    {
        get => b2Contact_GetRestitutionThreshold(Native);
        set => b2Contact_SetRestitutionThreshold(Native, value);
    }

    public float TangentSpeed
    {
        get => b2Contact_GetTangentSpeed(Native);
        set => b2Contact_SetTangentSpeed(Native, value);
    }

    internal Contact(IntPtr native)
    {
        _native = native;
    }

    public void GetWorldManifold(WorldManifold worldManifold)
    {
        if (worldManifold is null)
        {
            throw new ArgumentNullException(nameof(worldManifold));
        }

        b2Contact_GetWorldManifold(Native, worldManifold.Native);
    }

    public void ResetFriction()
        => b2Contact_ResetFriction(Native);

    public void ResetRestitution()
        => b2Contact_ResetRestitution(Native);

    public void ResetRestitutionThreshold()
        => b2Contact_ResetRestitutionThreshold(Native);

    public void Evaluate(in Manifold manifold, Transform xfA, Transform xfB)
        => b2Contact_Evaluate(Native, manifold.Native, ref xfA, ref xfB);

    public Enumerator GetEnumerator()
        => new(_native);

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
