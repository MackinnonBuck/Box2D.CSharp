using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Collision;

[StructLayout(LayoutKind.Sequential)]
public struct AABB
{
    public Vector2 LowerBound { get; set; }

    public Vector2 UpperBound { get; set; }

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

    public Vector2 Center => 0.5f * (LowerBound + UpperBound);

    public Vector2 Extents => 0.5f * (UpperBound - LowerBound);

    public float Perimeter
    {
        get
        {
            var wx = UpperBound.X - LowerBound.X;
            var wy = UpperBound.Y - LowerBound.Y;
            return 2f * (wx + wy);
        }
    }

    public void Combine(AABB other)
    {
        LowerBound = Vector2.Min(LowerBound, other.LowerBound);
        UpperBound = Vector2.Max(UpperBound, other.UpperBound);
    }

    public void Combine(AABB aabb1, AABB aabb2)
    {
        LowerBound = Vector2.Min(aabb1.LowerBound, aabb2.LowerBound);
        UpperBound = Vector2.Max(aabb1.UpperBound, aabb2.UpperBound);
    }

    public bool Contains(AABB other)
    {
        var result = true;
        result = result && LowerBound.X <= other.LowerBound.X;
        result = result && LowerBound.Y <= other.LowerBound.Y;
        result = result && other.UpperBound.X <= UpperBound.X;
        result = result && other.UpperBound.Y <= UpperBound.Y;
        return result;
    }
}
