using Box2D.Core.Allocation;
using System;
using System.Numerics;

namespace Box2D.Collision.Shapes;

using static Interop.NativeMethods;

/// <summary>
/// Represents a line segment (edge) shape. These can be connected in chains or loops
/// to other edge shapes. Edges created independently are two-sided and do
/// no provide smooth movement across junctions.
/// </summary>
public class EdgeShape : Shape
{
    private static readonly IAllocator<EdgeShape> _allocator = Allocator.Create<EdgeShape>(static () => new());

    /// <summary>
    /// Gets the first adjacent vertex.
    /// </summary>
    public Vector2 Vertex0
    {
        get
        {
            b2EdgeShape_get_m_vertex0(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the first edge vertex.
    /// </summary>
    public Vector2 Vertex1
    {
        get
        {
            b2EdgeShape_get_m_vertex1(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the second edge vertex.
    /// </summary>
    public Vector2 Vertex2
    {
        get
        {
            b2EdgeShape_get_m_vertex2(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the second adjacent vertex.
    /// </summary>
    public Vector2 Vertex3
    {
        get
        {
            b2EdgeShape_get_m_vertex3(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets whether <see cref="Vertex0"/> and <see cref="Vertex3"/> should be used
    /// to create smooth collision.
    /// </summary>
    public bool OneSided => b2EdgeShape_get_m_oneSided(Native);

    /// <inheritdoc/>
    public override ShapeType Type => ShapeType.Edge;

    /// <summary>
    /// Creates a new <see cref="EdgeShape"/> instance.
    /// </summary>
    public static EdgeShape Create()
        => _allocator.Allocate();

    private EdgeShape() : base(isUserOwned: true)
    {
        var native = b2EdgeShape_new();
        Initialize(native);
    }

    internal EdgeShape(IntPtr native) : base(isUserOwned: false)
    {
        Initialize(native);
    }

    /// <summary>
    /// Set this as a part of a sequence. <paramref name="v0"/> precedes the edge and <paramref name="v3"/>
    /// follows. These extra vertices are used to provide smooth movement
    /// across junctions. This also makes the collision one-sided. The edge
    /// normal points to the right looking from <paramref name="v1"/> to <paramref name="v2"/>.
    /// </summary>
    public void SetOneSided(Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3)
        => b2EdgeShape_SetOneSided(Native, ref v0, ref v1, ref v2, ref v3);

    /// <summary>
    /// Set this as an isolated edge. Collision is two-sided.
    /// </summary>
    public void SetTwoSided(Vector2 v1, Vector2 v2)
        => b2EdgeShape_SetTwoSided(Native, ref v1, ref v2);

    private protected override bool TryRecycle()
        => _allocator.TryRecycle(this);

    private protected override void Reset()
        => b2EdgeShape_reset(Native);
}
