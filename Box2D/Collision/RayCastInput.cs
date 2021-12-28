using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

[StructLayout(LayoutKind.Sequential)]
public readonly struct RayCastInput
{
    public Vector2 P1 { get; init; }

    public Vector2 P2 { get; init; }

    public float MaxFraction { get; init; } = 1f;
}
