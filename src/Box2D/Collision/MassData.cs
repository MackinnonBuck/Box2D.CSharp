using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

/// <summary>
/// Holds mass data computed for a shape.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MassData
{
    /// <summary>
    /// The mass of the shape, usually in kilograms.
    /// </summary>
    public float Mass { get; set; }

    /// <summary>
    /// The position of the shape's centroid relative to the shape's origin.
    /// </summary>
    public Vector2 Center { get; set; }

    /// <summary>
    /// The rotational inertia of the shape about the local origin.
    /// </summary>
    public float Inertia { get; set; }
}
