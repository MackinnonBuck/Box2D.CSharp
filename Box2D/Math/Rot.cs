using System;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct Rot : IEquatable<Rot>
{
    public static readonly Rot Identity = new() { S = 0, C = 1 };

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

    public bool Equals(Rot other)
        => S == other.S && C == other.C;

    public override int GetHashCode()
        => HashCode.Combine(S, C);
}
