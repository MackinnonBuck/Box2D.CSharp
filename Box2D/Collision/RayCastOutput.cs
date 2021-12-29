using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

[StructLayout(LayoutKind.Sequential)]
public struct RayCastOutput
{
    public Vector2 Normal { get; set; }

    public float Fraction { get; set; }
}
