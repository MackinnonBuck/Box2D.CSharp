using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Math;

/// <summary>
/// Rotation.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Rot : IEquatable<Rot>
{
    private static readonly Rot _identity = new() { Sin = 0, Cos = 1 };

    /// <summary>
    /// Gets the identity rotation.
    /// </summary>
    public static ref readonly Rot Identity => ref _identity;

    /// <summary>
    /// Gets the sine value.
    /// </summary>
    public float Sin { get; set; }

    /// <summary>
    /// Gets the cosine value.
    /// </summary>
    public float Cos { get; set; }

    /// <summary>
    /// Gets the angle in radians.
    /// </summary>
    public float Angle => (float)System.Math.Atan2(Sin, Cos);

    /// <summary>
    /// Gets the x-axis.
    /// </summary>
    public Vector2 XAxis => new(Cos, Sin);

    /// <summary>
    /// Gets the y-axis.
    /// </summary>
    public Vector2 YAxis => new(-Sin, Cos);

    /// <summary>
    /// Constructs a new <see cref="Rot"/> instance.
    /// </summary>
    /// <param name="angle">The angle in radians.</param>
    public Rot(float angle)
    {
        Sin = (float)System.Math.Sin(angle);
        Cos = (float)System.Math.Cos(angle);
    }

    /// <summary>
    /// Constructs a new <see cref="Rot"/> instance.
    /// </summary>
    /// <param name="sin">The sine value.</param>
    /// <param name="cos">The cosine value.</param>
    public Rot(float sin, float cos)
    {
        Sin = sin;
        Cos = cos;
    }

    /// <summary>
    /// Multiplies two rotations.
    /// </summary>
    /// <param name="q">The first rotation.</param>
    /// <param name="r">The second rotation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rot Mul(Rot q, Rot r)
        => new(q.Sin * r.Cos + q.Cos * r.Sin, q.Cos * r.Cos - q.Sin * r.Sin);

    /// <summary>
    /// Transpose multiplies two rotations.
    /// </summary>
    /// <param name="q">The first rotation.</param>
    /// <param name="r">The second rotation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rot MulT(Rot q, Rot r)
        => new(q.Cos * r.Sin - q.Sin * r.Cos, q.Cos * r.Cos + q.Sin * r.Sin);

    /// <summary>
    /// Rotates a vector.
    /// </summary>
    /// <param name="q">The rotation.</param>
    /// <param name="v">The vector.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Mul(Rot q, Vector2 v)
        => new(q.Cos * v.X - q.Sin * v.Y, q.Sin * v.X + q.Cos * v.Y);

    /// <summary>
    /// Inverse rotates a vector.
    /// </summary>
    /// <param name="q">The rotation.</param>
    /// <param name="v">The vector.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 MulT(Rot q, Vector2 v)
        => new(q.Cos * v.X + q.Sin * v.Y, -q.Sin * v.X + q.Cos * v.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rot a, Rot b)
        => a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rot a, Rot b)
        => !a.Equals(b);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
        => obj is Rot a && Equals(a);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Rot other)
        => Sin == other.Sin && Cos == other.Cos;

    /// <inheritdoc/>
    public override int GetHashCode()
        => HashCode.Combine(Sin, Cos);

    /// <summary>
    /// Returns the string representation of the rotation.
    /// </summary>
    public override string ToString()
        => $"{Angle}rad";
}
