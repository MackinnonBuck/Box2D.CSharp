using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct Transform : IEquatable<Transform>
{
    private static readonly Transform _identity = new(Vec2.Zero, Rot.Identity);

    public static ref readonly Transform Identity => ref _identity;

    public Vec2 P { get; set; }

    public Rot Q { get; set; }

    public Transform(Vec2 position, Rot rotation)
    {
        P = position;
        Q = rotation;
    }

    public Transform(Vec2 position, float angle)
    {
        P = position;
        Q = new(angle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Transform a, Transform b)
        => a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Transform a, Transform b)
        => !a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
        => obj is Transform a && Equals(a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Transform other)
        => P.Equals(other.P) && Q.Equals(other.Q);

    public override int GetHashCode()
        => HashCode.Combine(P, Q);

    public override string ToString()
        => $"{P}, {Q}";
}
