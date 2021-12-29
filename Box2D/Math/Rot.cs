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
