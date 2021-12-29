using Box2D.Collision;
using Box2D.Core;
using Box2D.Math;
using System;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

public sealed class Body : Box2DSubObject, IBox2DList<Body>
{
    internal static BodyFromIntPtr FromIntPtr { get; } = new();

    internal class BodyFromIntPtr : IGetFromIntPtr<Body>
    {
        public IntPtr GetManagedHandle(IntPtr obj)
            => b2Body_GetUserData(obj);
    }

    public World World { get; }

    public object? UserData { get; set; }

    public Vector2 Position
    {
        get
        {
            b2Body_GetPosition(Native, out var value);
            return value;
        }
        set => b2Body_SetPosition(Native, ref value);
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

    public float Angle
    {
        get => b2Body_GetAngle(Native);
        set => b2Body_SetAngle(Native, value);
    }

    public float GravityScale
    {
        get => b2Body_GetGravityScale(Native);
        set => b2Body_SetGravityScale(Native, value);
    }

    public float Mass => b2Body_GetMass(Native);

    public BodyType Type
    {
        get => b2Body_GetType(Native);
        set => b2Body_SetType(Native, value);
    }

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
        World = world;
        UserData = def.UserData;

        var native = b2World_CreateBody(world.Native, def.Native, Handle);
        Initialize(native);
    }

    internal Body(World world)
    {
        World = world;

        var native = b2World_CreateBody2(world.Native, Handle);
        Initialize(native);
    }

    public Fixture CreateFixture(in FixtureDef def)
        => new(this, in def);

    public Fixture CreateFixture(Shape shape, float density)
        => new(this, shape, density);

    public void DestroyFixture(Fixture fixture)
    {
        b2Body_DestroyFixture(Native, fixture.Native);
        fixture.Invalidate();
    }

    public void GetTransform(out Transform transform)
        => b2Body_GetTransform(Native, out transform);

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
