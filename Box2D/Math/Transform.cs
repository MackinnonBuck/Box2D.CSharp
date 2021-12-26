using System;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct Transform : IEquatable<Transform>
{
    public static readonly Transform Identity = new(Vec2.Zero, Rot.Identity);

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

    public bool Equals(Transform other)
        => P.Equals(other.P) && Q.Equals(other.Q);

    public override int GetHashCode()
        => HashCode.Combine(P, Q);
}
