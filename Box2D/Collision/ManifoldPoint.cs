﻿using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Sequential)]
public struct ManifoldPoint
{
    public Vec2 LocalPoint { get; set; }

    public float NormalImpulse { get; set; }

    public float TangentImpulse { get; set; }

    public ContactFeature Id { get; set; }
}
