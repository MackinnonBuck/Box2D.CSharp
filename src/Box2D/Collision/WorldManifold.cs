using Box2D.Collections;
using Box2D.Core;
using Box2D.Math;
using System.Numerics;

namespace Box2D.Collision;

using static Interop.NativeMethods;

/// <summary>
/// Used to compute the current state of a contact manifold.
/// </summary>
public class WorldManifold : Box2DDisposableObject
{
    /// <summary>
    /// Gets or sets the world vector pointing from A to B.
    /// </summary>
    public Vector2 Normal
    {
        get
        {
            b2WorldManifold_get_normal(Native, out var value);
            return value;
        }
        set => b2WorldManifold_set_normal(Native, ref value);
    }

    /// <summary>
    /// Gets the world contact points.
    /// </summary>
    public ArrayMember<Vector2> Points { get; }

    /// <summary>
    /// Gets the separations.
    /// </summary>
    public ArrayMember<float> Separations { get; }

    /// <summary>
    /// Constructs a new <see cref="WorldManifold"/> instance.
    /// </summary>
    public WorldManifold() : base(isUserOwned: true)
    {
        var native = b2WorldManifold_new(out var points, out var separations);
        Points = new(points, 2);
        Separations = new(separations, 2);

        Initialize(native);
    }

    /// <summary>
    /// Evaluates the manifold with the supplied transforms. This assumes
    /// modest motion from the original state. This does not change the
    /// point count, impulses, etc. The radii must come from the shapes
    /// that generated the manifold.
    /// </summary>
    public void Initialize(in Manifold manifold, Transform xfA, float radiusA, Transform xfB, float radiusB)
        => b2WorldManifold_Initialize(Native, manifold.Native, ref xfA, radiusA, ref xfB, radiusB);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        Points.Invalidate();
        Separations.Invalidate();

        b2WorldManifold_delete(Native);
    }
}
