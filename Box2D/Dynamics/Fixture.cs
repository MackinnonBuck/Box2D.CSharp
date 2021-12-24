using System;
using System.Runtime.InteropServices;

namespace Box2D;

using static NativeMethods;

[StructLayout(LayoutKind.Sequential)]
public struct Filter
{
    public ushort CategoryBits { get; set; } = 0x0001;

    public ushort MaskBits { get; set; } = 0xFFFF;

    public short GroupIndex { get; set; } = 0;
}

public readonly struct FixtureDef
{
    public Shape? Shape { get; init; } = default;

    public object? UserData { get; init; } = default;

    public float Friction { get; init; } = 0.2f;

    public float Restitution { get; init; } = 0f;

    public float RestitutionThreshold { get; init; } = 1f;

    public float Density { get; init; } = 0f;

    public bool IsSensor { get; init; } = false;

    public Filter Filter { get; init; } = new();
}

[StructLayout(LayoutKind.Sequential)]
internal struct FixtureDefInternal
{
    public IntPtr shape;
    public IntPtr userData;
    public float friction;
    public float restitution;
    public float restitutionThreshold;
    public float density;
    [MarshalAs(UnmanagedType.U1)]
    public bool isSensor;
    public Filter filter;
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
            if (_shape is null || !_shape.IsValid)
            {
                var shapeNative = b2Fixture_GetShape(Native);
                _shape = Shape.FromIntPtr.Create(shapeNative, Type)!;
            }

            return _shape;
        }
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
        var defInternal = def.ToInternalFormat(Handle);
        var native = b2Body_CreateFixture(body.Native, ref defInternal);

        Initialize(native);
    }

    internal Fixture(Body body, Shape shape, float density)
        : this(body, new()
        {
            Shape = shape,
            Density = density
        })
    {
    }

    public bool TestPoint(Vec2 p)
        => b2Fixture_TestPoint(Native, ref p);

    private protected override void OnFreeingHandle()
    {
        // This shape is not user-owned, so disposing it is safe.
        _shape?.Dispose();
    }
}

public static class FixtureDefExtensions
{
    internal static FixtureDefInternal ToInternalFormat(this in FixtureDef def, IntPtr userData)
        => new()
        {
            shape = def.Shape?.Native ?? IntPtr.Zero,
            userData = userData,
            friction = def.Friction,
            restitution = def.Restitution,
            restitutionThreshold = def.RestitutionThreshold,
            density = def.Density,
            isSensor = def.IsSensor,
            filter = def.Filter,
        };
}

