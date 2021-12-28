using Box2D.Math;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

[StructLayout(LayoutKind.Sequential)]
public struct RayCastOutput
{
    public Vec2 Normal { get; set; }

    public float Fraction { get; set; }
}
