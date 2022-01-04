using Box2D.Core;
using Box2D.Dynamics;
using Box2D.Math;
using System;

namespace Box2D.Collision;

using static Interop.NativeMethods;

/// <summary>
/// Manages contact between two shapes. A contact exists for each overlapping
/// AABB in the broad-phase (except if filtered). Therefore, a contact object
/// may exist that has no contact points.
/// </summary>
public readonly ref struct Contact
{
    private readonly IntPtr _native;

    internal IntPtr Native
    {
        get
        {
            Errors.ThrowIfNull(_native, nameof(Contact));
            return _native;
        }
    }

    /// <summary>
    /// Gets whether this <see cref="Contact"/> points to a null unmanaged contact.
    /// </summary>
    public bool IsNull => _native == IntPtr.Zero;

    /// <summary>
    /// Gets the contact manifold. Do not modify the manifold unless you understand
    /// the internals of Box2D.
    /// </summary>
    public Manifold Manifold => Manifold.Create(b2Contact_GetManifold(Native));

    /// <summary>
    /// Gets whether the contact is touching.
    /// </summary>
    public bool IsTouching => b2Contact_IsTouching(Native);

    /// <summary>
    /// Gets or sets whether the contact is enabled. This can be used inside
    /// the pre-solve contact listener. The contact is only disabled for the
    /// current time step (or sub-step in continuous collisions).
    /// </summary>
    public bool Enabled
    {
        get => b2Contact_IsEnabled(Native);
        set => b2Contact_SetEnabled(Native, value);
    }

    /// <summary>
    /// Gets the next contact in the world's contact list.
    /// </summary>
    public Contact Next => new(b2Contact_GetNext(Native));

    /// <summary>
    /// Gets fixture A in this contact.
    /// </summary>
    public Fixture FixtureA => new(b2Contact_GetFixtureA(Native))!;

    /// <summary>
    /// Gets the child primitive index for fixture A.
    /// </summary>
    public int ChildIndexA => b2Contact_GetChildIndexA(Native);

    /// <summary>
    /// Gets fixture B in this contact.
    /// </summary>
    public Fixture FixtureB => new(b2Contact_GetFixtureB(Native))!;

    /// <summary>
    /// Gets the child primitive index for fixture B.
    /// </summary>
    public int ChildIndexB => b2Contact_GetChildIndexB(Native);

    /// <summary>
    /// Gets or sets the default friction mixture. You can call this in
    /// <see cref="Dynamics.Callbacks.IContactListener.PreSolve(in Contact, in Manifold)"/>.
    /// This value persists until set or reset.
    /// </summary>
    public float Friction
    {
        get => b2Contact_GetFriction(Native);
        set => b2Contact_SetFriction(Native, value);
    }

    /// <summary>
    /// Gets or sets the default restitution mixture. You can call this in
    /// <see cref="Dynamics.Callbacks.IContactListener.PreSolve(in Contact, in Manifold)"/>.
    /// This value persists until set or reset.
    /// </summary>
    public float Restitution
    {
        get => b2Contact_GetRestitution(Native);
        set => b2Contact_SetRestitution(Native, value);
    }

    /// <summary>
    /// Gets or sets the default restitution velocity threshold mixture. You can call this in
    /// <see cref="Dynamics.Callbacks.IContactListener.PreSolve(in Contact, in Manifold)"/>.
    /// This value persists until set or reset.
    /// </summary>
    public float RestitutionThreshold
    {
        get => b2Contact_GetRestitutionThreshold(Native);
        set => b2Contact_SetRestitutionThreshold(Native, value);
    }

    /// <summary>
    /// Gets or sets the desired tangent speed for a conveyor belt behavior.
    /// Units are in meters per second.
    /// </summary>
    public float TangentSpeed
    {
        get => b2Contact_GetTangentSpeed(Native);
        set => b2Contact_SetTangentSpeed(Native, value);
    }

    internal Contact(IntPtr native)
    {
        _native = native;
    }

    /// <summary>
    /// Gets the world manifold.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public void GetWorldManifold(WorldManifold worldManifold)
    {
        if (worldManifold is null)
        {
            throw new ArgumentNullException(nameof(worldManifold));
        }

        b2Contact_GetWorldManifold(Native, worldManifold.Native);
    }

    /// <summary>
    /// Resets the friction mixture to the default value.
    /// </summary>
    public void ResetFriction()
        => b2Contact_ResetFriction(Native);

    /// <summary>
    /// Resets the restitution to the default value.
    /// </summary>
    public void ResetRestitution()
        => b2Contact_ResetRestitution(Native);

    /// <summary>
    /// Resets the restitution threshold to the default value.
    /// </summary>
    public void ResetRestitutionThreshold()
        => b2Contact_ResetRestitutionThreshold(Native);

    /// <summary>
    /// Evaluate this contact with your own manifold and transforms.
    /// </summary>
    public void Evaluate(in Manifold manifold, Transform xfA, Transform xfB)
        => b2Contact_Evaluate(Native, manifold.Native, ref xfA, ref xfB);

    /// <summary>
    /// Returns an <see cref="Enumerator"/> for the current <see cref="Contact"/> instance.
    /// </summary>
    /// <returns></returns>
    public Enumerator GetEnumerator()
        => new(_native);

    /// <summary>
    /// An enumerator for <see cref="Contact"/> instances.
    /// </summary>
    public struct Enumerator
    {
        private IntPtr _current;
        private IntPtr _next;

        /// <summary>
        /// Gets the current element.
        /// </summary>
        public Contact Current => new(_current);

        /// <summary>
        /// Constructs a new <see cref="Enumerator"/> instance.
        /// </summary>
        public Enumerator(IntPtr native)
        {
            _current = IntPtr.Zero;
            _next = native;
        }

        /// <summary>
        /// Moves to the next element.
        /// </summary>
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
