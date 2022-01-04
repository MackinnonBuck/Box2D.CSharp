using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using Box2D.Dynamics.Joints;
using Box2D.Math;
using Silk.NET.Input;
using System.Numerics;

namespace Testbed.Tests;

[TestEntry("Forces", "Apply Force")]
internal class ApplyForce : Test
{
    private const float Restitution = 0.4f;

    private readonly Body _body;

    private bool _isForward;
    private bool _isLeft;
    private bool _isRight;

    public ApplyForce()
    {
        World.Gravity = new(0f, 0f);

        var ground = World.CreateBody(position: new(0f, 20f));

        {
            using var shape = EdgeShape.Create();

            using var sd = FixtureDef.Create();
            sd.Shape = shape;
            sd.Density = 0f;
            sd.Restitution = Restitution;

            // Left vertical
            shape.SetTwoSided(new(-20f, -20f), new(-20f, 20f));
            ground.CreateFixture(sd);

            // Right vertical
            shape.SetTwoSided(new(20f, -20f), new(20f, 20f));
            ground.CreateFixture(sd);

            // Top horizontal
            shape.SetTwoSided(new(-20f, 20f), new(20f, 20f));
            ground.CreateFixture(sd);

            // Bottom horizontal
            shape.SetTwoSided(new(-20f, -20f), new(20f, -20f));
            ground.CreateFixture(sd);
        }

        {
            using var bd = BodyDef.Create();
            bd.Type = BodyType.Dynamic;
            bd.Position = new(0f, 3f);
            bd.Angle = MathF.PI;
            bd.AllowSleep = false;

            _body = World.CreateBody(bd);

            var xf1 = new Transform
            {
                Rotation = new(0.3524f * MathF.PI),
            };
            xf1.Position = xf1.Rotation.XAxis;

            Span<Vector2> vertices = stackalloc Vector2[]
            {
                Transform.Mul(xf1, new Vector2(-1f, 0f)),
                Transform.Mul(xf1, new Vector2(1f, 0f)),
                Transform.Mul(xf1, new Vector2(0f, 0.5f)),
            };

            using var poly = PolygonShape.Create();
            poly.Set(vertices);

            _body.CreateFixture(poly, 2f);

            var xf2 = new Transform
            {
                Rotation = new(-0.3524f * MathF.PI),
            };
            xf2.Position = -xf2.Rotation.XAxis;

            vertices[0] = Transform.Mul(xf2, new Vector2(-1f, 0f));
            vertices[1] = Transform.Mul(xf2, new Vector2(1f, 0f));
            vertices[2] = Transform.Mul(xf2, new Vector2(0f, 0.5f));

            poly.Set(vertices);

            _body.CreateFixture(poly, 2f);

            var gravity = 10f;
            var inertia = _body.Inertia;
            var mass = _body.Mass;

            var radius = MathF.Sqrt(2f * inertia / mass);

            using var jd = FrictionJointDef.Create();
            jd.BodyA = ground;
            jd.BodyB = _body;
            jd.LocalAnchorA = Vector2.Zero;
            jd.LocalAnchorB = _body.LocalCenter;
            jd.CollideConnected = true;
            jd.MaxForce = 0.5f * mass * gravity;
            jd.MaxTorque = 0.2f * mass * radius * gravity;

            World.CreateJoint(jd);
        }

        {
            using var shape = PolygonShape.Create();
            shape.SetAsBox(0.5f, 0.5f);

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.Density = 1f;
            fd.Friction = 0.3f;

            using var jd = FrictionJointDef.Create();
            jd.LocalAnchorA = Vector2.Zero;
            jd.LocalAnchorB = Vector2.Zero;
            jd.BodyA = ground;
            jd.CollideConnected = true;

            for (var i = 0; i < 10; i++)
            {
                var body = World.CreateBody(BodyType.Dynamic, new(0f, 7f + 1.54f * i));
                body.CreateFixture(fd);

                var gravity = 10f;
                var inertia = body.Inertia;
                var mass = body.Mass;

                var radius = MathF.Sqrt(2f * inertia / mass);
                jd.BodyB = body;
                jd.MaxForce = mass * gravity;
                jd.MaxTorque = 0.1f * mass * radius * gravity;

                World.CreateJoint(jd);
            }
        }
    }

    public override void Keyboard(Key key)
    {
        switch (key)
        {
            case Key.W:
                _isForward = true;
                break;
            case Key.A:
                _isLeft = true;
                break;
            case Key.D:
                _isRight = true;
                break;
        }
    }

    public override void KeyboardUp(Key key)
    {
        switch (key)
        {
            case Key.W:
                _isForward = false;
                break;
            case Key.A:
                _isLeft = false;
                break;
            case Key.D:
                _isRight = false;
                break;
        }
    }

    public override void Step()
    {
        DebugDraw.DrawString(5, TextLine, "Forward (W), Turn (A) and (D)");
        TextLine += TextIncrement;

        if (_isForward)
        {
            var f = _body.GetWorldVector(new(0f, -50f));
            var p = _body.GetWorldPoint(new(0f, 3f));
            _body.ApplyForce(f, p, true);
        }

        if (_isLeft)
        {
            _body.ApplyTorque(10f, true);
        }

        if (_isRight)
        {
            _body.ApplyTorque(-10f, true);
        }

        base.Step();
    }
}
