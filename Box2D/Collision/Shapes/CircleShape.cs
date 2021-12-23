using System;

namespace Box2D;

using static NativeMethods;

public class CircleShape : Shape
{
    public override ShapeType Type => ShapeType.Circle;

    public Vec2 P
    {
        get
        {
            ThrowIfDisposed();
            b2CircleShape_get_m_p(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2CircleShape_set_m_p(Native, ref value);
        }
    }

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
