using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

/// <summary>
/// Ray-cast output data. The ray hits at <c>P1 + Fraction * (P2 - P1)</c>, where
/// <c>P1</c> and <c>P2</c> come from <see cref="RayCastInput"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RayCastOutput
{
    /// <summary>
    /// Gets or sets the normal.
    /// </summary>
    public Vector2 Normal { get; set; }

    /// <summary>
    /// Gets or sets the fraction.
    /// </summary>
    public float Fraction { get; set; }
}
