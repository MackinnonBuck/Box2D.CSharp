using Box2D;
using Xunit;

namespace Tests;

public class WorldTests
{
    [Fact]
    public void Test1()
    {
        using var world = new World(new(0f, 10f));
        var body = world.CreateBody(new()
        {
            Position = new(0f, 10f)
        });
        var position = body.Position;
    }
}