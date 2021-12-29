using Box2D.Core;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

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
