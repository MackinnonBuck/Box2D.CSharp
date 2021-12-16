namespace Box2D;

using Box2D.Math;

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
    public float Radius { get; protected set; }

    public abstract ShapeType Type { get; }

    public abstract int ChildCount { get; }

    public Shape() : base(isUserOwned: true)
    {
    }

    public abstract bool TestPoint(Transform xf, Vec2 p);

    public abstract bool RayCast(out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex);

    public abstract void ComputeAABB(out AABB aabb, Transform xf, int childIndex);

    public abstract void ComputeMass(out MassData massData, float density);
}
