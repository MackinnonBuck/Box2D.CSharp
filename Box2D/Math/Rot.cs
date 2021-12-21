using System;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct Rot : IEquatable<Rot>
{
    public float S { get; set; }
    public float C { get; set; }

    public float Angle => MathF.Atan2(S, C);

    public Vec2 XAxis => new(C, S);

    public Vec2 YAxis => new(-S, C);

    public Rot(float angle)
    {
        S = MathF.Sin(angle);
        C = MathF.Cos(angle);
    }

    public void Set(float angle)
    {
        S = MathF.Sin(angle);
        C = MathF.Cos(angle);
    }

    public void SetIdentity()
    {
        S = 0f;
        C = 1f;
    }

    public bool Equals(Rot other)
        => S == other.S && C == other.C;

    public override int GetHashCode()
        => HashCode.Combine(S, C);
}
