using System;
using System.Runtime.InteropServices;

namespace Box2D.Math;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Vec2 : IEquatable<Vec2>
{
    public static Vec2 Zero { get; } = new(0f, 0f);

    public float X { get; set; }

    public float Y { get; set; }

    public float Length => MathF.Sqrt(X * X + Y * Y);

    public float LengthSquared => X * X + Y * Y;

    public bool IsValid => float.IsFinite(X) && float.IsFinite(Y);

    public Vec2 Skew => new(-Y, X);

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

    public void SetZero()
    {
        X = 0f;
        Y = 0f;
    }

    public void Set(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float Normalize()
    {
        var length = Length;

        if (length < float.Epsilon)
        {
            return 0f;
        }

        var invLength = 1f / length;
        X *= invLength;
        Y *= invLength;

        return length;
    }

    public float Dot(Vec2 other)
        => X * other.X + Y * other.Y;

    public static Vec2 operator +(Vec2 v)
        => v;

    public static Vec2 operator -(Vec2 v)
        => new(-v.X, -v.Y);

    public static Vec2 operator +(Vec2 a, Vec2 b)
        => new(a.X + b.X, a.Y + b.Y);

    public static Vec2 operator -(Vec2 a, Vec2 b)
        => a + -b;

    public static Vec2 operator *(Vec2 v, float a)
        => new(v.X * a, v.Y * a);

    public static Vec2 operator *(float a, Vec2 v)
        => v * a;

    public static Vec2 Min(Vec2 a, Vec2 b)
        => new(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y));

    public static Vec2 Max(Vec2 a, Vec2 b)
        => new(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y));

    public static float Dot(Vec2 a, Vec2 b)
        => a.Dot(b);

    private static IndexOutOfRangeException ComponentIndexOutOfRange(int i)
        => new($"The component index '{i}' is out of range for {nameof(Vec2)} instances.");

    public bool Equals(Vec2 other)
        => X == other.X && Y == other.Y;

    public override int GetHashCode()
        => HashCode.Combine(X, Y);
}

