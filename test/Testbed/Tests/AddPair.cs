﻿using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using System.Numerics;

namespace Testbed.Tests;

[TestEntry("Benchmark", "Add Pair")]
internal class AddPair : Test
{
    public AddPair()
    {
        World.Gravity = new(0f, 0f);

        {
            using var shape = new CircleShape
            {
                Position = new(0f, 0f),
                Radius = 0.1f,
            };

            var minX = -6f;
            var maxX = 0f;
            var minY = 4f;
            var maxY = 6f;

            for (var i = 0; i < 400; i++)
            {
                var position = new Vector2(MathUtils.RandomFloat(minX, maxX), MathUtils.RandomFloat(minY, maxY));
                var body = World.CreateBody(BodyType.Dynamic, position);
                body.CreateFixture(shape, 0.01f);
            }
        }

        {
            using var shape = new PolygonShape();
            shape.SetAsBox(1.5f, 1.5f);
            using var bd = new BodyDef
            {
                Type = BodyType.Dynamic,
                Position = new(-40f, 5f),
                Bullet = true,
            };
            var body = World.CreateBody(bd);
            body.CreateFixture(shape, 1f);
            body.LinearVelocity = new(10f, 0f);
        }
    }
}
