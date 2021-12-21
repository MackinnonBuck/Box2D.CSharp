using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct AABB
{
    public Vec2 LowerBound { get; set; }

    public Vec2 UpperBound { get; set; }

    public bool IsValid
    {
        get
        {
            var d = UpperBound - LowerBound;
            return d.X >= 0f & d.Y >= 0f && LowerBound.IsValid && UpperBound.IsValid;
        }
    }

    public Vec2 Center => 0.5f * (LowerBound + UpperBound);

    public Vec2 Extents => 0.5f * (UpperBound - LowerBound);

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
        LowerBound = Vec2.Min(LowerBound, other.LowerBound);
        UpperBound = Vec2.Max(UpperBound, other.UpperBound);
    }

    public void Combine(AABB aabb1, AABB aabb2)
    {
        LowerBound = Vec2.Min(aabb1.LowerBound, aabb2.LowerBound);
        UpperBound = Vec2.Max(aabb1.UpperBound, aabb2.UpperBound);
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
