using Box2D;
using Testbed.Drawing;

namespace Testbed;

internal class TestDestructionListener : DestructionListener
{
    private readonly Test _test;

    public TestDestructionListener(Test test)
    {
        _test = test;
    }

    protected override void SayGoodbye(Joint joint)
    {
        if (_test.MouseJoint == joint)
        {
            _test.MouseJoint = null;
        }
        else
        {
            _test.JointDestroyed(joint);
        }
    }
}

struct ContactPoint
{
    public Fixture? FixtureA { get; init; }

    public Fixture? FixtureB { get; init; }

    public Vec2 Normal { get; init; }

    public Vec2 Position { get; init; }

    public PointState State { get; init; }

    public float NormalImpulse { get; init; }

    public float TangentImpulse { get; init; }

    public float Separation { get; set; }
}

internal class Test : ContactListener
{
    protected const int TextIncrement = 13;

    protected const int MaxContactPoints = 2048;

    private readonly WorldManifold _worldManifold = new();

    public Joint? MouseJoint { get; set; } = default!;

    protected DebugDraw DebugDraw { get; }

    protected World World { get; }

    protected TestDestructionListener DestructionListener { get; }

    protected ContactPoint[] ContactPoints { get; } = new ContactPoint[2];

    protected Body GroundBody { get; }

    protected Body? Bomb { get; private set; } = default!;

    protected int PointCount { get; private set; }

    protected bool BombSpawning { get; private set; }

    protected int StepCount { get; private set; }

    protected int TextLine { get; set; } = 30;

    public Test(DebugDraw debugDraw)
    {
        DebugDraw = debugDraw;
        DestructionListener = new(this);
        World = new World(new(0f, -10f));
        World.SetDestructionListener(DestructionListener);
        World.SetContactListener(this);
        World.SetDebugDraw(debugDraw);

        var bodyDef = new BodyDef();
        GroundBody = World.CreateBody(bodyDef);

        // TODO: Profiling support and display.
    }

    public void DrawTitle(string title)
    {
        DebugDraw.DrawString(5, 5, title);
        TextLine = 26;
    }

    public virtual void JointDestroyed(Joint joint)
    {
    }

    protected override void PreSolve(in Contact contact, in Manifold oldManifold)
    {
        var manifold = contact.Manifold;

        if (manifold.Points.Length == 0)
        {
            return;
        }

        var fixtureA = contact.FixtureA;
        var fixtureB = contact.FixtureB;

        Span<PointState> state1 = stackalloc PointState[2];
        Span<PointState> state2 = stackalloc PointState[2];

        Collision.GetPointStates(state1, state2, oldManifold, manifold);

        contact.GetWorldManifold(_worldManifold);

        for (var i = 0; i < manifold.Points.Length && PointCount < MaxContactPoints; i++, PointCount++)
        {
            ContactPoints[i] = new()
            {
                FixtureA = fixtureA,
                FixtureB = fixtureB,
                Position = _worldManifold.Points[i],
                Normal = _worldManifold.Normal,
                State = state2[i],
                NormalImpulse = manifold.Points[i].NormalImpulse,
                TangentImpulse = manifold.Points[i].TangentImpulse,
                Separation = _worldManifold.Separations[i],
            };
        }
    }

    protected override void Dispose(bool disposing)
    {
        World.Dispose();

        base.Dispose(disposing);
    }
}
