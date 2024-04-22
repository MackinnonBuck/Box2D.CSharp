using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

/// <summary>
/// Ray-cast input data. The ray extends from <c>P1 to P1 + MaxFraction * (P2 - P1)</c>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct RayCastInput
{
    /// <summary>
    /// Gets the first point.
    /// </summary>
    public Vector2 P1 { get; init; }

    /// <summary>
    /// Gets the second point.
    /// </summary>
    public Vector2 P2 { get; init; }

    /// <summary>
    /// Gets the maximum fraction.
    /// </summary>
    public float MaxFraction { get; init; } = 1f;

    /// <summary>
    /// Constructs a new <see cref="RayCastInput"/>.
    /// </summary>
    public RayCastInput() { }
}
