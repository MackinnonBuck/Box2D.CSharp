using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public readonly struct RayCastInput
{
    public Vec2 P1 { get; init; }

    public Vec2 P2 { get; init; }

    public float MaxFraction { get; init; } = 1f;
}
