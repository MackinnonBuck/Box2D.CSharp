using Box2D;
using Silk.NET.Input;

namespace Testbed.Tests;

[TestEntry("Examples", "Car")]
internal class Car : Test
{
    private static readonly float _speed = 50f;

    private readonly Body _car;
    private readonly Body _wheel1;
    private readonly Body _wheel2;

    private readonly WheelJoint _spring1;
    private readonly WheelJoint _spring2;

    public Car()
    {
        var ground = World.CreateBody(new());

        {
            using var shape = new EdgeShape();

            var fd = new FixtureDef
            {
                Shape = shape,
                Density = 0f,
                Friction = 0.6f,
            };

            shape.SetTwoSided(new(-20f, 0f), new(20f, 0f));
            ground.CreateFixture(fd);

            Span<float> hs = stackalloc float[] { 0.25f, 1f, 4f, 0f, 0f, -1f, -2f, -2f, -1.25f, 0f };
            var x = 20f;
            var y1 = 0f;
            var dx = 5f;

            for (var i = 0; i < 10; i++)
            {
                var y2 = hs[i];
                shape.SetTwoSided(new(x, y1), new(x + dx, y2));
                ground.CreateFixture(fd);
                y1 = y2;
                x += dx;
            }

            for (var i = 0; i < 10; i++)
            {
                var y2 = hs[i];
                shape.SetTwoSided(new(x, y1), new(x + dx, y2));
                ground.CreateFixture(fd);
                y1 = y2;
                x += dx;
            }

            shape.SetTwoSided(new(x, 0f), new(x + 40f, 0f));
            ground.CreateFixture(fd);

            x += 80f;
            shape.SetTwoSided(new(x, 0f), new(x + 40f, 0f));
            ground.CreateFixture(fd);

            x += 40f;
            shape.SetTwoSided(new(x, 0f), new(x + 10f, 5f));
            ground.CreateFixture(fd);

            x += 20f;
            shape.SetTwoSided(new(x, 0f), new(x + 40f, 0f));
            ground.CreateFixture(fd);

            x += 40f;
            shape.SetTwoSided(new(x, 0f), new(x, 20f));
            ground.CreateFixture(fd);
        }

        // Teeter
        {
            var bd = new BodyDef
            {
                Position = new(140f, 1f),
                Type = BodyType.Dynamic,
            };
            var body = World.CreateBody(bd);

            using var box = new PolygonShape();
            box.SetAsBox(10f, 0.25f);
            body.CreateFixture(box, 1f);

            using var jd = new RevoluteJointDef();
            jd.Initialize(ground, body, body.Position);
            jd.LowerAngle = -8f * MathF.PI / 180f;
            jd.UpperAngle = 8f * MathF.PI / 180f;
            jd.EnableLimit = true;
            World.CreateJoint(jd);

            body.ApplyAngularImpulse(100f, true);
        }

        // Bridge
        {
            var n = 20;
            using var shape = new PolygonShape();
            shape.SetAsBox(1f, 0.125f);

            var fd = new FixtureDef
            {
                Shape = shape,
                Density = 1f,
                Friction = 0.6f,
            };

            using var jd = new RevoluteJointDef();
            Vec2 anchor;

            var prevBody = ground;
            for (var i = 0; i < n; i++)
            {
                var bd = new BodyDef
                {
                    Type = BodyType.Dynamic,
                    Position = new(161f + 2f * i, -0.125f),
                };
                var body = World.CreateBody(bd);
                body.CreateFixture(fd);

                anchor = new Vec2(160f + 2f * i, -0.125f);
                jd.Initialize(prevBody, body, anchor);
                World.CreateJoint(jd);

                prevBody = body;
            }

            anchor = new Vec2(160f + 2f * n, -0.125f);
            jd.Initialize(prevBody, ground, anchor);
            World.CreateJoint(jd);
        }

        // Boxes
        {
            using var box = new PolygonShape();
            box.SetAsBox(0.5f, 0.5f);

            var bd = new BodyDef
            {
                Type = BodyType.Dynamic,
            };

            var body = World.CreateBody(bd with { Position = new(230f, 0.5f) });
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(bd with { Position = new(230f, 1.5f) });
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(bd with { Position = new(230f, 2.5f) });
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(bd with { Position = new(230f, 3.5f) });
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(bd with { Position = new(230f, 4.5f) });
            body.CreateFixture(box, 0.5f);
        }

        // Car
        {
            using var chassis = new PolygonShape();
            Span<Vec2> vertices = stackalloc Vec2[]
            {
                new(-1.5f, -0.5f),
                new(1.5f, -0.5f),
                new(1.5f, 0f),
                new(0f, 0.9f),
                new(-1.15f, 0.9f),
                new(-1.5f, 0.2f)
            };
            chassis.Set(vertices);

            using var circle = new CircleShape
            {
                Radius = 0.4f,
            };

            var bd = new BodyDef
            {
                Type = BodyType.Dynamic,
                Position = new(0f, 1f),
            };
            _car = World.CreateBody(bd);
            _car.CreateFixture(chassis, 1f);

            var fd = new FixtureDef
            {
                Shape = circle,
                Density = 1f,
                Friction = 0.9f,
            };

            _wheel1 = World.CreateBody(bd with { Position = new(-1f, 0.35f) });
            _wheel1.CreateFixture(fd);

            _wheel2 = World.CreateBody(bd with { Position = new(1f, 0.4f) });
            _wheel2.CreateFixture(fd);

            using var jd = new WheelJointDef();
            var axis = new Vec2(0f, 1f);

            var mass1 = _wheel1.Mass;
            var mass2 = _wheel2.Mass;

            var hertz = 4f;
            var dampingRatio = 0.7f;
            var omega = 2f * MathF.PI * hertz;

            jd.Initialize(_car, _wheel1, _wheel1.Position, axis);
            jd.MotorSpeed = 0f;
            jd.MaxMotorTorque = 20f;
            jd.EnableMotor = true;
            jd.Stiffness = mass1 * omega * omega;
            jd.Damping = 2f * mass1 * dampingRatio * omega;
            jd.LowerTranslation = -0.25f;
            jd.UpperTranslation = 0.25f;
            jd.EnableLimit = true;
            _spring1 = (WheelJoint)World.CreateJoint(jd);

            jd.Initialize(_car, _wheel2, _wheel2.Position, axis);
            jd.MotorSpeed = 0f;
            jd.MaxMotorTorque = 10f;
            jd.EnableMotor = false;
            jd.Stiffness = mass2 * omega * omega;
            jd.Damping = 2f * mass2 * dampingRatio * omega;
            jd.LowerTranslation = -0.25f;
            jd.UpperTranslation = 0.25f;
            jd.EnableLimit = true;
            _spring2 = (WheelJoint)World.CreateJoint(jd);
        }
    }

    public override void Keyboard(Key key)
    {
        switch (key)
        {
            case Key.A:
                _spring1.MotorSpeed = _speed;
                break;

            case Key.S:
                _spring1.MotorSpeed = 0f;
                break;

            case Key.D:
                _spring1.MotorSpeed = -_speed;
                break;
        }
    }

    public override void Step()
    {
        DebugDraw.DrawString(5, TextLine, "Keys: left = a, brake = s, right = d, hz down = q, hz up = e");
        TextLine += TextIncrement;

        Camera.Center = Camera.Center with { X = _car.Position.X };

        base.Step();
    }
}
