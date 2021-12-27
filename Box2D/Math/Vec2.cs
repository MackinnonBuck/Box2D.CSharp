using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct Vec2 : IEquatable<Vec2>
{
    private static readonly Vec2 _zero = new(0f, 0f);
    private static readonly Vec2 _unitX = new(1f, 0f);
    private static readonly Vec2 _unitY = new(0f, 1f);

    public static ref readonly Vec2 Zero => ref _zero;

    public static ref readonly Vec2 UnitX => ref _unitX;

    public static ref readonly Vec2 UnitY => ref _unitY;

    public float X { get; set; }

    public float Y { get; set; }

    public float Length => MathF.Sqrt(X * X + Y * Y);

    public float LengthSquared => X * X + Y * Y;

    public bool IsValid => float.IsFinite(X) && float.IsFinite(Y);

    public Vec2 Skew => new(-Y, X);

    public Vec2 Normalized
    {
        get
        {
            var length = Length;

            if (length < float.Epsilon)
            {
                return Zero;
            }

            var invLength = 1f / length;
            return new(X * invLength, Y * invLength);
        }
    }

    public float this[int i]
    {
        get => i switch
        {
            0 => X,
            1 => Y,
            _ => throw ComponentIndexOutOfRange(i)
        };
        set
        {
            switch (i)
            {
                case 0:
                    X = value;
                    break;
                case 1:
                    Y = value;
                    break;
                default:
                    throw ComponentIndexOutOfRange(i);
            };
        }
    }

    public Vec2(float x, float y)
    {
        X = x;
        Y = y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Dot(Vec2 other)
        => X * other.X + Y * other.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Cross(Vec2 other)
        => X * other.Y - Y * other.X;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vec2 Cross(float s)
        => new(s * Y, -s * X);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 operator +(Vec2 v)
        => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 operator -(Vec2 v)
        => new(-v.X, -v.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 operator +(Vec2 a, Vec2 b)
        => new(a.X + b.X, a.Y + b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 operator -(Vec2 a, Vec2 b)
        => a + -b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 operator *(Vec2 v, float a)
        => new(v.X * a, v.Y * a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 operator *(float a, Vec2 v)
        => v * a;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 Min(Vec2 a, Vec2 b)
        => new(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 Max(Vec2 a, Vec2 b)
        => new(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Dot(Vec2 a, Vec2 b)
        => a.X * b.X + a.Y * b.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cross(Vec2 a, Vec2 b)
        => a.X * b.Y - a.Y * b.X;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 Cross(Vec2 a, float s)
        => new(s * a.Y, -s * a.X);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec2 Cross(float s, Vec2 a)

        => new(-s * a.Y, s * a.X);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vec2 a, Vec2 b)
        => a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vec2 a, Vec2 b)
        => !a.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vec2 other)
        => X == other.X && Y == other.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
        => obj is Vec2 a && Equals(a);

    public override int GetHashCode()
        => HashCode.Combine(X, Y);

    public override string ToString()
        => $"<{X}, {Y}>";

    private static IndexOutOfRangeException ComponentIndexOutOfRange(int i)
        => new($"The component index '{i}' is out of range for {nameof(Vec2)} instances.");
}

