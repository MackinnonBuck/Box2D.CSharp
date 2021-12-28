using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Math;

[StructLayout(LayoutKind.Sequential)]
public struct Rot : IEquatable<Rot>
{
    private static readonly Rot _identity = new() { S = 0, C = 1 };

    public static ref readonly Rot Identity => ref _identity;

    public float S { get; set; }

    public float C { get; set; }

    public float Angle => (float)System.Math.Atan2(S, C);

    public Vector2 XAxis => new(C, S);

    public Vector2 YAxis => new(-S, C);

    public Rot(float angle)
    {
        S = (float)System.Math.Sin(angle);
        C = (float)System.Math.Cos(angle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rot a, Rot b)
        => a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rot a, Rot b)
        => !a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
        => obj is Rot a && Equals(a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Rot other)
        => S == other.S && C == other.C;

    public override int GetHashCode()
        => HashCode.Combine(S, C);

    public override string ToString()
        => $"{Angle}rad";
}
