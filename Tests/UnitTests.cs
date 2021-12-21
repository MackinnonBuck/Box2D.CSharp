using Box2D;
using System;
using Xunit;

namespace Tests;

public class UnitTests
{
    [Fact]
    public void HelloWorld_Works()
    {
        // Define the gravity vector.
        var gravity = new Vec2(0f, -10f);

        // Construct a world object, which will hold and simulate the rigid bodies.
        using var world = new World(gravity);

        // Define the ground body.
        var groundBodyDef = new BodyDef
        {
            Position = new(0f, -10f),
        };

        // Call the body factory to create a body and add it to the world.
        var groundBody = world.CreateBody(groundBodyDef);

        // Define the ground box shape.
        using var groundBox = new PolygonShape();

        // Set the extents (half-widths) of the box.
        groundBox.SetAsBox(50f, 10f);

        // Add the ground fixture to the ground body.
        groundBody.CreateFixture(groundBox, 0.0f);

        // Define the dynamic body. We set its position and call the body factory.
        var bodyDef = new BodyDef
        {
            Type = BodyType.Dyanmic,
            Position = new(0f, 4f),
        };
        var body = world.CreateBody(bodyDef);

        // Define another box shape for our dynamic body.
        using var dynamicBox = new PolygonShape();
        dynamicBox.SetAsBox(1f, 1f);

        // Define the dynamic body fixture.
        var fixtureDef = new FixtureDef
        {
            Shape = dynamicBox,
            Density = 1f,       // Non-zero density so it will be dynamic.
            Friction = 0.3f,    // Override the default friction.
        };

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

    [Fact]
    public void CollisionTest_Works()
    {
        var center = new Vec2(100f, -50f);
        var hx = 0.5f;
        var hy = 1.5f;
        var angle1 = 0.25f;

        using var polygon1 = new PolygonShape();
        polygon1.SetAsBox(hx, hy, center, angle1);

        var epsilon = 1.192092896e-07f;
        var absTol = 2f * epsilon;
        var relTol = 2f * epsilon;

        Assert.True(MathF.Abs(polygon1.Centroid.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(polygon1.Centroid.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));

        Span<Vec2> vertices = stackalloc Vec2[]
        {
            new Vec2(center.X - hx, center.Y - hy),
            new Vec2(center.X + hx, center.Y - hy),
            new Vec2(center.X - hx, center.Y + hy),
            new Vec2(center.X + hx, center.Y + hy),
        };

        using var polygon2 = new PolygonShape();
        polygon2.Set(vertices);

        Assert.True(MathF.Abs(polygon2.Centroid.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(polygon2.Centroid.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));

        var mass = 4f * hx * hy;
        var inertia = (mass / 3f) * (hx * hx + hy * hy) + mass * center.Dot(center);

        polygon1.ComputeMass(out var massData1, 1f);

        Assert.True(MathF.Abs(massData1.Center.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(massData1.Center.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));
        Assert.True(MathF.Abs(massData1.Mass - mass) < 20f * (absTol + relTol * mass));
        Assert.True(MathF.Abs(massData1.I - inertia) < 40f * (absTol + relTol * inertia));

        polygon2.ComputeMass(out var massData2, 1f);

        Assert.True(MathF.Abs(massData2.Center.X - center.X) < absTol + relTol * MathF.Abs(center.X));
        Assert.True(MathF.Abs(massData2.Center.Y - center.Y) < absTol + relTol * MathF.Abs(center.Y));
        Assert.True(MathF.Abs(massData2.Mass - mass) < 20f * (absTol + relTol * mass));
        Assert.True(MathF.Abs(massData2.I - inertia) < 40f * (absTol + relTol * inertia));
    }

    [Fact]
    public void JointTest_Works()
    {
        var gravity = new Vec2(0f, -10f);
        using var world = new World(gravity);

        var bodyDef = new BodyDef
        {
            Type = BodyType.Dyanmic,
            Position = new(-2f, 3f),
        };
        var ground = world.CreateBody(bodyDef);

        using var circle = new CircleShape
        {
            Radius = 1f,
        };

        var fixtureDef = new FixtureDef
        {
            Filter = new()
            {
                MaskBits = 0,
            },
            Density = 1f,
            Shape = circle,
        };

        var bodyA = world.CreateBody(bodyDef);
        var bodyB = world.CreateBody(bodyDef);
        var bodyC = world.CreateBody(bodyDef);

        circle.ComputeMass(out var massData, fixtureDef.Density);
        var mg = massData.Mass * gravity.Y;

        bodyA.CreateFixture(fixtureDef);
        bodyB.CreateFixture(fixtureDef);
        bodyC.CreateFixture(fixtureDef);

        using var distanceJointDef = new DistanceJointDef();
        distanceJointDef.Initialize(ground, bodyA, bodyDef.Position + new Vec2(0f, 4f), bodyDef.Position);
        distanceJointDef.MinLength = distanceJointDef.Length;
        distanceJointDef.MaxLength = distanceJointDef.Length;

        using var prismaticJointDef = new PrismaticJointDef();
        prismaticJointDef.Initialize(ground, bodyB, bodyDef.Position, new Vec2(1f, 0f));

        using var revoluteJointDef = new RevoluteJointDef();
        revoluteJointDef.Initialize(ground, bodyC, bodyDef.Position);

        var distanceJoint = (DistanceJoint)world.CreateJoint(distanceJointDef);
        var prismaticJoint = (PrismaticJoint)world.CreateJoint(prismaticJointDef);
        var revoluteJoint = (RevoluteJoint)world.CreateJoint(revoluteJointDef);

        var timeStep = 1f / 60f;
        var invTimeStep = 60f;
        var velocityIterations = 6;
        var positionIterations = 2;

        world.Step(timeStep, velocityIterations, positionIterations);

        var tol = 1e-5f;

        {
            var f = distanceJoint.GetReactionForce(invTimeStep);
            var t = distanceJoint.GetReactionTorque(invTimeStep);
            Assert.Equal(0f, f.X);
            Assert.True(MathF.Abs(f.Y + mg) < tol);
            Assert.Equal(0f, t);
        }

        {
            var f = prismaticJoint.GetReactionForce(invTimeStep);
            var t = prismaticJoint.GetReactionTorque(invTimeStep);
            Assert.Equal(0f, f.X);
            Assert.True(MathF.Abs(f.Y + mg) < tol);
            Assert.Equal(0f, t);
        }

        {
            var f = revoluteJoint.GetReactionForce(invTimeStep);
            var t = revoluteJoint.GetReactionTorque(invTimeStep);
            Assert.Equal(0f, f.X);
            Assert.True(MathF.Abs(f.Y + mg) < tol);
            Assert.Equal(0f, t);
        }
    }

    [Fact]
    public void WorldTest_Works()
    {
        using var world = new World(new Vec2(0f, -10f));
        var listener = new MyContactListener();
        world.SetContactListener(listener);

        using var circle = new CircleShape
        {
            Radius = 5f,
        };

        var bodyDef = new BodyDef
        {
            Type = BodyType.Dyanmic,
        };

        var bodyA = world.CreateBody(bodyDef);
        var bodyB = world.CreateBody(bodyDef);
        bodyA.CreateFixture(circle, 0f);
        bodyB.CreateFixture(circle, 0f);

        bodyA.SetTransform(new Vec2(0f, 0f), 0f);
        bodyB.SetTransform(new Vec2(100f, 0f), 0f);

        var timeStep = 1f / 60f;
        var velocityIterations = 6;
        var positionIterations = 2;

        world.Step(timeStep, velocityIterations, positionIterations);

        Assert.False(world.ContactList.IsValid);
        Assert.False(listener.DidBeginContact);

        bodyB.SetTransform(new Vec2(1f, 0f), 0f);

        world.Step(timeStep, velocityIterations, positionIterations);

        Assert.True(world.ContactList.IsValid);
        Assert.True(listener.DidBeginContact);
    }

    private class MyContactListener : ContactListener
    {
        public bool DidBeginContact { get; private set; }

        protected override void BeginContact(in Contact contact)
        {
            DidBeginContact = true;
        }
    }
}
