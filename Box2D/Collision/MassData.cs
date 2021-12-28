using Box2D.Math;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

[StructLayout(LayoutKind.Sequential)]
public struct MassData
{
    public float Mass { get; set; }

    public Vec2 Center { get; set; }

    public float I { get; set; }
}
