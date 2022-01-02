using Box2D.Collision.Shapes;
using Box2D.Core;
using Box2D.Dynamics;
using System.Diagnostics;
using System.Numerics;

namespace Testbed.Tests;

[TestEntry("Benchmark", "Tiles")]
internal class Tiles : Test
{
    private readonly int _fixtureCount = 0;
    private readonly float _createTime;

    public Tiles()
    {
        var stopwatch = Stopwatch.StartNew();

        using var bodyDef = BodyDef.Create();
        bodyDef.Angle = 3f;

        {
            var a = 0.5f;
            var ground = World.CreateBody(position: new(0f, -a));

            var n = 200;
            var m = 10;
            var position = Vector2.Zero;
            using var shape = PolygonShape.Create();
            for (var j = 0; j < m; j++)
            {
                position.X = -n * a;
                for (var i = 0; i < n; i++)
                {
                    shape.SetAsBox(a, a, position, 0f);
                    ground.CreateFixture(shape, 0f);
                    _fixtureCount++;
                    position.X += 2f * a;
                }
                position.Y -= 2f * a;
            }
        }

        {
            var a = 0.5f;
            using var shape = PolygonShape.Create();
            shape.SetAsBox(a, a);

            var x = new Vector2(-7f, 0.75f);
            var y = Vector2.Zero;
            var deltaX = new Vector2(0.5625f, 1.25f);
            var deltaY = new Vector2(1.125f, 0f);

            for (var i = 0; i < 20; i++)
            {
                y = x;

                for (var j = i; j < 20; j++)
                {
                    var body = World.CreateBody(BodyType.Dynamic, y);
                    body.CreateFixture(shape, 5f);
                    _fixtureCount++;
                    y += deltaY;
                }

                x += deltaX;
            }
        }

        _createTime = (float)stopwatch.Elapsed.TotalMilliseconds;
    }

    public override void Step()
    {
        var height = World.TreeHeight;
        var leafCount = World.ProxyCount;
        var minimumNodeCount = 2 * leafCount - 1;
        var minimumHeight = (int)MathF.Ceiling(MathF.Log(minimumNodeCount) / MathF.Log(2f));

        DebugDraw.DrawString(5, TextLine, $"dynamic tree height = {height}, min = {minimumHeight}");
        TextLine += TextIncrement;

        base.Step();

        DebugDraw.DrawString(5, TextLine, $"create time = {_createTime} ms, fixture count = {_fixtureCount}");
        TextLine += TextIncrement;
    }
}
