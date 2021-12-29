using System;
using System.Numerics;

namespace Box2D.Collision.Shapes;

using static Interop.NativeMethods;

/// <summary>
/// Represents a solid circle shape.
/// </summary>
public class CircleShape : Shape
{
    /// <inheritdoc/>
    public override ShapeType Type => ShapeType.Circle;

    /// <summary>
    /// Gets the position of the circle.
    /// </summary>
    public Vector2 Position
    {
        get
        {
            b2CircleShape_get_m_p(Native, out var value);
            return value;
        }
        set => b2CircleShape_set_m_p(Native, ref value);
    }

    /// <summary>
    /// Constructs a new <see cref="CircleShape"/> instance.
    /// </summary>
    public CircleShape() : base(isUserOwned: true)
    {
        var native = b2CircleShape_new();
        Initialize(native);
    }

    internal CircleShape(IntPtr native) : base(isUserOwned: false)
    {
        Initialize(native);
    }
}
