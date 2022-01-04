using Box2D.Collections;
using Box2D.Core;
using System;
using System.Numerics;

namespace Box2D.Collision;

using static Interop.NativeMethods;

/// <summary>
/// A manifold for two touching convex shapes.
/// Box2D supports multiple types of contact:
/// <list type="bullet">
/// <item>Clip point versus plane with radius.</item>
/// <item>Point versus point with radius (circles).</item>
/// </list>
/// The local point usage depends on the manifold type:
/// <list type="bullet">
/// <item><see cref="ManifoldType.Circles"/>: The local center of circle A.</item>
/// <item><see cref="ManifoldType.FaceA"/>: The local center of face A.</item>
/// <item><see cref="ManifoldType.FaceB"/>: the local center of face B.</item>
/// </list>
/// Similarly the local normal usage:
/// <list type="bullet">
/// <item><see cref="ManifoldType.Circles"/>: Not used.</item>
/// <item><see cref="ManifoldType.FaceA"/>: The normal on polygon A.</item>
/// <item><see cref="ManifoldType.FaceB"/>: The normal on polygon B.</item>
/// </list>
/// We store contacts in this way so that position correction can
/// account for movement, which is critical for continuous physics.
/// All contact scenarios must be expressed in one of these types.
/// </summary>
public readonly ref struct Manifold
{
    private readonly IntPtr _native;

    internal IntPtr Native
    {
        get
        {
            Errors.ThrowIfNull(_native, nameof(Manifold));
            return _native;
        }
    }

    /// <summary>
    /// Gets whether this <see cref="Manifold"/> points to a null unmanaged manifold.
    /// </summary>
    public bool IsNull => _native == IntPtr.Zero;

    /// <summary>
    /// Gets the points of contact.
    /// </summary>
    public ArrayRef<ManifoldPoint> Points { get; }

    /// <summary>
    /// Gets or sets the local normal.
    /// </summary>
    public Vector2 LocalNormal
    {
        get
        {
            b2Manifold_get_localNormal(Native, out var value);
            return value;
        }
        set => b2Manifold_set_localNormal(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local point.
    /// </summary>
    public Vector2 LocalPoint
    {
        get
        {
            b2Manifold_get_localPoint(Native, out var value);
            return value;
        }
        set => b2Manifold_set_localPoint(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the manifold type.
    /// </summary>
    public ManifoldType Type
    {
        get => b2Manifold_get_type(Native);
        set => b2Manifold_set_type(Native, value);
    }

    internal static Manifold Create(IntPtr native)
    {
        if (native == IntPtr.Zero)
        {
            return default;
        }

        var pointsNative = b2Manifold_get_points(native, out int pointsLength);
        return new(native, new(pointsNative, pointsLength));
    }

    /// <summary>
    /// Compute the point states given two manifolds. The states pertain to the transition from <paramref name="manifold1"/>
    /// to <paramref name="manifold2"/>. So <paramref name="state1"/> is either persisted or removed while <paramref name="state2"/>
    /// is either added or persisted.
    /// </summary>
    /// <remarks>
    /// Note: <paramref name="state1"/> and <paramref name="state2"/> must have a length of at least 2.
    /// </remarks>
    /// <exception cref="ArgumentException"></exception>
    public static void GetPointStates(Span<PointState> state1, Span<PointState> state2, in Manifold manifold1, in Manifold manifold2)
    {
        if (state1.Length < 2)
        {
            throw new ArgumentException($"Expected '{nameof(state1)}' to have a length of at least 2.", nameof(state1));
        }

        if (state2.Length < 2)
        {
            throw new ArgumentException($"Expected '{nameof(state2)}' to have a length of at least 2.", nameof(state2));
        }

        b2GetPointStates_wrap(out state1.GetPinnableReference(), out state2.GetPinnableReference(), manifold1.Native, manifold2.Native);
    }

    private Manifold(IntPtr native, in ArrayRef<ManifoldPoint> points)
    {
        _native = native;
        Points = points;
    }
}
