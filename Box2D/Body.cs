using Box2D.Math;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Box2D;

using static Interop.NativeMethods;

public enum BodyType
{
    Static,
    Kinematic,
    Dyanmic,
}

public readonly struct BodyDef
{
    public BodyType Type { get; init; } = BodyType.Static;
    public Vec2 Position { get; init; } = Vec2.Zero;
    public float Angle { get; init; } = 0f;
    public Vec2 LinearVelocity { get; init; } = Vec2.Zero;
    public float AngularVelocity { get; init; } = 0f;
    public float LinearDamping { get; init; } = 0f;
    public float AngularDamping { get; init; } = 0f;
    public bool AllowSleep { get; init; } = true;
    public bool Awake { get; init; } = true;
    public bool FixedRotation { get; init; } = false;
    public bool Bullet { get; init; } = false;
    public bool Enabled { get; init; } = true;
    public object? UserData { get; init; } = default;
    public float GravityScale { get; init; } = 1f;
}

[StructLayout(LayoutKind.Sequential)]
internal struct BodyDefInternal
{
    public BodyType type;
    public Vec2 position;
    public float angle;
    public Vec2 linearVelocity;
    public float angularVelocity;
    public float linearDamping;
    public float angularDamping;
    [MarshalAs(UnmanagedType.U1)]
    public bool allowSleep;
    [MarshalAs(UnmanagedType.U1)]
    public bool awake;
    [MarshalAs(UnmanagedType.U1)]
    public bool fixedRotation;
    [MarshalAs(UnmanagedType.U1)]
    public bool bullet;
    [MarshalAs(UnmanagedType.U1)]
    public bool enabled;
    public IntPtr userData;
    public float gravityScale;
}

public sealed class Body : Box2DObject
{
    public object? UserData { get; set; }

    public Vec2 Position
    {
        get
        {
            b2Body_GetPosition(Native, out var value);
            return value;
        }
    }

    public float Angle => b2Body_GetAngle(Native);

    public Fixture? FixtureList => Fixture.FromIntPtr(b2Body_GetFixtureList(Native));

    public JointEdge? JointList => JointEdge.FromIntPtr(b2Body_GetJointList(Native));

    public Body? Next => FromIntPtr(b2Body_GetNext(Native));

    internal IntPtr Handle { get; private set; }

    internal static Body? FromIntPtr(IntPtr obj)
    {
        if (obj == IntPtr.Zero)
        {
            return null;
        }

        var userData = b2Body_GetUserData(obj);
       
        if (userData == IntPtr.Zero)
        {
            throw new InvalidOperationException("The Box2D body does not have an associated managed object.");
        }

        if (GCHandle.FromIntPtr(userData).Target is not Body body)
        {
            throw new InvalidOperationException($"The managed {nameof(Body)} object could not be revived.");
        }

        return body;
    }

    internal Body(IntPtr worldNative, in BodyDef def)
    {
        UserData = def.UserData;
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak));
        var defInternal = def.ToInternalFormat(Handle);
        var native = b2World_CreateBody(worldNative, ref defInternal);

        Initialize(native);
    }

    public Fixture CreateFixture(in FixtureDef def)
        => new(Native, in def);

    public Fixture CreateFixture(Shape shape, float density)
        => new(Native, shape, density);

    internal void InvalidateInstance()
    {
        Invalidate();

        Debug.Assert(Handle != IntPtr.Zero);
        GCHandle.FromIntPtr(Handle).Free();
        Handle = IntPtr.Zero;
    }
}

public static class BodyDefExtensions
{
    internal static BodyDefInternal ToInternalFormat(this in BodyDef def, IntPtr userData)
        => new()
        {
            type = def.Type,
            position = def.Position,
            angle = def.Angle,
            linearVelocity = def.LinearVelocity,
            angularVelocity = def.AngularVelocity,
            linearDamping = def.LinearDamping,
            angularDamping = def.AngularDamping,
            allowSleep = def.AllowSleep,
            awake = def.Awake,
            fixedRotation = def.FixedRotation,
            bullet = def.Bullet,
            enabled = def.Enabled,
            userData = userData,
            gravityScale = def.GravityScale,
        };
}

