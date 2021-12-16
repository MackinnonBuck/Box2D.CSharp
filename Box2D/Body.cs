using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Box2D;

using static NativeMethods;

public enum BodyType
{
    Static,
    Kinematic,
    Dyanmic,
}

public struct BodyDef
{
    public BodyType Type { get; set; } = BodyType.Static;
    public Vec2 Position { get; set; } = Vec2.Zero;
    public float Angle { get; set; } = 0f;
    public Vec2 LinearVelocity { get; set; } = Vec2.Zero;
    public float AngularVelocity { get; set; } = 0f;
    public float LinearDamping { get; set; } = 0f;
    public float AngularDamping { get; set; } = 0f;
    public bool AllowSleep { get; set; } = true;
    public bool Awake { get; set; } = true;
    public bool FixedRotation { get; set; } = false;
    public bool Bullet { get; set; } = false;
    public bool Enabled { get; set; } = true;
    public object? UserData { get; set; } = default;
    public float GravityScale { get; set; } = 1f;
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
    public Vec2 Position
    {
        get
        {
            b2Body_GetPosition(Native, out var value);
            return value;
        }
    }

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

    internal Body(IntPtr worldNative, ref BodyDef def)
    {
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak));
        var defInternal = def.ToInternalFormat(Handle);
        var native = b2World_CreateBody(worldNative, ref defInternal);

        Initialize(native);
    }

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
    internal static BodyDefInternal ToInternalFormat(this ref BodyDef def, IntPtr userData)
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

