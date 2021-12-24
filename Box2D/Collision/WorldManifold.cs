namespace Box2D;

using System;
using static NativeMethods;

public class WorldManifold : Box2DDisposableObject
{
    private readonly IntPtr _points;

    private readonly IntPtr _separations;

    public Vec2 Normal
    {
        get
        {
            b2WorldManifold_get_normal(Native, out var value);
            return value;
        }
        set => b2WorldManifold_set_normal(Native, ref value);
    }

    public Box2DArray<Vec2> Points => new(_points, 2);

    public Box2DArray<float> Separations => new(_separations, 2);

    public WorldManifold() : base(isUserOwned: true)
    {
        var native = b2WorldManifold_new(out _points, out _separations);
        Initialize(native);
    }

    public void Initialize(in Manifold manifold, Transform xfA, float radiusA, Transform xfB, float radiusB)
        => b2WorldManifold_Initialize(Native, manifold.Native, ref xfA, radiusA, ref xfB, radiusB);

    protected override void Dispose(bool disposing)
        => b2WorldManifold_delete(Native);
}
