using System;

namespace Box2D;

using static NativeMethods;
using static Errors;

public enum ManifoldType
{
    Circles,
    FaceA,
    FaceB,
}

public readonly ref struct Manifold
{
    internal IntPtr Native { get; }

    public bool IsValid => Native != IntPtr.Zero;

    public Box2DArray<ManifoldPoint> Points { get; }

    public Vec2 LocalNormal
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            b2Manifold_get_localNormal(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Manifold_set_localNormal(Native, ref value);
        }
    }

    public Vec2 LocalPoint
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            b2Manifold_get_localPoint(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Manifold_set_localPoint(Native, ref value);
        }
    }

    public ManifoldType Type
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return b2Manifold_get_type(Native);
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2Manifold_set_type(Native, value);
        }
    }

    internal static Manifold Create(IntPtr native)
    {
        if (native == IntPtr.Zero)
        {
            return default;
        }

        var pointsNative = b2Manifold_get_points(native, out int pointsLength);
        return new(native, new(pointsNative, pointsLength));
    }

    private Manifold(IntPtr native, in Box2DArray<ManifoldPoint> points)
    {
        Native = native;
        Points = points;
    }
}
