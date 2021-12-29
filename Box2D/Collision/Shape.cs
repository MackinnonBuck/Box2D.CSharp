using Box2D.Collision.Shapes;
using Box2D.Core;
using Box2D.Math;
using System;
using System.Numerics;

namespace Box2D.Collision;

using static Interop.NativeMethods;

public enum ShapeType
{
    Circle = 0,
    Edge = 1,
    Polygon = 2,
    Chain = 3,
}

public abstract class Shape : Box2DDisposableObject
{
    internal static ShapeFromIntPtr FromIntPtr { get; } = new();

    internal struct ShapeFromIntPtr
    {
        public Shape? Create(IntPtr obj, ShapeType type)
        {
            if (obj == IntPtr.Zero)
            {
                return null;
            }

            return type switch
            {
                ShapeType.Circle => new CircleShape(obj),
                ShapeType.Edge => new EdgeShape(obj),
                ShapeType.Chain => throw new NotImplementedException(),
                ShapeType.Polygon => new PolygonShape(obj),
                var x => throw new ArgumentException($"Invalid shape type '{x}'.", nameof(type)),
            };
        }
    }

    public abstract ShapeType Type { get; }

    public float Radius
    {
        get => b2Shape_get_m_radius(Native);
        set => b2Shape_set_m_radius(Native, value);
    }

    public int ChildCount => b2Shape_GetChildCount(Native);

    public Shape(bool isUserOwned) : base(isUserOwned)
    {
    }

    public void ComputeAABB(out AABB aabb, Transform transform, int childIndex)
        => b2Shape_ComputeAABB(Native, out aabb, ref transform, childIndex);

    public void ComputeMass(out MassData massData, float density)
        => b2Shape_ComputeMass(Native, out massData, density);

    public bool RayCast(out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex)
        => b2Shape_RayCast(Native, out output, in input, ref transform, childIndex);

    public bool TestPoint(Transform transform, Vector2 p)
        => b2Shape_TestPoint(Native, ref transform, ref p);

    protected override void Dispose(bool disposing)
    {
        if (IsUserOwned)
        {
            b2Shape_delete(Native);
        }
    }
}
