using Box2D.Collision;
using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using ImGuiNET;
using System.Numerics;

namespace Testbed.Tests;

[TestEntry("Collision", "Sensors")]
internal class Sensor : Test
{
    private const int Count = 7;

    private readonly Fixture _sensor;
    private readonly Body[] _bodies = new Body[Count];
    private readonly bool[] _touching = new bool[Count];

    private float _force = 100f;

    public Sensor()
    {
        using var groundBd = new BodyDef();
        var ground = World.CreateBody(groundBd);

        {
            using var shape = new EdgeShape();
            shape.SetTwoSided(new(-40f, 0f), new(40f, 0f));
            ground.CreateFixture(shape, 0f);
        }

        {
            using var shape = new CircleShape
            {
                Radius = 5f,
                Position = new(0f, 10f),
            };

            using var fd = new FixtureDef
            {
                Shape = shape,
                IsSensor = true,
            };
            _sensor = ground.CreateFixture(fd);
        }

        {
            using var shape = new CircleShape
            {
                Radius = 1f,
            };

            using var bd = new BodyDef
            {
                Type = BodyType.Dynamic,
            };

            for (var i = 0; i < Count; i++)
            {
                bd.Position = new(-10f + 3f * i, 20f);
                bd.UserData = i;

                _touching[i] = false;
                _bodies[i] = World.CreateBody(bd);
                _bodies[i].CreateFixture(shape, 1f);
            }
        }
    }

    public override void BeginContact(in Contact contact)
    {
        var fixtureA = contact.FixtureA;
        var fixtureB = contact.FixtureB;

        if (fixtureA == _sensor)
        {
            if (fixtureB.Body.UserData is int index && index < Count)
            {
                _touching[index] = true;
            }
        }

        if (fixtureB == _sensor)
        {
            if (fixtureA.Body.UserData is int index && index < Count)
            {
                _touching[index] = true;
            }
        }
    }

    public override void EndContact(in Contact contact)
    {
        var fixtureA = contact.FixtureA;
        var fixtureB = contact.FixtureB;

        if (fixtureA == _sensor)
        {
            if (fixtureB.Body.UserData is int index && index < Count)
            {
                _touching[index] = false;
            }
        }

        if (fixtureB == _sensor)
        {
            if (fixtureA.Body.UserData is int index && index < Count)
            {
                _touching[index] = false;
            }
        }
    }

    public override void UpdateUI()
    {
        ImGui.SetNextWindowPos(new(10f, 100f));
        ImGui.SetNextWindowSize(new(200f, 60f));
        ImGui.Begin("Sensor Controls", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize);

        ImGui.SliderFloat("Force", ref _force, 0f, 2000f, "%.0f");

        ImGui.End();
    }

    public override void Step()
    {
        base.Step();

        for (var i = 0; i < Count; i++)
        {
            if (!_touching[i])
            {
                continue;
            }

            var body = _bodies[i];
            var ground = _sensor.Body;

            var circle = (CircleShape)_sensor.Shape;
            var center = ground.GetWorldPoint(circle.Position);
            var position = body.Position;
            var d = center - position;

            if (d.LengthSquared() < float.Epsilon * float.Epsilon)
            {
                continue;
            }

            d = Vector2.Normalize(d);
            var f = _force * d;
            body.ApplyForce(f, position, false);
        }
    }
}
