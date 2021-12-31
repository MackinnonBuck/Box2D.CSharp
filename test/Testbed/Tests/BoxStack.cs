﻿using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using Silk.NET.Input;
using System.Diagnostics;

namespace Testbed.Tests;

[TestEntry("Stacking", "Boxes")]
internal class BoxStack : Test
{
    private const int ColumnCount = 1;
    private const int RowCount = 15;

    private readonly Body[] _bodies = new Body[RowCount * ColumnCount];
    private readonly int[] _indices = new int[RowCount * ColumnCount];

    private Body? _bullet;

    public BoxStack()
    {
        {
            var ground = World.CreateBody();

            using var shape = new EdgeShape();
            shape.SetTwoSided(new(-40f, 0f), new(40f, 0f));
            ground.CreateFixture(shape, 0f);

            shape.SetTwoSided(new(20f, 0f), new(20f, 20f));
            ground.CreateFixture(shape, 0f);
        }

        {
            Span<float> xs = stackalloc float[] { 0f, -10f, -5f, 5f, 10f };

            using var shape = new PolygonShape();
            shape.SetAsBox(0.5f, 0.5f);

            using var fd = new FixtureDef
            {
                Shape = shape,
                Density = 1f,
                Friction = 0.3f,
            };

            for (var j = 0; j < ColumnCount; j++)
            {
                for (var i = 0; i < RowCount; i++)
                {
                    var n = j * RowCount + i;
                    Debug.Assert(n < RowCount * ColumnCount);
                    _indices[n] = n;

                    var x = 0f;
                    var body = World.CreateBody(BodyType.Dynamic, new(xs[j] + x, 0.55f + 1.1f * i));
                    body.UserData = n;

                    _bodies[n] = body;
                    body.CreateFixture(fd);
                }
            }
        }
    }

    public override void Keyboard(Key key)
    {
        switch (key)
        {
            case Key.Comma:
                if (_bullet is not null)
                {
                    World.DestroyBody(_bullet);
                    _bullet = null;
                }

                {
                    using var shape = new CircleShape();
                    shape.Radius = 0.25f;

                    using var fd = new FixtureDef
                    {
                        Shape = shape,
                        Density = 20f,
                        Restitution = 0.05f,
                    };

                    using var bd = new BodyDef
                    {
                        Type = BodyType.Dynamic,
                        Bullet = true,
                        Position = new(-31f, 5f),
                    };

                    _bullet = World.CreateBody(bd);
                    _bullet.CreateFixture(fd);
                    _bullet.LinearVelocity = new(400f, 0f);
                }
                break;
        }
    }

    public override void Step()
    {
        base.Step();

        DebugDraw.DrawString(5, TextLine, "Press: (,) to launch a bullet.");
        TextLine += TextIncrement;
    }
}
