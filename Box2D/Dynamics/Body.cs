using System;

namespace Box2D;

using static NativeMethods;

public enum BodyType
{
    Static,
    Kinematic,
    Dynamic,
}

public class BodyDef : Box2DDisposableObject
{
    internal IntPtr InternalUserData
    {
        get => b2BodyDef_get_userData(Native);
        set => b2BodyDef_set_userData(Native, value);
    }

    public object? UserData { get; set; }

    public BodyType Type
    {
        get => b2BodyDef_get_type(Native);
        set => b2BodyDef_set_type(Native, value);
    }

    public Vec2 Position
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

    public Vec2 LinearVelocity
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

    public Vec2 Position
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

    public Vec2 LinearVelocity
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

    public void SetTransform(Vec2 position, float angle)
        => b2Body_SetTransform(Native, ref position, angle);

    public void ApplyForce(Vec2 force, Vec2 point, bool wake)
        => b2Body_ApplyForce(Native, ref force, ref point, wake);

    public void ApplyForceToCenter(Vec2 force, bool wake)
        => b2Body_ApplyForceToCenter(Native, ref force, wake);

    public void ApplyTorque(float torque, bool wake)
        => b2Body_ApplyTorque(Native, torque, wake);

    public void ApplyLinearImpulse(Vec2 impluse, Vec2 point, bool wake)
        => b2Body_ApplyLinearImpulse(Native, ref impluse, ref point, wake);

    public void ApplyLinearImpulseToCenter(Vec2 impulse, bool wake)
        => b2Body_ApplyLinearImpulseToCenter(Native, ref impulse, wake);

    public void ApplyAngularImpulse(float impulse, bool wake)
        => b2Body_ApplyAngularImpulse(Native, impulse, wake);
}
