using Box2D.Collision;
using Box2D.Core;
using Box2D.Math;
using System;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

public enum BodyType
{
    Static,
    Kinematic,
    Dynamic,
}

public sealed class BodyDef : Box2DDisposableObject
{
    public object? UserData { get; set; }

    public BodyType Type
    {
        get => b2BodyDef_get_type(Native);
        set => b2BodyDef_set_type(Native, value);
    }

    public Vector2 Position
    {
        get
        {
            b2BodyDef_get_position(Native, out var value);
            return value;
        }
        set => b2BodyDef_set_position(Native, ref value);
    }

    public float Angle
    {
        get => b2BodyDef_get_angle(Native);
        set => b2BodyDef_set_angle(Native, value);
    }

    public Vector2 LinearVelocity
    {
        get
        {
            b2BodyDef_get_linearVelocity(Native, out var value);
            return value;
        }
        set => b2BodyDef_set_linearVelocity(Native, ref value);
    }

    public float AngularVelocity
    {
        get => b2BodyDef_get_angularVelocity(Native);
        set => b2BodyDef_set_angularVelocity(Native, value);
    }

    public float LinearDamping
    {
        get => b2BodyDef_get_linearDamping(Native);
        set => b2BodyDef_set_linearDamping(Native, value);
    }

    public float AngularDamping
    {
        get => b2BodyDef_get_angularDamping(Native);
        set => b2BodyDef_set_angularDamping(Native, value);
    }

    public bool AllowSleep
    {
        get => b2BodyDef_get_allowSleep(Native);
        set => b2BodyDef_set_allowSleep(Native, value);
    }

    public bool Awake
    {
        get => b2BodyDef_get_awake(Native);
        set => b2BodyDef_set_awake(Native, value);
    }

    public bool FixedRotation
    {
        get => b2BodyDef_get_fixedRotation(Native);
        set => b2BodyDef_set_fixedRotation(Native, value);
    }

    public bool Bullet
    {
        get => b2BodyDef_get_bullet(Native);
        set => b2BodyDef_set_bullet(Native, value);
    }

    public bool Enabled
    {
        get => b2BodyDef_get_enabled(Native);
        set => b2BodyDef_set_enabled(Native, value);
    }

    internal IntPtr InternalUserData
    {
        get => b2BodyDef_get_userData(Native);
        set => b2BodyDef_set_userData(Native, value);
    }

    public float GravityScale
    {
        get => b2BodyDef_get_gravityScale(Native);
        set => b2BodyDef_set_gravityScale(Native, value);
    }

    public BodyDef() : base(isUserOwned: true)
    {
        var native = b2BodyDef_new();
        Initialize(native);
    }

    protected override void Dispose(bool disposing)
        => b2BodyDef_delete(Native);
}

public sealed class Body : Box2DSubObject, IBox2DList<Body>
{
    internal static BodyFromIntPtr FromIntPtr { get; } = new();

    internal class BodyFromIntPtr : IGetFromIntPtr<Body>
    {
        public IntPtr GetManagedHandle(IntPtr obj)
            => b2Body_GetUserData(obj);
    }

    public BodyType Type { get; }

    public World World { get; }

    public object? UserData { get; set; }

    public Vector2 Position
    {
        get
        {
            b2Body_GetPosition(Native, out var value);
            return value;
        }
    }

    public Transform Transform
    {
        get
        {
            b2Body_GetTransform(Native, out var value);
            return value;
        }
    }

    public Vector2 LinearVelocity
    {
        get
        {
            b2Body_GetLinearVelocity(Native, out var value);
            return value;
        }
        set => b2Body_SetLinearVelocity(Native, ref value);
    }

    public float AngularVelocity
    {
        get => b2Body_GetAngularVelocity(Native);
        set => b2Body_SetAngularVelocity(Native, value);
    }

    public float Angle => b2Body_GetAngle(Native);

    public float Mass => b2Body_GetMass(Native);

    public bool Awake
    {
        get => b2Body_IsAwake(Native);
        set => b2Body_SetAwake(Native, value);
    }

    public Fixture? FixtureList => Fixture.FromIntPtr.Get(b2Body_GetFixtureList(Native));

    public JointEdge JointList => new(b2Body_GetJointList(Native));

    public Body? Next => FromIntPtr.Get(b2Body_GetNext(Native));

    internal Body(World world, BodyDef def)
    {
        Type = def.Type;
        World = world;
        UserData = def.UserData;
        def.InternalUserData = Handle;
        var native = b2World_CreateBody(world.Native, def.Native);

        Initialize(native);
    }

    public Fixture CreateFixture(in FixtureDef def)
        => new(this, in def);

    public Fixture CreateFixture(Shape shape, float density)
        => new(this, shape, density);

    public void SetTransform(Vector2 position, float angle)
        => b2Body_SetTransform(Native, ref position, angle);

    public void ApplyForce(Vector2 force, Vector2 point, bool wake)
        => b2Body_ApplyForce(Native, ref force, ref point, wake);

    public void ApplyForceToCenter(Vector2 force, bool wake)
        => b2Body_ApplyForceToCenter(Native, ref force, wake);

    public void ApplyTorque(float torque, bool wake)
        => b2Body_ApplyTorque(Native, torque, wake);

    public void ApplyLinearImpulse(Vector2 impluse, Vector2 point, bool wake)
        => b2Body_ApplyLinearImpulse(Native, ref impluse, ref point, wake);

    public void ApplyLinearImpulseToCenter(Vector2 impulse, bool wake)
        => b2Body_ApplyLinearImpulseToCenter(Native, ref impulse, wake);

    public void ApplyAngularImpulse(float impulse, bool wake)
        => b2Body_ApplyAngularImpulse(Native, impulse, wake);

    public Vector2 GetWorldPoint(Vector2 localPoint)
    {
        b2Body_GetWorldPoint(Native, ref localPoint, out var value);
        return value;
    }

    public Vector2 GetWorldVector(Vector2 localVector)
    {
        b2Body_GetWorldVector(Native, ref localVector, out var value);
        return value;
    }

    public Vector2 GetLocalPoint(Vector2 worldPoint)
    {
        b2Body_GetLocalPoint(Native, ref worldPoint, out var value);
        return value;
    }

    public Vector2 GetLocalVector(Vector2 worldVector)
    {
        b2Body_GetLocalVector(Native, ref worldVector, out var value);
        return value;
    }

    public Vector2 GetLinearVelocityFromWorldPoint(Vector2 worldPoint)
    {
        b2Body_GetLinearVelocityFromWorldPoint(Native, ref worldPoint, out var value);
        return value;
    }

    public Vector2 GetLinearVelocityFromLocalPoint(Vector2 localPoint)
    {
        b2Body_GetLinearVelocityFromLocalPoint(Native, ref localPoint, out var value);
        return value;
    }
}
