using Box2D.Collision.Shapes;
using Box2D.Dynamics;

namespace Testbed.Tests;

[TestEntry("Forces", "Restitution")]
internal class Restitution : Test
{
    private const float Threshold = 10f;

    private static readonly float[] _restitution = new float[] { 0f, 0.1f, 0.3f, 0.5f, 0.75f, 0.9f, 1f };

    public Restitution()
    {
        {
            var ground = World.CreateBody();

            using var shape = EdgeShape.Create();
            shape.SetTwoSided(new(-40f, 0f), new(40f, 0f));

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.RestitutionThreshold = Threshold;
            ground.CreateFixture(fd);
        }

        {
            using var shape = CircleShape.Create();
            shape.Radius = 1f;

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.Density = 1f;
            fd.RestitutionThreshold = Threshold;

            for (var i = 0; i < _restitution.Length; i++)
            {
                var body = World.CreateBody(BodyType.Dynamic, new(-10f + 3f * i, 20f));
                fd.Restitution = _restitution[i];

                body.CreateFixture(fd);
            }
        }
    }
}
