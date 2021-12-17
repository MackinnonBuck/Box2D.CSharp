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
    public abstract ShapeType Type { get; }

    public float Radius
    {
        get => b2Shape_GetRadius(Native);
        set => b2Shape_SetRadius(Native, value);
    }

    public int ChildCount => b2Shape_GetChildCount(Native);

    internal static Shape? FromIntPtr(IntPtr obj, ShapeType type)
    {
        // TODO: Shape instance deduplication?

        if (obj == IntPtr.Zero)
        {
            return null;
        }

        return type switch
        {
            ShapeType.Circle => new CircleShape(obj),
            ShapeType.Edge => throw new NotImplementedException(),
            ShapeType.Chain => throw new NotImplementedException(),
            ShapeType.Polygon => new PolygonShape(obj),
            var x => throw new ArgumentException($"Invalid shape type '{x}'.", nameof(type)),
        };
    }

    public Shape(bool isUserOwned) : base(isUserOwned)
    {
    }

    public void ComputeAABB(out AABB aabb, Transform transform, int childIndex)
        => b2Shape_ComputeAABB(Native, out aabb, transform, childIndex);

    public void ComputeMass(out MassData massData, float density)
        => b2Shape_ComputeMass(Native, out massData, density);

    public bool RayCast(out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex)
        => b2Shape_RayCast(Native, out output, in input, transform, childIndex);

    public bool TestPoint(Transform transform, Vec2 p)
        => b2Shape_TestPoint(Native, transform, p);

    sealed private protected override void Dispose(bool disposing)
    {
        if (IsUserOwned)
        {
            b2Shape_delete(Native);
        }
    }
}
