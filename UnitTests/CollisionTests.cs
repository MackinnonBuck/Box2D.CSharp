using Box2D;
using System;
using Xunit;

namespace UnitTests;

public class CollisionTests
{
    [Fact]
    public void CollisionTest_Works()
    {
        var center = new Vec2(100f, -50f);
        var hx = 0.5f;
        var hy = 1.5f;
        var angle1 = 0.25f;

        using var polygon1 = new PolygonShape();
        polygon1.SetAsBox(hx, hy, center, angle1);

        var epsilon = 1.192092896e-07f;
        var absTol = 2f * epsilon;
        var relTol = 2f * epsilon;

        Assert.True(MathF.Abs(polygon1.Centroid.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(polygon1.Centroid.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));

        Span<Vec2> vertices = stackalloc Vec2[]
        {
            new Vec2(center.X - hx, center.Y - hy),
            new Vec2(center.X + hx, center.Y - hy),
            new Vec2(center.X - hx, center.Y + hy),
            new Vec2(center.X + hx, center.Y + hy),
        };

        using var polygon2 = new PolygonShape();
        polygon2.Set(vertices);

        Assert.True(MathF.Abs(polygon2.Centroid.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(polygon2.Centroid.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));

        var mass = 4f * hx * hy;
        var inertia = (mass / 3f) * (hx * hx + hy * hy) + mass * center.Dot(center);

        polygon1.ComputeMass(out var massData1, 1f);

        Assert.True(MathF.Abs(massData1.Center.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(massData1.Center.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));
        Assert.True(MathF.Abs(massData1.Mass - mass) < 20f * (absTol + relTol * mass));
        Assert.True(MathF.Abs(massData1.I - inertia) < 40f * (absTol + relTol * inertia));

        polygon2.ComputeMass(out var massData2, 1f);

        Assert.True(MathF.Abs(massData2.Center.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(massData2.Center.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));
        Assert.True(MathF.Abs(massData2.Mass - mass) < 20f * (absTol + relTol * mass));
        Assert.True(MathF.Abs(massData2.I - inertia) < 40f * (absTol + relTol * inertia));
    }
}
