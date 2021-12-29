using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

/// <summary>
/// An axis-aligned bounding box.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct AABB : IEquatable<AABB>
{
    /// <summary>
    /// Gets or sets the lower vertex.
    /// </summary>
    public Vector2 LowerBound { get; set; }

    /// <summary>
    /// Gets or sets the upper vertex.
    /// </summary>
    public Vector2 UpperBound { get; set; }

    /// <summary>
    /// Gets whether the bounds are sorted.
    /// </summary>
    public bool IsValid
    {
        get
        {
            var d = UpperBound - LowerBound;
            return d.X >= 0f & d.Y >= 0f &&
                !float.IsInfinity(LowerBound.X) && !float.IsInfinity(LowerBound.Y) &&
                !float.IsInfinity(UpperBound.X) && !float.IsInfinity(UpperBound.Y);
        }
    }

    /// <summary>
    /// Gets the center of the AABB.
    /// </summary>
    public Vector2 Center => 0.5f * (LowerBound + UpperBound);

    /// <summary>
    /// Gets the extends of the AABB (half-widths).
    /// </summary>
    public Vector2 Extents => 0.5f * (UpperBound - LowerBound);

    /// <summary>
    /// Gets the perimeter length.
    /// </summary>
    public float Perimeter
    {
        get
        {
            var wx = UpperBound.X - LowerBound.X;
            var wy = UpperBound.Y - LowerBound.Y;
            return 2f * (wx + wy);
        }
    }

    /// <summary>
    /// Combines two AABBs.
    /// </summary>
    public static AABB Combine(AABB aabb1, AABB aabb2)
        => new()
        {
            LowerBound = Vector2.Min(aabb1.LowerBound, aabb2.LowerBound),
            UpperBound = Vector2.Max(aabb1.UpperBound, aabb2.UpperBound),
        };

    /// <summary>
    /// Returns whether this AABB contains the provided AABB.
    /// </summary>
    public bool Contains(AABB other)
    {
        var result = true;
        result = result && LowerBound.X <= other.LowerBound.X;
        result = result && LowerBound.Y <= other.LowerBound.Y;
        result = result && other.UpperBound.X <= UpperBound.X;
        result = result && other.UpperBound.Y <= UpperBound.Y;
        return result;
    }

    public static bool operator ==(AABB a, AABB b)
        => a.Equals(b);

    public static bool operator !=(AABB a, AABB b)
        => !a.Equals(b);

    /// <inheritdoc/>
    public bool Equals(AABB other)
        => LowerBound.Equals(other.LowerBound) && UpperBound.Equals(other.UpperBound);

    /// <inheritdoc/>
    public override bool Equals(object obj)
        => obj is AABB aabb && Equals(aabb);

    /// <inheritdoc/>
    public override int GetHashCode()
        => HashCode.Combine(LowerBound, UpperBound);
}
