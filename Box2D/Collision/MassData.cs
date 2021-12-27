using Box2D.Math;

namespace Box2D.Collision;

public struct MassData
{
    public float Mass { get; set; }

    public Vec2 Center { get; set; }

    public float I { get; set; }
}
