﻿using Box2D.Math;
using System;

namespace Box2D.Collision.Shapes;

using static Interop.NativeMethods;

public class CircleShape : Shape
{
    public override ShapeType Type => ShapeType.Circle;

    public Vec2 Position
    {
        get
        {
            b2CircleShape_get_m_p(Native, out var value);
            return value;
        }
        set => b2CircleShape_set_m_p(Native, ref value);
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
