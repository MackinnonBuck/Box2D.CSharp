using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using Box2D.Dynamics.Joints;
using Silk.NET.Input;

namespace Testbed.Tests;

[TestEntry("Examples", "Body Types")]
internal class BodyTypes : Test
{
    private const float Speed = 3f;

    private readonly Body _attachmenet;
    private readonly Body _platform;

    public BodyTypes()
    {
        var ground = World.CreateBody();

        {
            using var shape = EdgeShape.Create();
            shape.SetTwoSided(new(-20f, 0f), new(20f, 0f));

            ground.CreateFixture(shape, 0f);
        }

        // Define attachment
        {
            _attachmenet = World.CreateBody(BodyType.Dynamic, new(0f, 3f));

            using var shape = PolygonShape.Create();
            shape.SetAsBox(0.5f, 2f);
            _attachmenet.CreateFixture(shape, 2f);
        }

        // Define platform
        {
            _platform = World.CreateBody(BodyType.Dynamic, new(-4f, 5f));

            using var shape = PolygonShape.Create();
            shape.SetAsBox(0.5f, 4f, new(4f, 0f), 0.5f * MathF.PI);

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.Friction = 0.6f;
            fd.Density = 2f;
            _platform.CreateFixture(fd);

            using var rjd = RevoluteJointDef.Create();
            rjd.Initialize(_attachmenet, _platform, new(0f, 5f));
            rjd.MaxMotorTorque = 50f;
            rjd.EnableMotor = true;
            World.CreateJoint(rjd);

            using var pjd = PrismaticJointDef.Create();
            pjd.Initialize(ground, _platform, new(0f, 5f), new(1f, 0f));
            pjd.MaxMotorForce = 1000f;
            pjd.EnableMotor = true;
            pjd.LowerTranslation = -10f;
            pjd.UpperTranslation = 10f;
            pjd.EnableLimit = true;

            World.CreateJoint(pjd);
        }

        // Create a payload
        {
            var body = World.CreateBody(BodyType.Dynamic, new(0f, 8f));

            using var shape = PolygonShape.Create();
            shape.SetAsBox(0.75f, 0.75f);

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.Friction = 0.6f;
            fd.Density = 2f;

            body.CreateFixture(fd);
        }
    }

    public override void Keyboard(Key key)
    {
        switch (key)
        {
            case Key.D:
                _platform.Type = BodyType.Dynamic;
                break;

            case Key.S:
                _platform.Type = BodyType.Static;
                break;

            case Key.K:
                _platform.Type = BodyType.Kinematic;
                _platform.LinearVelocity = new(-Speed, 0f);
                _platform.AngularVelocity = 0f;
                break;
        }
    }

    public override void Step()
    {
        if (_platform.Type == BodyType.Kinematic)
        {
            var p = _platform.Position;
            var v = _platform.LinearVelocity;

            if ((p.X < -10f && v.X < 0f) ||
                (p.X > 10f && v.X > 0f))
            {
                v.X = -v.X;
                _platform.LinearVelocity = v;
            }
        }

        base.Step();

        DebugDraw.DrawString(5, TextLine, "Keys: (d) dynamic, (s) static, (k) kinematic");
        TextLine += TextIncrement;
    }
}
