using Box2D;
using Xunit;

namespace UnitTests;

public class WorldTests
{

    [Fact]
    public void WorldTest_Works()
    {
        using var world = new World(new Vec2(0f, -10f));
        var destructionListener = new MyDestructionListener();
        var contactListener = new MyContactListener();
        var draw = new MyDraw
        {
            Flags = DrawFlags.ShapeBit | DrawFlags.AabbBit
        };
        world.SetDestructionListener(destructionListener);
        world.SetContactListener(contactListener);
        world.SetDebugDraw(draw);

        using var circle = new CircleShape
        {
            Radius = 5f,
        };

        using var bodyDef = new BodyDef
        {
            Type = BodyType.Dynamic,
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
        Assert.False(contactListener.DidBeginContact);

        bodyB.SetTransform(new Vec2(1f, 0f), 0f);

        world.Step(timeStep, velocityIterations, positionIterations);

        Assert.Equal(0, draw.SolidCircleDrawCount);
        Assert.Equal(0, draw.PolygonDrawCount);

        world.DebugDraw();

        Assert.Equal(2, draw.SolidCircleDrawCount); // 2 circle shapes
        Assert.Equal(2, draw.PolygonDrawCount); // 2 AABBs

        Assert.True(world.ContactList.IsValid);
        Assert.True(contactListener.DidBeginContact);

        Assert.Equal(0, destructionListener.SayGoodbyeJointCount);
        Assert.Equal(0, destructionListener.SayGoodbyeFixtureCount);

        world.DestroyBody(bodyA);
        world.DestroyBody(bodyB);

        Assert.Equal(0, destructionListener.SayGoodbyeJointCount);
        Assert.Equal(2, destructionListener.SayGoodbyeFixtureCount);
    }

    private class MyDestructionListener : DestructionListener
    {
        public int SayGoodbyeJointCount { get; private set; }

        public int SayGoodbyeFixtureCount { get; private set; }

        protected override void SayGoodbye(Joint joint)
        {
            SayGoodbyeJointCount++;
        }

        protected override void SayGoodbye(Fixture fixture)
        {
            SayGoodbyeFixtureCount++;
        }
    }

    private class MyContactListener : ContactListener
    {
        public bool DidBeginContact { get; private set; }

        protected override void BeginContact(in Contact contact)
        {
            DidBeginContact = true;
        }
    }

    private class MyDraw : Draw
    {
        public int SolidCircleDrawCount { get; private set; }

        public int PolygonDrawCount { get; private set; }

        public override void DrawPolygon(in Box2DArrayRef<Vec2> vertices, Color color)
        {
            PolygonDrawCount++;
        }

        public override void DrawSolidCircle(Vec2 center, float radius, Vec2 axis, Color color)
        {
            SolidCircleDrawCount++;
        }
    }
}
