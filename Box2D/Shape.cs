using Box2D.Math;
using System;

namespace Box2D;

using static Interop.NativeMethods;

public enum ShapeType
{
    Circle = 0,
    Edge = 1,
    Polygon = 2,
    Chain = 3,
    // TypeCount = 4,
}

public struct MassData
{
    public float Mass { get; set; }

    public Vec2 Center { get; set; }

    public float I { get; set; }
}

public abstract class Shape : Box2DDisposableObject
{
    public float Radius => b2Shape_GetRadius(Native);

    public abstract ShapeType Type { get; }

    public abstract int ChildCount { get; }

    public Shape(bool isUserOwned) : base(isUserOwned)
    {
    }

    public abstract bool TestPoint(Transform xf, Vec2 p);

    public abstract bool RayCast(out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex);

    public abstract void ComputeAABB(out AABB aabb, Transform xf, int childIndex);

    public abstract void ComputeMass(out MassData massData, float density);

    internal static Shape? FromIntPtr(IntPtr obj, ShapeType type)
    {
        if (obj == IntPtr.Zero)
        {
            return null;
        }

        return type switch
        {
            ShapeType.Circle => throw new NotImplementedException(),
            ShapeType.Edge => throw new NotImplementedException(),
            ShapeType.Chain => throw new NotImplementedException(),
            ShapeType.Polygon => new PolygonShape(obj),
            var x => throw new ArgumentException($"Invalid shape type '{x}'.", nameof(type)),
        };
    }
}
