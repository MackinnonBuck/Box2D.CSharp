using System;
using System.Runtime.InteropServices;

namespace Box2D;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Vec2
{
    public static Vec2 Zero { get; } = new(0f, 0f);

    public float X { get; set; }
    public float Y { get; set; }

    public Vec2(float x, float y)
    {
        X = x;
        Y = y;
    }
}

