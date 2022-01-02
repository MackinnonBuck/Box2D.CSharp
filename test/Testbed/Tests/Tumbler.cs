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
            using var bd = BodyDef.Create();
            bd.Type = BodyType.Dynamic;
            bd.AllowSleep = false;
            bd.Position = new(0f, 10f);
            var body = World.CreateBody(bd);

            using var shape = PolygonShape.Create();
            shape.SetAsBox(0.5f, 10f, new(10f, 0f), 0f);
            body.CreateFixture(shape, 5f);
            shape.SetAsBox(0.5f, 10f, new(-10f, 0f), 0f);
            body.CreateFixture(shape, 5f);
            shape.SetAsBox(10f, 0.5f, new(0f, 10f), 0f);
            body.CreateFixture(shape, 5f);
            shape.SetAsBox(10f, 0.5f, new(0f, -10f), 0f);
            body.CreateFixture(shape, 5f);

            using var jd = RevoluteJointDef.Create();
            jd.BodyA = ground;
            jd.BodyB = body;
            jd.LocalAnchorA = new(0f, 10f);
            jd.LocalAnchorB = new(0f, 0f);
            jd.ReferenceAngle = 0f;
            jd.MotorSpeed = 0.05f * MathF.PI;
            jd.MaxMotorTorque = 1e8f;
            jd.EnableMotor = true;
            World.CreateJoint(jd);
        }

        _count = 0;
    }

    public override void Step()
    {
        base.Step();

        if (_count < 800)
        {
            var body = World.CreateBody(BodyType.Dynamic, new(0f, 10f));

            using var shape = PolygonShape.Create();
            shape.SetAsBox(0.125f, 0.125f);
            body.CreateFixture(shape, 1f);

            _count++;
        }
    }
}
