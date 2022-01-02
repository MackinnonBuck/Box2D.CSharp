using Box2D.Collision.Shapes;
using Box2D.Dynamics;
using System.Numerics;
using Xunit;

namespace UnitTests;

public class HelloWorld
{
    [Fact]
    public void HelloWorld_Works()
    {
        // Define the gravity vector.
        var gravity = new Vector2(0f, -10f);

        // Construct a world object, which will hold and simulate the rigid bodies.
        using var world = new World(gravity);

        // Define the ground body.
        using var groundBodyDef = BodyDef.Create();
        groundBodyDef.Position = new(0f, -10f);

        // Call the body factory to create a body and add it to the world.
        var groundBody = world.CreateBody(groundBodyDef);

        // Define the ground box shape.
        using var groundBox = PolygonShape.Create();

        // Set the extents (half-widths) of the box.
        groundBox.SetAsBox(50f, 10f);

        // Add the ground fixture to the ground body.
        groundBody.CreateFixture(groundBox, 0.0f);

        // Define the dynamic body. We set its position and call the body factory.
        using var bodyDef = BodyDef.Create();
        bodyDef.Type = BodyType.Dynamic;
        bodyDef.Position = new(0f, 4f);
        var body = world.CreateBody(bodyDef);

        // Define another box shape for our dynamic body.
        using var dynamicBox = PolygonShape.Create();
        dynamicBox.SetAsBox(1f, 1f);

        // Define the dynamic body fixture.
        using var fixtureDef = FixtureDef.Create();
        fixtureDef.Shape = dynamicBox;
        fixtureDef.Density = 1f;       // Non-zero density so it will be dynamic.
        fixtureDef.Friction = 0.3f;    // Override the default friction.

        // Add the fixture to the body.
        body.CreateFixture(fixtureDef);

        // Prepare for simulation with a time step of 1/60 of a second.
        var timeStep = 1f / 60f;
        var velocityIterations = 6;
        var positionIterations = 2;

        for (var i = 0; i < 60; i++)
        {
            // Instruct the world to perform a single step of simulation.
            world.Step(timeStep, velocityIterations, positionIterations);
        }

        // Assert that the simulation result matches what we expect.
        Assert.True(body.Position.X < 0.01f);
        Assert.True(body.Position.Y - 1.01f < 0.01f);
        Assert.True(body.Angle < 0.01f);
    }
}
