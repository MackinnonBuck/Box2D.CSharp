using System.Runtime.CompilerServices;

namespace System.Numerics;

public static class Vector2Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cross(this Vector2 a, Vector2 b)
        => a.X * b.Y - a.Y * b.X;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Cross(this Vector2 a, float b)
        => new(b * a.Y, -b * a.X);
}
