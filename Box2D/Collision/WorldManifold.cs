using Box2D.Collections;
using Box2D.Core;
using Box2D.Math;

namespace Box2D.Collision;

using static Interop.NativeMethods;

public class WorldManifold : Box2DDisposableObject
{
    public Vec2 Normal
    {
        get
        {
            b2WorldManifold_get_normal(Native, out var value);
            return value;
        }
        set => b2WorldManifold_set_normal(Native, ref value);
    }

    public ArrayMember<Vec2> Points { get; }

    public ArrayMember<float> Separations { get; }

    public WorldManifold() : base(isUserOwned: true)
    {
        var native = b2WorldManifold_new(out var points, out var separations);
        Points = new(points, 2);
        Separations = new(separations, 2);

        Initialize(native);
    }

    public void Initialize(in Manifold manifold, Transform xfA, float radiusA, Transform xfB, float radiusB)
        => b2WorldManifold_Initialize(Native, manifold.Native, ref xfA, radiusA, ref xfB, radiusB);

    protected override void Dispose(bool disposing)
    {
        Points.Invalidate();
        Separations.Invalidate();

        b2WorldManifold_delete(Native);
    }
}
