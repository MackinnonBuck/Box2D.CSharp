using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct RayCastOutput
{
    public Vec2 Normal { get; set; }

    public float Fraction { get; set; }
}
