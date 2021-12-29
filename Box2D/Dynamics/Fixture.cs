using Box2D.Collision;
using Box2D.Core;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

[StructLayout(LayoutKind.Sequential)]
public struct Filter
{
    public ushort CategoryBits { get; set; } = 0x0001;

    public ushort MaskBits { get; set; } = 0xFFFF;

    public short GroupIndex { get; set; } = 0;
}

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

public sealed class Fixture : Box2DSubObject, IBox2DList<Fixture>
{
    internal static FixtureFromIntPtr FromIntPtr { get; } = new();

    internal class FixtureFromIntPtr : IGetFromIntPtr<Fixture>
    {
        public IntPtr GetManagedHandle(IntPtr obj)
            => b2Fixture_GetUserData(obj);
    }

    private Shape? _shape;

    public ShapeType Type { get; }

    public Body Body { get; }

    public object? UserData { get; set; }

    public Shape Shape
    {
        get
        {
            // We can't simply cache the shape instance provided by the FixtureDef
            // because each fixture allocates its own copy of the specified shape.

            if (_shape is null || !_shape.IsValid)
            {
                var shapeNative = b2Fixture_GetShape(Native);
                _shape = Shape.FromIntPtr.Create(shapeNative, Type)!;
            }

            return _shape;
        }
    }

    public bool IsSensor
    {
        get => b2Fixture_IsSensor(Native);
        set => b2Fixture_SetSensor(Native, value);
    }

    public Fixture? Next => FromIntPtr.Get(b2Fixture_GetNext(Native));

    internal Fixture(Body body, in FixtureDef def)
    {
        if (def.Shape is null)
        {
            throw new InvalidOperationException($"Cannot create a {nameof(Fixture)} from a {nameof(FixtureDef)} without a {nameof(Shape)}.");
        }

        Type = def.Shape.Type;
        Body = body;
        UserData = def.UserData;

        def.InternalUserData = Handle;
        var native = b2Body_CreateFixture(body.Native, def.Native);
        Initialize(native);
    }

    internal Fixture(Body body, Shape shape, float density)
    {
        Type = shape.Type;
        Body = body;

        var native = b2Body_CreateFixture2(body.Native, shape.Native, density, Handle);
        Initialize(native);
    }

    public bool TestPoint(Vector2 p)
        => b2Fixture_TestPoint(Native, ref p);

    internal override void Invalidate()
    {
        // This shape is not user-owned, so disposing it is safe.
        _shape?.Dispose();

        base.Invalidate();
    }
}
