using Box2D.Collision.Shapes;
using Box2D.Core;
using Box2D.Math;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Box2D.Collision;

using static Interop.NativeMethods;

/// <summary>
/// Used for collision detection. You can create a shape however you like.
/// Shapes used for simulation in <see cref="Dynamics.World"/> are created
/// automatically when a <see cref="Dynamics.Fixture"/> is created.
/// Shapes may encapsulate one or more child shapes.
/// </summary>
public abstract class Shape : Box2DDisposableObject, IBox2DRecyclableObject
{
    private static readonly Dictionary<IntPtr, Shape> _nativeOwnedShapeCache = new();

    internal static Shape? GetFromCache(IntPtr obj)
    {
        if (!_nativeOwnedShapeCache.TryGetValue(obj, out var cachedShape))
        {
            return null;
        }

        return cachedShape;
    }

    internal static Shape? GetFromCacheOrCreate(IntPtr obj, ShapeType type)
    {
        if (obj == IntPtr.Zero)
        {
            return null;
        }

        if (_nativeOwnedShapeCache.TryGetValue(obj, out var cachedShape))
        {
            return cachedShape;
        }

        Shape shape = type switch
        {
            ShapeType.Circle => new CircleShape(obj),
            ShapeType.Edge => new EdgeShape(obj),
            ShapeType.Chain => throw new NotImplementedException(),
            ShapeType.Polygon => new PolygonShape(obj),
            var x => throw new ArgumentException($"Invalid shape type '{x}'.", nameof(type)),
        };

        _nativeOwnedShapeCache.Add(obj, shape);
        return shape;
    }

    /// <summary>
    /// Gets the type of this shape. You can use this to down cast to the concrete shape.
    /// </summary>
    public abstract ShapeType Type { get; }

    /// <summary>
    /// Gets or sets the radius of the shape.
    /// </summary>
    public float Radius
    {
        get => b2Shape_get_m_radius(Native);
        set => b2Shape_set_m_radius(Native, value);
    }

    /// <summary>
    /// Gets the number of child primitives.
    /// </summary>
    public int ChildCount => b2Shape_GetChildCount(Native);

    private protected Shape(bool isUserOwned) : base(isUserOwned)
    {
    }

    /// <summary>
    /// Given a transform, computes the associated axis-aligned bounding box for the child shape.
    /// </summary>
    /// <param name="aabb">Gets the axis-aligned bounding box.</param>
    /// <param name="transform">The world transform of the shape.</param>
    /// <param name="childIndex">The child shape index.</param>
    public void ComputeAABB(out AABB aabb, Transform transform, int childIndex)
        => b2Shape_ComputeAABB(Native, out aabb, ref transform, childIndex);

    /// <summary>
    /// Computes the mass properties of this shpae using its dimensions and density.
    /// </summary>
    /// <param name="massData">Gets the mass data for this shape.</param>
    /// <param name="density">The density in kilograms per meter squared.</param>
    public void ComputeMass(out MassData massData, float density)
        => b2Shape_ComputeMass(Native, out massData, density);

    /// <summary>
    /// Casts a ray against a child shape.
    /// </summary>
    /// <param name="output">Gets the ray-cast results.</param>
    /// <param name="input">The ray-cast input parameters.</param>
    /// <param name="transform">The transform to be applied to the shape.</param>
    /// <param name="childIndex">The child shape index.</param>
    public bool RayCast(out RayCastOutput output, in RayCastInput input, Transform transform, int childIndex)
        => b2Shape_RayCast(Native, out output, in input, ref transform, childIndex);

    /// <summary>
    /// Tests a point for containment in this shape. This only works for convex shapes.
    /// </summary>
    /// <param name="transform">The shape world transform.</param>
    /// <param name="p">A point in world coordinates.</param>
    public bool TestPoint(Transform transform, Vector2 p)
        => b2Shape_TestPoint(Native, ref transform, ref p);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (IsUserOwned)
        {
            b2Shape_delete(Native);
        }
        else
        {
            _nativeOwnedShapeCache.Remove(Native);
        }
    }

    bool IBox2DRecyclableObject.TryRecycle()
        // We only want to attempt recycling if this object is user owned.
        => IsUserOwned && TryRecycle();

    void IBox2DRecyclableObject.Reset()
        => Reset();

    private protected abstract bool TryRecycle();

    private protected abstract void Reset();
}
