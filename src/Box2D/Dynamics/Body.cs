using Box2D.Collision;
using Box2D.Core;
using Box2D.Math;
using System;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// A rigid body. These are created via <see cref="World.CreateBody"/>.
/// </summary>
public readonly struct Body : IEquatable<Body>
{
    internal readonly struct Reviver : IPersistentDataReviver
    {
        public string RevivedObjectName => nameof(Body);

        public IntPtr GetPersistentDataPointer(IntPtr native)
            => b2Body_GetUserData(native);
    }

    internal readonly struct Destroyer : INativeResourceDestroyer
    {
        private readonly IntPtr _worldNative;

        public Destroyer(IntPtr worldNative)
        {
            _worldNative = worldNative;
        }

        public void Destroy(IntPtr native)
        {
            b2World_DestroyBody(_worldNative, native);
        }
    }

    private readonly NativeHandle<Reviver> _nativeHandle;

    internal IntPtr Native => _nativeHandle.Ptr;

    /// <summary>
    /// Gets whether this body instance is null.
    /// </summary>
    /// <remarks>
    /// This will not necessarily return <see langword="true"/> if the body has been implicitly destroyed.
    /// You can manually nullify <see cref="Body"/> instances by assigning them to <see langword="default"/>.
    /// </remarks>
    public bool IsNull => _nativeHandle.IsNull;

    /// <summary>
    /// Gets the user data object.
    /// </summary>
    public object? UserData => _nativeHandle.GetUserData();

    /// <summary>
    /// Gets or sets the world body origin position.
    /// Manipulating a body's transform may cause non-physical behavior.
    /// </summary>
    public Vector2 Position
    {
        get
        {
            b2Body_GetPosition(Native, out var value);
            return value;
        }
        set => b2Body_SetPosition(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the linear velocity of the center of mass.
    /// </summary>
    public Vector2 LinearVelocity
    {
        get
        {
            b2Body_GetLinearVelocity(Native, out var value);
            return value;
        }
        set => b2Body_SetLinearVelocity(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the angular velocity.
    /// </summary>
    public float AngularVelocity
    {
        get => b2Body_GetAngularVelocity(Native);
        set => b2Body_SetAngularVelocity(Native, value);
    }

    /// <summary>
    /// Gets or sets the angle in radians.
    /// Manipulating a body's transform may cause non-physical behavior.
    /// </summary>
    public float Angle
    {
        get => b2Body_GetAngle(Native);
        set => b2Body_SetAngle(Native, value);
    }

    /// <summary>
    /// Gets the world position of the center of mass.
    /// </summary>
    public Vector2 WorldCenter
    {
        get
        {
            b2Body_GetWorldCenter(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the local position of the center of mass.
    /// </summary>
    public Vector2 LocalCenter
    {
        get
        {
            b2Body_GetLocalCenter(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets or sets the gravity scale of the body.
    /// </summary>
    public float GravityScale
    {
        get => b2Body_GetGravityScale(Native);
        set => b2Body_SetGravityScale(Native, value);
    }

    /// <summary>
    /// Gets the total mass of the body.
    /// </summary>
    public float Mass => b2Body_GetMass(Native);

    /// <summary>
    /// Gets the rotational inertia of the body about the local origin.
    /// </summary>
    public float Inertia => b2Body_GetInertia(Native);

    /// <summary>
    /// Gets or sets the type of this body.
    /// </summary>
    public BodyType Type
    {
        get => b2Body_GetType(Native);
        set => b2Body_SetType(Native, value);
    }

    /// <summary>
    /// Gets or sets the sleep state of the body. A sleeping body has very
    /// low CPU cost.
    /// </summary>
    public bool Awake
    {
        get => b2Body_IsAwake(Native);
        set => b2Body_SetAwake(Native, value);
    }

    /// <summary>
    /// Gets the list of all fixtures attached to this body.
    /// </summary>
    public Fixture FixtureList => new(b2Body_GetFixtureList(Native));

    /// <summary>
    /// Gets the list of all joints attached to this body.
    /// </summary>
    public JointEdge JointList => new(b2Body_GetJointList(Native));

    /// <summary>
    /// Gets the next bodyin the world's body list.
    /// </summary>
    public Body Next => new(b2Body_GetNext(Native));

    internal Body(IntPtr world, BodyDef def)
    {
        var persistentDataHandle = PersistentDataHandle.Create(def.UserData);
        var native = b2World_CreateBody(world, def.Native, persistentDataHandle.Ptr);
        _nativeHandle = new(native, persistentDataHandle);
    }

    internal Body(IntPtr world, BodyType type, ref Vector2 position, float angle, object? userData)
    {
        var persistentDataHandle = PersistentDataHandle.Create(userData);
        var native = b2World_CreateBody2(world, type, ref position, angle, persistentDataHandle.Ptr);
        _nativeHandle = new(native, persistentDataHandle);
    }

    internal Body(IntPtr native)
    {
        _nativeHandle = new(native);
    }

    internal void Invalidate()
        => _nativeHandle.Invalidate();

    internal void Destroy(IntPtr worldNative)
        => _nativeHandle.Destroy(new Destroyer(worldNative));

    /// <summary>
    /// Creates a fixture and attaches it to this body. Use this function if you need
    /// to set some fixture parameters, like friction. Otherwise you can create the
    /// fixture directly from a shape using <see cref="CreateFixture(Shape, float)"/>.
    /// If the density is non-zero, this function automatically updates the mass of the body.
    /// Contacts are not created until the next time step.
    /// </summary>
    /// <param name="def">The fixture definition.</param>
    public Fixture CreateFixture(in FixtureDef def)
        => new(this, in def);

    /// <summary>
    /// Creates a fixture from a shape and attaches it to this body.
    /// This is a convenience function. Use <see cref="CreateFixture(in FixtureDef)"/> if you need
    /// to set parameters like friction, restitution, user data, or filtering.
    /// If the density is non-zero, this function automatically updates the mass of the body.
    /// </summary>
    /// <param name="shape">The shape to be cloned.</param>
    /// <param name="density">The shape density (set to zero for static bodies).</param>
    public Fixture CreateFixture(Shape shape, float density)
        => new(this, shape, density);

    /// <summary>
    /// Destroy a fixture. This removes the fixture from the broad-phase and
    /// destroys all contacts associated with this fixture. This will
    /// automatically adjust the mass of the body if the body is dynamic and the
    /// fixture has positive density.
    /// All fixtures attached to a body are implicitly destroyed when the body is destroyed.
    /// </summary>
    /// <param name="fixture">The fixture to be removed.</param>
    public void DestroyFixture(Fixture fixture)
        => fixture.Destroy(Native);

    /// <summary>
    /// Get the body transform for the body's origin.
    /// </summary>
    /// <param name="transform">The world transform of the body's origin.</param>
    public void GetTransform(out Transform transform)
        => b2Body_GetTransform(Native, out transform);

    /// <summary>
    /// Set the position of the body's origin and rotation.
    /// Manipulating a body's transform may cause non-physical behavior.
    /// </summary>
    /// <remarks>
    /// Note: contacts are updated on the next call to <see cref="World.Step(float, int, int)"/>.
    /// </remarks>
    /// <param name="position">The world position of the body's local origin.</param>
    /// <param name="angle">The world rotation in radians.</param>
    public void SetTransform(Vector2 position, float angle)
        => b2Body_SetTransform(Native, ref position, angle);

    /// <summary>
    /// Apply a force at a world point. If the force is not
    /// applied at the center of mass, it will generate a torque and
    /// affect the angular velocity.
    /// </summary>
    /// <param name="force">The world force vector, usually in Newtons (N).</param>
    /// <param name="point">The world position of the point of application.</param>
    /// <param name="wake">Also wake up the body.</param>
    public void ApplyForce(Vector2 force, Vector2 point, bool wake)
        => b2Body_ApplyForce(Native, ref force, ref point, wake);

    /// <summary>
    /// Apply a force to the center of mass.
    /// </summary>
    /// <param name="force">The world force vector, usually in Newtons (N).</param>
    /// <param name="wake">Also wake up the body.</param>
    public void ApplyForceToCenter(Vector2 force, bool wake)
        => b2Body_ApplyForceToCenter(Native, ref force, wake);

    /// <summary>
    /// Apply a torque. This affects the angular velocity without affecting the
    /// linear velocity of the center of mass.
    /// </summary>
    /// <param name="torque">The torque about the z-axis (out of the screen), usually in N-m.</param>
    /// <param name="wake">Also wake up the body.</param>
    public void ApplyTorque(float torque, bool wake)
        => b2Body_ApplyTorque(Native, torque, wake);

    /// <summary>
    /// Apply an impulse at a point. This immediately modifies the velocity.
    /// It also modifies the angular velocity if the point of application
    /// is not at the center of mass.
    /// </summary>
    /// <param name="impluse">The world impulse vector, usually in N-seconds or kg-m/s.</param>
    /// <param name="point">The world position of the point of application.</param>
    /// <param name="wake">Also wake up the body.</param>
    public void ApplyLinearImpulse(Vector2 impluse, Vector2 point, bool wake)
        => b2Body_ApplyLinearImpulse(Native, ref impluse, ref point, wake);

    /// <summary>
    /// Apply an impulse to the center of mass. This immediately modifies the velocity.
    /// </summary>
    /// <param name="impulse">The world impulse vector, usually in N-seconds or kg-m/s.</param>
    /// <param name="wake">Also wake up the body.</param>
    public void ApplyLinearImpulseToCenter(Vector2 impulse, bool wake)
        => b2Body_ApplyLinearImpulseToCenter(Native, ref impulse, wake);

    /// <summary>
    /// Apply an angular impulse.
    /// </summary>
    /// <param name="impulse">The angular impulse in units of kg*m*m/s.</param>
    /// <param name="wake">Also wake up the body.</param>
    public void ApplyAngularImpulse(float impulse, bool wake)
        => b2Body_ApplyAngularImpulse(Native, impulse, wake);

    /// <summary>
    /// Gets the world coordinates of a point given the local coordinates.
    /// </summary>
    /// <param name="localPoint">A point on the body measured relative to the body's origin.</param>
    /// <returns>The same point expressed in world coordinates.</returns>
    public Vector2 GetWorldPoint(Vector2 localPoint)
    {
        b2Body_GetWorldPoint(Native, ref localPoint, out var value);
        return value;
    }

    /// <summary>
    /// Gets the world coordinates of a vector given the local coordinates.
    /// </summary>
    /// <param name="localVector">A vector fixed in the body.</param>
    /// <returns>The same vector expresssed in world coordinates.</returns>
    public Vector2 GetWorldVector(Vector2 localVector)
    {
        b2Body_GetWorldVector(Native, ref localVector, out var value);
        return value;
    }

    /// <summary>
    /// Gets a local point relative to the body's origin given a world point.
    /// </summary>
    /// <param name="worldPoint">A point in world coordinates.</param>
    /// <returns>The corresponding local point relative to the body's origin.</returns>
    public Vector2 GetLocalPoint(Vector2 worldPoint)
    {
        b2Body_GetLocalPoint(Native, ref worldPoint, out var value);
        return value;
    }

    /// <summary>
    /// Gets a local vector given a world vector.
    /// </summary>
    /// <param name="worldVector">A vector in world coordinates.</param>
    /// <returns>The corresponding local vector.</returns>
    public Vector2 GetLocalVector(Vector2 worldVector)
    {
        b2Body_GetLocalVector(Native, ref worldVector, out var value);
        return value;
    }

    /// <summary>
    /// Gets the world linear velocity of a world point attached to this body.
    /// </summary>
    /// <param name="worldPoint">A point in world coordinates.</param>
    /// <returns>The world velocity of a point.</returns>
    public Vector2 GetLinearVelocityFromWorldPoint(Vector2 worldPoint)
    {
        b2Body_GetLinearVelocityFromWorldPoint(Native, ref worldPoint, out var value);
        return value;
    }

    /// <summary>
    /// Gets the world velocity of a local point.
    /// </summary>
    /// <param name="localPoint">A point in local coordinates.</param>
    /// <returns>The world velocity of a point.</returns>
    public Vector2 GetLinearVelocityFromLocalPoint(Vector2 localPoint)
    {
        b2Body_GetLinearVelocityFromLocalPoint(Native, ref localPoint, out var value);
        return value;
    }

    public static bool operator ==(Body a, Body b)
        => a.Equals(b);

    public static bool operator !=(Body a, Body b)
        => !a.Equals(b);

    /// <inheritdoc/>
    public bool Equals(Body other)
        => _nativeHandle.Equals(other._nativeHandle);

    /// <inheritdoc/>
    public override bool Equals(object obj)
        => obj is Body body && Equals(body);

    /// <inheritdoc/>
    public override int GetHashCode()
        => _nativeHandle.GetHashCode();
}
