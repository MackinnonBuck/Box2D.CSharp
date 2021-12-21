using System.Runtime.InteropServices;

namespace Box2D;

using static NativeMethods;

public class WorldManifold : Box2DObject
{
    public Vec2 Normal
    {
        get
        {
            ThrowIfDisposed();
            b2WorldManifold_get_normal(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2WorldManifold_set_normal(Native, ref value);
        }
    }

    public Box2DOwnedArray<Vec2> Points { get; }

    public Box2DOwnedArray<float> Separations { get; }

    public WorldManifold() : base(isUserOwned: true)
    {
        var native = b2WorldManifold_new(out var points, out var separations);
        Points = new(this, points, 2);
        Separations = new(this, separations, 2);

        Initialize(native);
    }

    public void Initialize(in Manifold manifold, Transform xfA, float radiusA, Transform xfB, float radiusB)
    {
        ThrowIfDisposed();
        b2WorldManifold_Initialize(Native, manifold.Native, ref xfA, radiusA, ref xfB, radiusB);
    }

    private protected override void Dispose(bool disposing)
    {
        b2WorldManifold_delete(Native);
    }
}
