using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

/// <summary>
/// A manifold point is a contact point belonging to a contact
/// manifold. It holds details related to the geometry and dynamics
/// of the contact points.
/// The local point usage depends on the manifold type:
/// <list type="bullet">
/// <item><see cref="ManifoldType.Circles"/>: The local center of circle B.</item>
/// <item><see cref="ManifoldType.FaceA"/>: The local center of circle B or the clip point of polygon B.</item>
/// <item><see cref="ManifoldType.FaceB"/>: The clip point of polygon A.</item>
/// </list>
/// Note: the impulses are used for internal caching and may not
/// provide reliable contact forces, especially for high speed collisions.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ManifoldPoint
{
    /// <summary>
    /// Gets the local point.
    /// </summary>
    public Vector2 LocalPoint { get; set; }

    /// <summary>
    /// Gets the non-penetration impulse.
    /// </summary>
    public float NormalImpulse { get; set; }

    /// <summary>
    /// Gets the friction impulse.
    /// </summary>
    public float TangentImpulse { get; set; }

    /// <summary>
    /// Gets the unique identifier for a contact point between two shapes.
    /// </summary>
    public ContactFeature Id { get; set; }
}
