using Box2D;

namespace Testbed.Tests;

[TestEntry("Forces", "Restitution")]
internal class Restitution : Test
{
    private const float Threshold = 10f;

    private static readonly float[] _restitution = new float[] { 0f, 0.1f, 0.3f, 0.5f, 0.75f, 0.9f, 1f };

    public Restitution()
    {
        {
            using var bd = new BodyDef();
            var ground = World.CreateBody(bd);

            using var shape = new EdgeShape();
            shape.SetTwoSided(new(-40f, 0f), new(40f, 0f));

            var fd = new FixtureDef
            {
                Shape = shape,
                RestitutionThreshold = Threshold,
            };
            ground.CreateFixture(fd);
        }

        {
            using var shape = new CircleShape();
            shape.Radius = 1f;

            var fd = new FixtureDef
            {
                Shape = shape,
                Density = 1f,
            };

            using var bd = new BodyDef
            {
                Type = BodyType.Dynamic,
            };

            for (var i = 0; i < _restitution.Length; i++)
            {
                bd.Position = new(-10f + 3f * i, 20f);

                var body = World.CreateBody(bd);

                body.CreateFixture(fd with
                {
                    Restitution = _restitution[i],
                    RestitutionThreshold = Threshold,
                });
            }
        }
    }
}
