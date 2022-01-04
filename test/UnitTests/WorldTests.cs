using Box2D.Collections;
using Box2D.Collision;
using Box2D.Collision.Shapes;
using Box2D.Drawing;
using Box2D.Dynamics;
using Box2D.Dynamics.Callbacks;
using Box2D.Math;
using System.Numerics;
using Xunit;

namespace UnitTests;

public class WorldTests
{
    [Fact]
    public void WorldTest_Works()
    {
        using var world = new World(new Vector2(0f, -10f));
        var destructionListener = new MyDestructionListener();
        var contactListener = new MyContactListener();
        var draw = new MyDraw();
        world.SetDestructionListener(destructionListener);
        world.SetContactListener(contactListener);
        world.SetDebugDraw(draw);
        world.DrawFlags = DrawFlags.ShapeBit | DrawFlags.AabbBit;

        using var circle = CircleShape.Create();
        circle.Radius = 5f;

        using var bodyDef = BodyDef.Create();
        bodyDef.Type = BodyType.Dynamic;

        var bodyA = world.CreateBody(bodyDef);
        var bodyB = world.CreateBody(bodyDef);
        bodyA.CreateFixture(circle, 0f);
        bodyB.CreateFixture(circle, 0f);

        bodyA.SetTransform(new Vector2(0f, 0f), 0f);
        bodyB.SetTransform(new Vector2(100f, 0f), 0f);

        var timeStep = 1f / 60f;
        var velocityIterations = 6;
        var positionIterations = 2;

        world.Step(timeStep, velocityIterations, positionIterations);

        Assert.True(world.ContactList.IsNull);
        Assert.False(contactListener.DidBeginContact);

        bodyB.SetTransform(new Vector2(1f, 0f), 0f);

        world.Step(timeStep, velocityIterations, positionIterations);

        Assert.Equal(0, draw.SolidCircleDrawCount);
        Assert.Equal(0, draw.PolygonDrawCount);

        world.DebugDraw();

        Assert.Equal(2, draw.SolidCircleDrawCount); // 2 circle shapes
        Assert.Equal(2, draw.PolygonDrawCount); // 2 AABBs

        Assert.False(world.ContactList.IsNull);
        Assert.True(contactListener.DidBeginContact);

        Assert.Equal(0, destructionListener.SayGoodbyeJointCount);
        Assert.Equal(0, destructionListener.SayGoodbyeFixtureCount);

        world.DestroyBody(bodyA);
        world.DestroyBody(bodyB);

        Assert.Equal(0, destructionListener.SayGoodbyeJointCount);
        Assert.Equal(2, destructionListener.SayGoodbyeFixtureCount);
    }

    private class MyDestructionListener : IDestructionListener
    {
        public int SayGoodbyeJointCount { get; private set; }

        public int SayGoodbyeFixtureCount { get; private set; }

        void IDestructionListener.SayGoodbye(Joint joint)
        {
            SayGoodbyeJointCount++;
        }

        void IDestructionListener.SayGoodbye(Fixture fixture)
        {
            SayGoodbyeFixtureCount++;
        }
    }

    private class MyContactListener : IContactListener
    {
        public bool DidBeginContact { get; private set; }

        void IContactListener.BeginContact(in Contact contact)
        {
            DidBeginContact = true;
        }

        void IContactListener.EndContact(in Contact contact)
        {
        }

        void IContactListener.PostSolve(in Contact contact, in ContactImpulse impulse)
        {
        }

        void IContactListener.PreSolve(in Contact contact, in Manifold oldManifold)
        {
        }
    }

    private class MyDraw : IDraw
    {
        public int SolidCircleDrawCount { get; private set; }

        public int PolygonDrawCount { get; private set; }

        void IDraw.DrawCircle(Vector2 center, float radius, Color color)
        {
        }

        void IDraw.DrawPoint(Vector2 p, float size, Color color)
        {
        }

        void IDraw.DrawPolygon(in ArrayRef<Vector2> vertices, Color color)
        {
            PolygonDrawCount++;
        }

        void IDraw.DrawSegment(Vector2 p1, Vector2 p2, Color color)
        {
        }

        void IDraw.DrawSolidCircle(Vector2 center, float radius, Vector2 axis, Color color)
        {
            SolidCircleDrawCount++;
        }

        void IDraw.DrawSolidPolygon(in ArrayRef<Vector2> vertices, Color color)
        {
        }

        void IDraw.DrawTransform(Transform xf)
        {
        }
    }
}
