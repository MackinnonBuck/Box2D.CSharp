using Box2D.Collision;
using Box2D.Core;
using System;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

public sealed class FixtureDef : Box2DDisposableObject
{
    private Shape? _shape;

    public object? UserData { get; set; }

    public Shape? Shape
    {
        get => _shape;
        set
        {
            _shape = value;
            b2FixtureDef_set_shape(Native, value?.Native ?? IntPtr.Zero);
        }
    }

    internal IntPtr InternalUserData
    {
        get => b2FixtureDef_get_userData(Native);
        set => b2FixtureDef_set_userData(Native, value);
    }

    public float Friction
    {
        get => b2FixtureDef_get_friction(Native);
        set => b2FixtureDef_set_friction(Native, value);
    }

    public float Restitution
    {
        get => b2FixtureDef_get_restitution(Native);
        set => b2FixtureDef_set_restitution(Native, value);
    }

    public float RestitutionThreshold
    {
        get => b2FixtureDef_get_restitutionThreshold(Native);
        set => b2FixtureDef_set_restitutionThreshold(Native, value);
    }

    public float Density
    {
        get => b2FixtureDef_get_density(Native);
        set => b2FixtureDef_set_density(Native, value);
    }

    public bool IsSensor
    {
        get => b2FixtureDef_get_isSensor(Native);
        set => b2FixtureDef_set_isSensor(Native, value);
    }

    public Filter Filter
    {
        get
        {
            b2FixtureDef_get_filter(Native, out var value);
            return value;
        }
        set => b2FixtureDef_set_filter(Native, ref value);
    }

    public FixtureDef() : base(isUserOwned: true)
    {
        var native = b2FixtureDef_new();
        Initialize(native);
    }

    protected override void Dispose(bool disposing)
        => b2FixtureDef_delete(Native);
}
