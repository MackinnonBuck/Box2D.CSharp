using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

[StructLayout(LayoutKind.Sequential)]
public struct MassData
{
    public float Mass { get; set; }

    public Vector2 Center { get; set; }

    public float I { get; set; }
}
