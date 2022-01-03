using Box2D.Collision.Shapes;
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

    private Body _bullet;

    public BoxStack()
    {
        {
            var ground = World.CreateBody();

            using var shape = EdgeShape.Create();
            shape.SetTwoSided(new(-40f, 0f), new(40f, 0f));
            ground.CreateFixture(shape, 0f);

            shape.SetTwoSided(new(20f, 0f), new(20f, 20f));
            ground.CreateFixture(shape, 0f);
        }

        {
            Span<float> xs = stackalloc float[] { 0f, -10f, -5f, 5f, 10f };

            using var shape = PolygonShape.Create();
            shape.SetAsBox(0.5f, 0.5f);

            using var fd = FixtureDef.Create();
            fd.Shape = shape;
            fd.Density = 1f;
            fd.Friction = 0.3f;

            for (var j = 0; j < ColumnCount; j++)
            {
                for (var i = 0; i < RowCount; i++)
                {
                    var n = j * RowCount + i;
                    Debug.Assert(n < RowCount * ColumnCount);
                    _indices[n] = n;

                    var x = 0f;
                    using var bd = BodyDef.Create();
                    bd.Type = BodyType.Dynamic;
                    bd.Position = new(xs[j] + x, 0.55f + 1.1f * i);
                    bd.UserData = n;
                    var body = World.CreateBody(bd);

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
                if (!_bullet.IsNull)
                {
                    World.DestroyBody(_bullet);
                    _bullet = default;
                }

                {
                    using var shape = CircleShape.Create();
                    shape.Radius = 0.25f;

                    using var fd = FixtureDef.Create();
                    fd.Shape = shape;
                    fd.Density = 20f;
                    fd.Restitution = 0.05f;

                    using var bd = BodyDef.Create();
                    bd.Type = BodyType.Dynamic;
                    bd.Bullet = true;
                    bd.Position = new(-31f, 5f);

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
