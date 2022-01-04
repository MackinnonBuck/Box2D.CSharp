using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using Box2D.Dynamics.Joints;
using Silk.NET.Input;
using System.Numerics;

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
        var ground = World.CreateBody();

        {
            using var shape = EdgeShape.Create();

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.Density = 0f;
            fd.Friction = 0.6f;

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
            var body = World.CreateBody(BodyType.Dynamic, new(140f, 1f));

            using var box = PolygonShape.Create();
            box.SetAsBox(10f, 0.25f);
            body.CreateFixture(box, 1f);

            using var jd = RevoluteJointDef.Create();
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
            using var shape = PolygonShape.Create();
            shape.SetAsBox(1f, 0.125f);

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.Density = 1f;
            fd.Friction = 0.6f;

            using var jd = RevoluteJointDef.Create();
            Vector2 anchor;

            var prevBody = ground;
            for (var i = 0; i < n; i++)
            {
                var position = new Vector2(161f + 2f * i, -0.125f);
                var body = World.CreateBody(BodyType.Dynamic, position);
                body.CreateFixture(fd);

                anchor = new Vector2(160f + 2f * i, -0.125f);
                jd.Initialize(prevBody, body, anchor);
                World.CreateJoint(jd);

                prevBody = body;
            }

            anchor = new Vector2(160f + 2f * n, -0.125f);
            jd.Initialize(prevBody, ground, anchor);
            World.CreateJoint(jd);
        }

        // Boxes
        {
            using var box = PolygonShape.Create();
            box.SetAsBox(0.5f, 0.5f);

            var body = World.CreateBody(BodyType.Dynamic, new(230f, 0.5f));
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(BodyType.Dynamic, new(230f, 1.5f));
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(BodyType.Dynamic, new(230f, 2.5f));
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(BodyType.Dynamic, new(230f, 3.5f));
            body.CreateFixture(box, 0.5f);

            body = World.CreateBody(BodyType.Dynamic, new(230f, 4.5f));
            body.CreateFixture(box, 0.5f);
        }

        // Car
        {
            using var chassis = PolygonShape.Create();
            Span<Vector2> vertices = stackalloc Vector2[]
            {
                new(-1.5f, -0.5f),
                new(1.5f, -0.5f),
                new(1.5f, 0f),
                new(0f, 0.9f),
                new(-1.15f, 0.9f),
                new(-1.5f, 0.2f)
            };
            chassis.Set(vertices);

            using var circle = CircleShape.Create();
            circle.Radius = 0.4f;

            _car = World.CreateBody(BodyType.Dynamic, new(0f, 1f));
            _car.CreateFixture(chassis, 1f);

            using var fd = FixtureDef.Create();
            fd.Shape = circle;
            fd.Density = 1f;
            fd.Friction = 0.9f;

            _wheel1 = World.CreateBody(BodyType.Dynamic, new(-1f, 0.35f));
            _wheel1.CreateFixture(fd);

            _wheel2 = World.CreateBody(BodyType.Dynamic, new(1f, 0.4f));
            _wheel2.CreateFixture(fd);

            using var jd = WheelJointDef.Create();
            var axis = new Vector2(0f, 1f);

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
