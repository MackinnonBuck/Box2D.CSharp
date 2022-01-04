using Box2D.Core.Allocation;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision.Shapes;

using static Interop.NativeMethods;

/// <summary>
/// Represents a solid convex polygon. It is assumed that the interior of the polygon is to
/// the left of each edge.
/// </summary>
public class PolygonShape : Shape
{
    private static readonly IAllocator<PolygonShape> _allocator = Allocator.Create<PolygonShape>(static () => new());

    /// <inheritdoc/>
    public override ShapeType Type => ShapeType.Polygon;

    /// <summary>
    /// Gets the centroid of the polygon shape.
    /// </summary>
    public Vector2 Centroid
    {
        get
        {
            b2PolygonShape_get_m_centroid(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Creates a new <see cref="PolygonShape"/>.
    /// </summary>
    public static PolygonShape Create()
        => _allocator.Allocate();

    private PolygonShape() : base(isUserOwned: true)
    {
        var native = b2PolygonShape_new();
        Initialize(native);
    }

    internal PolygonShape(IntPtr native) : base(isUserOwned: false)
    {
        Initialize(native);
    }

    /// <summary>
    /// Create a convex hull from teh given array of local points.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Warning: The points may be re-ordered, even if they form a convex polygon.
    /// </para>
    /// <para>
    /// Warning: Collinear points are handled but not removed. Collinear points may
    /// lead to poor stacking behavior.
    /// </para>
    /// </remarks>
    /// <param name="points"></param>
    public void Set(Span<Vector2> points)
        => b2PolygonShape_Set(Native, ref MemoryMarshal.GetReference(points), points.Length);

    /// <summary>
    /// Build vertices to represent an axis-aligned box centered on the local origin.
    /// </summary>
    /// <param name="hx">The half-width.</param>
    /// <param name="hy">The half-height.</param>
    public void SetAsBox(float hx, float hy)
        => b2PolygonShape_SetAsBox(Native, hx, hy);

    /// <summary>
    /// Build vertices to represent an oriented box.
    /// </summary>
    /// <param name="hx">The half-width.</param>
    /// <param name="hy">The half-height.</param>
    /// <param name="center">The center of the box in local coordinates.</param>
    /// <param name="angle">The rotation of the box in local coordinates.</param>
    public void SetAsBox(float hx, float hy, Vector2 center, float angle)
        => b2PolygonShape_SetAsBox2(Native, hx, hy, ref center, angle);

    private protected override bool TryRecycle()
        => _allocator.TryRecycle(this);

    private protected override void Reset()
        => b2PolygonShape_reset(Native);
}
