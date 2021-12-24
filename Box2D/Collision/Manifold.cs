using System;

namespace Box2D;

using static NativeMethods;

public enum ManifoldType
{
    Circles,
    FaceA,
    FaceB,
}

public readonly ref struct Manifold
{
    private readonly IntPtr _native;

    internal IntPtr Native
    {
        get
        {
            Errors.ThrowIfInvalidAccess(_native);
            return _native;
        }
    }

    public bool IsValid => _native != IntPtr.Zero;

    public Box2DArray<ManifoldPoint> Points { get; }

    public Vec2 LocalNormal
    {
        get
        {
            b2Manifold_get_localNormal(Native, out var value);
            return value;
        }
        set => b2Manifold_set_localNormal(Native, ref value);
    }

    public Vec2 LocalPoint
    {
        get
        {
            b2Manifold_get_localPoint(Native, out var value);
            return value;
        }
        set => b2Manifold_set_localPoint(Native, ref value);
    }

    public ManifoldType Type
    {
        get => b2Manifold_get_type(Native);
        set => b2Manifold_set_type(Native, value);
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
        _native = native;
        Points = points;
    }
}
