using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Box2D;

using static Interop.NativeMethods;

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

public class Fixture : Box2DObject, IBox2DList<Fixture>
{
    private Shape? _shape;

    public ShapeType Type { get; }

    public object? UserData { get; set; }

    public Shape Shape
    {
        get
        {
            if (_shape is null)
            {
                var shapeNative = b2Fixture_GetShape(Native);
                _shape = Shape.FromIntPtr(shapeNative, Type)!;
            }

            return _shape;
        }
    }

    public Fixture? Next => FromIntPtr(b2Fixture_GetNext(Native));

    internal IntPtr Handle { get; private set; }

    internal static Fixture? FromIntPtr(IntPtr obj)
    {
        if (obj == IntPtr.Zero)
        {
            return null;
        }

        var userData = b2Fixture_GetUserData(obj);
       
        if (userData == IntPtr.Zero)
        {
            throw new InvalidOperationException("The Box2D body does not have an associated managed object.");
        }

        if (GCHandle.FromIntPtr(userData).Target is not Fixture fixture)
        {
            throw new InvalidOperationException($"The managed {nameof(Fixture)} object could not be revived.");
        }

        return fixture;
    }

    internal Fixture(IntPtr bodyNative, in FixtureDef def)
    {
        if (def.Shape is null)
        {
            throw new InvalidOperationException($"Cannot create a {nameof(Fixture)} from a {nameof(FixtureDef)} without a {nameof(Shape)}.");
        }

        Type = def.Shape.Type;
        UserData = def.UserData;
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak));
        var defInternal = def.ToInternalFormat(Handle);
        var native = b2Body_CreateFixture(bodyNative, ref defInternal);

        Initialize(native);
    }

    internal Fixture(IntPtr bodyNative, Shape shape, float density)
        : this(bodyNative, new()
        {
            Shape = shape,
            Density = density
        })
    {
    }

    internal void InvalidateInstance()
    {
        Invalidate();

        Debug.Assert(Handle != IntPtr.Zero);
        GCHandle.FromIntPtr(Handle).Free();
        Handle = IntPtr.Zero;
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

