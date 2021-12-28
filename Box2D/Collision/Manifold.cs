using Box2D.Collections;
using Box2D.Core;
using System;
using System.Numerics;

namespace Box2D.Collision;

using static Interop.NativeMethods;

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

    public ArrayRef<ManifoldPoint> Points { get; }

    public Vector2 LocalNormal
    {
        get
        {
            b2Manifold_get_localNormal(Native, out var value);
            return value;
        }
        set => b2Manifold_set_localNormal(Native, ref value);
    }

    public Vector2 LocalPoint
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

    public static void GetPointStates(Span<PointState> state1, Span<PointState> state2, in Manifold manifold1, in Manifold manifold2)
    {
        if (state1.Length != 2)
        {
            throw new ArgumentException($"Expected '{nameof(state1)}' to have a length of 2.", nameof(state1));
        }

        if (state2.Length != 2)
        {
            throw new ArgumentException($"Expected '{nameof(state2)}' to have a length of 2.", nameof(state2));
        }

        b2GetPointStates_wrap(out state1.GetPinnableReference(), out state2.GetPinnableReference(), manifold1.Native, manifold2.Native);
    }

    private Manifold(IntPtr native, in ArrayRef<ManifoldPoint> points)
    {
        _native = native;
        Points = points;
    }
}
