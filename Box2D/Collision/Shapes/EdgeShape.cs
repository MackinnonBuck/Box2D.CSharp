using Box2D.Math;
using System;

namespace Box2D.Collision.Shapes;

using static Interop.NativeMethods;

public class EdgeShape : Shape
{
    public Vec2 Vertex0
    {
        get
        {
            b2EdgeShape_get_m_vertex0(Native, out var value);
            return value;
        }
    }

    public Vec2 Vertex1
    {
        get
        {
            b2EdgeShape_get_m_vertex1(Native, out var value);
            return value;
        }
    }

    public Vec2 Vertex2
    {
        get
        {
            b2EdgeShape_get_m_vertex2(Native, out var value);
            return value;
        }
    }

    public Vec2 Vertex3
    {
        get
        {
            b2EdgeShape_get_m_vertex3(Native, out var value);
            return value;
        }
    }

    public bool OneSided => b2EdgeShape_get_m_oneSided(Native);

    public override ShapeType Type => ShapeType.Edge;

    public EdgeShape() : base(isUserOwned: true)
    {
        var native = b2EdgeShape_new();
        Initialize(native);
    }

    internal EdgeShape(IntPtr native) : base(isUserOwned: false)
    {
        Initialize(native);
    }

    public void SetOneSided(Vec2 v0, Vec2 v1, Vec2 v2, Vec2 v3)
        => b2EdgeShape_SetOneSided(Native, ref v0, ref v1, ref v2, ref v3);

    public void SetTwoSided(Vec2 v1, Vec2 v2)
        => b2EdgeShape_SetTwoSided(Native, ref v1, ref v2);
}
