using Box2D;
using Box2D.Math;
using Xunit;

namespace Tests;

public class UnitTests
{
    [Fact]
    public void HelloWorld_Works()
    {
        var gravity = new Vec2(0f, -10f);

        using var world = new World(gravity);

        var groundBodyDef = new BodyDef
        {
            Position = new(0f, -10f),
        };

        var groundBody = world.CreateBody(groundBodyDef);

        var groundBox = new PolygonShape();

        groundBox.SetAsBox(50f, 10f);

        // TODO
        //groundBody->CreateFixture(groundBox, 0.0f);
    }
}
