using Box2D.Math;
using System;

namespace Box2D;

using static Interop.NativeMethods;

public class PolygonShape : Shape
{
    public override ShapeType Type => ShapeType.Polygon;

    public override int ChildCount => b2PolygonShape_GetChildCount(Native);

    public PolygonShape() : base(isUserOwned: true)
    {
        var native = b2PolygonShape_new();
        Initialize(native);
    }

    internal PolygonShape(IntPtr native) : base(isUserOwned: false)
    {
        Initialize(native);
    }

    public void SetAsBox(float hx, float hy)
        => b2PolygonShape_SetAsBox(Native, hx, hy);

    public void SetAsBox(float hx, float hy, Vec2 center, float angle)
        => b2PolygonShape_SetAsBox2(Native, hx, hy, center, angle);

    public override void ComputeAABB(out AABB aabb, Transform transform, int childIndex)
        => b2PolygonShape_ComputeAABB(Native, out aabb, transform, childIndex);

    public override void ComputeMass(out MassData massData, float density)
        => b2PolygonShape_ComputeMass(Native, out massData, density);

    public override bool RayCast(out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex)
        => b2PolygonShape_RayCast(Native, out output, in input, transform, childIndex);

    public override bool TestPoint(Transform transform, Vec2 p)
        => b2PolygonShape_TestPoint(Native, transform, p);

    private protected override void Dispose(bool disposing)
    {
        if (IsUserOwned)
        {
            b2PolygonShape_delete(Native);
        }
    } 
}
