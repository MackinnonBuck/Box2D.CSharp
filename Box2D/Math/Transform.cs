using System;
using System.Runtime.InteropServices;

namespace Box2D.Math;

[StructLayout(LayoutKind.Sequential)]
public struct Transform : IEquatable<Transform>
{
    public Vec2 P { get; set; }
    public Rot Q { get; set; }

    public Transform(ref Vec2 position, ref Rot rotation)
    {
        P = position;
        Q = rotation;
    }

    public void Set(ref Vec2 position, float angle)
    {
        P = position;
        Q.Set(angle);
    }

    public void SetIdentity()
    {
        P.SetZero();
        Q.SetIdentity();
    }

    public bool Equals(Transform other)
        => P.Equals(other.P) && Q.Equals(other.Q);

    public override int GetHashCode()
        => HashCode.Combine(P, Q);
}
