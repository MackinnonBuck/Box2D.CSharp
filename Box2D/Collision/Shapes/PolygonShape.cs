using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision.Shapes;

using static Interop.NativeMethods;

public class PolygonShape : Shape
{
    public override ShapeType Type => ShapeType.Polygon;

    public Vector2 Centroid
    {
        get
        {
            b2PolygonShape_get_m_centroid(Native, out var value);
            return value;
        }
    }

    public PolygonShape() : base(isUserOwned: true)
    {
        var native = b2PolygonShape_new();
        Initialize(native);
    }

    internal PolygonShape(IntPtr native) : base(isUserOwned: false)
    {
        Initialize(native);
    }

    public void Set(Span<Vector2> points)
        => b2PolygonShape_Set(Native, ref MemoryMarshal.GetReference(points), points.Length);

    public void SetAsBox(float hx, float hy)
        => b2PolygonShape_SetAsBox(Native, hx, hy);

    public void SetAsBox(float hx, float hy, Vector2 center, float angle)
        => b2PolygonShape_SetAsBox2(Native, hx, hy, ref center, angle);
}
