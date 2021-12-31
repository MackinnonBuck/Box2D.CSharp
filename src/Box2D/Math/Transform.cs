using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Math;

/// <summary>
/// Contains translation and rotation. Used to represent
/// the position and orientation of rigid frames.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Transform : IEquatable<Transform>
{
    private static readonly Transform _identity = new(Vector2.Zero, Rot.Identity);

    /// <summary>
    /// Gets the identity transform.
    /// </summary>
    public static ref readonly Transform Identity => ref _identity;

    /// <summary>
    /// Gets the position of the transform.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets the rotation of the transform.
    /// </summary>
    public Rot Rotation { get; set; }

    /// <summary>
    /// Constructs a new <see cref="Transform"/> instance.
    /// </summary>
    /// <param name="position">The postion.</param>
    /// <param name="rotation">The rotation.</param>
    public Transform(Vector2 position, Rot rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    /// <summary>
    /// Constructs a new <see cref="Transform"/> instance.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="angle">The angle of rotation in radians.</param>
    public Transform(Vector2 position, float angle)
    {
        Position = position;
        Rotation = new(angle);
    }

    /// <summary>
    /// Multiplies a transform by a vector.
    /// </summary>
    /// <param name="t">The transform.</param>
    /// <param name="v">The vector.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Mul(Transform t, Vector2 v)
    {
        var x = (t.Rotation.Cos * v.X - t.Rotation.Sin * v.Y) + t.Position.X;
        var y = (t.Rotation.Sin * v.X + t.Rotation.Cos * v.Y) + t.Position.Y;

        return new(x, y);
    }

    /// <summary>
    /// Transpose multiplies a transform by a vector.
    /// </summary>
    /// <param name="t">The transform.</param>
    /// <param name="v">The vector.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 MulT(Transform t, Vector2 v)
    {
        var px = v.X - t.Position.X;
        var py = v.Y - t.Position.Y;
        var x = (t.Rotation.Cos * px + t.Rotation.Sin * py);
        var y = (-t.Rotation.Sin * px + t.Rotation.Cos * py);

        return new(x, y);
    }

    /// <summary>
    /// Multiplies a transform by another transform.
    /// </summary>
    /// <param name="a">The first transform</param>
    /// <param name="b">The second transform.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Transform Mul(Transform a, Transform b)
        => new(Rot.Mul(a.Rotation, b.Position) + a.Position, Rot.Mul(a.Rotation, b.Rotation));

    /// <summary>
    /// Transpose multiplies a transform by another transform.
    /// </summary>
    /// <param name="a">The first transform</param>
    /// <param name="b">The second transform.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Transform MulT(Transform a, Transform b)
        => new(Rot.Mul(a.Rotation, b.Position) - a.Position, Rot.Mul(a.Rotation, b.Rotation));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Transform a, Transform b)
        => a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Transform a, Transform b)
        => !a.Equals(b);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
        => obj is Transform a && Equals(a);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Transform other)
        => Position.Equals(other.Position) && Rotation.Equals(other.Rotation);

    /// <inheritdoc/>
    public override int GetHashCode()
        => HashCode.Combine(Position, Rotation);

    /// <summary>
    /// Returns a string representation of the transform.
    /// </summary>
    public override string ToString()
        => $"{Position}, {Rotation}";
}
