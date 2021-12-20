using Box2D.Core;
using Box2D.Math;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

using static Interop.NativeMethods;
using static Errors;

public enum ContactFeatureType : byte
{
    Vertex = 0,
    Face = 1,
}

[StructLayout(LayoutKind.Sequential)]
public struct ContactFeature
{
    public byte IndexA { get; set; }

    public byte IndexB { get; set; }

    public ContactFeatureType TypeA { get; set; }

    public ContactFeatureType TypeB { get; set; }
}

[StructLayout(LayoutKind.Sequential)]
public struct ManifoldPoint
{
    public Vec2 LocalPoint { get; set; }

    public float NormalImpulse { get; set; }

    public float TangentImpulse { get; set; }

    public ContactFeature Id { get; set; }
}

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
            throw new ArgumentException("The native pointer cannot be null.", nameof(native));
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

public class WorldManifold : Box2DObject
{
    public Vec2 Normal
    {
        get
        {
            ThrowIfDisposed();
            b2WorldManifold_get_normal(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2WorldManifold_set_normal(Native, ref value);
        }
    }

    public Box2DOwnedArray<Vec2> Points { get; }

    public Box2DOwnedArray<float> Separations { get; }

    public WorldManifold() : base(isUserOwned: true)
    {
        var native = b2WorldManifold_new(out var points, out var separations);
        Points = new(this, points, 2);
        Separations = new(this, separations, 2);

        Initialize(native);
    }

    public void Initialize(in Manifold manifold, Transform xfA, float radiusA, Transform xfB, float radiusB)
    {
        ThrowIfDisposed();
        b2WorldManifold_Initialize(Native, manifold.Native, ref xfA, radiusA, ref xfB, radiusB);
    }

    private protected override void Dispose(bool disposing)
    {
        b2WorldManifold_delete(Native);
    }
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct RayCastInput
{
    public Vec2 P1 { get; init; }

    public Vec2 P2 { get; init; }

    public float MaxFraction { get; init; } = 1f;
}

[StructLayout(LayoutKind.Sequential)]
public struct RayCastOutput
{
    public Vec2 Normal { get; set; }

    public float Fraction { get; set; }
}

[StructLayout(LayoutKind.Sequential)]
public struct AABB
{
    public Vec2 LowerBound { get; set; }

    public Vec2 UpperBound { get; set; }

    public bool IsValid
    {
        get
        {
            var d = UpperBound - LowerBound;
            return d.X >= 0f & d.Y >= 0f && LowerBound.IsValid && UpperBound.IsValid;
        }
    }

    public Vec2 Center => 0.5f * (LowerBound + UpperBound);

    public Vec2 Extents => 0.5f * (UpperBound - LowerBound);

    public float Perimeter
    {
        get
        {
            var wx = UpperBound.X - LowerBound.X;
            var wy = UpperBound.Y - LowerBound.Y;
            return 2f * (wx + wy);
        }
    }

    public void Combine(AABB other)
    {
        LowerBound = Vec2.Min(LowerBound, other.LowerBound);
        UpperBound = Vec2.Max(UpperBound, other.UpperBound);
    }

    public void Combine(AABB aabb1, AABB aabb2)
    {
        LowerBound = Vec2.Min(aabb1.LowerBound, aabb2.LowerBound);
        UpperBound = Vec2.Max(aabb1.UpperBound, aabb2.UpperBound);
    }

    public bool Contains(AABB other)
    {
        var result = true;
        result = result && LowerBound.X <= other.LowerBound.X;
        result = result && LowerBound.Y <= other.LowerBound.Y;
        result = result && other.UpperBound.X <= UpperBound.X;
        result = result && other.UpperBound.Y <= UpperBound.Y;
        return result;
    }
}
