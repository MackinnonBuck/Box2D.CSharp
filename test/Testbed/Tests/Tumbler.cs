using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using Box2D.Dynamics.Joints;

namespace Testbed.Tests;

[TestEntry("Benchmark", "Tumbler")]
internal class Tumbler : Test
{
    private int _count;

    public Tumbler()
    {
        var ground = World.CreateBody();

        {
            using var bd = new BodyDef
            {
                Type = BodyType.Dynamic,
                AllowSleep = false,
                Position = new(0f, 10f),
            };
            var body = World.CreateBody(bd);

            using var shape = new PolygonShape();
            shape.SetAsBox(0.5f, 10f, new(10f, 0f), 0f);
            body.CreateFixture(shape, 5f);
            shape.SetAsBox(0.5f, 10f, new(-10f, 0f), 0f);
            body.CreateFixture(shape, 5f);
            shape.SetAsBox(10f, 0.5f, new(0f, 10f), 0f);
            body.CreateFixture(shape, 5f);
            shape.SetAsBox(10f, 0.5f, new(0f, -10f), 0f);
            body.CreateFixture(shape, 5f);

            using var jd = new RevoluteJointDef
            {
                BodyA = ground,
                BodyB = body,
                LocalAnchorA = new(0f, 10f),
                LocalAnchorB = new(0f, 0f),
                ReferenceAngle = 0f,
                MotorSpeed = 0.05f * MathF.PI,
                MaxMotorTorque = 1e8f,
                EnableMotor = true,
            };
            World.CreateJoint(jd);
        }

        _count = 0;
    }

    public override void Step()
    {
        base.Step();

        if (_count < 800)
        {
            using var bd = new BodyDef
            {
                Type = BodyType.Dynamic,
                Position = new(0f, 10f),
            };
            var body = World.CreateBody(bd);

            using var shape = new PolygonShape();
            shape.SetAsBox(0.125f, 0.125f);
            body.CreateFixture(shape, 1f);

            _count++;
        }
    }
}
