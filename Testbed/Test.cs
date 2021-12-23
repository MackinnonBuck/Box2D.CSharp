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

    private DebugDraw _debugDraw = default!;

    private Settings _settings = default!;

    public Joint? MouseJoint { get; set; } = default!;

    protected World World { get; }

    protected TestDestructionListener DestructionListener { get; }

    protected ContactPoint[] Points { get; } = new ContactPoint[MaxContactPoints];

    protected Body GroundBody { get; }

    protected Body? Bomb { get; private set; } = default!;

    protected int PointCount { get; private set; }

    protected bool BombSpawning { get; private set; }

    protected int StepCount { get; private set; }

    protected int TextLine { get; set; } = 30;

    protected Profile MaxProfile { get; private set; }

    protected Profile TotalProfile { get; private set; }

    public Test()
    {
        DestructionListener = new(this);
        World = new World(new(0f, -10f));
        World.SetDestructionListener(DestructionListener);
        World.SetContactListener(this);

        var bodyDef = new BodyDef();
        GroundBody = World.CreateBody(bodyDef);

        // TODO: Profiling support and display.
    }

    public void Initialize(DebugDraw debugDraw, Settings settings)
    {
        _debugDraw = debugDraw;
        _settings = settings;

        World.SetDebugDraw(_debugDraw);
    }

    public void Step()
    {
        var timeStep = _settings.hertz > 0f ? 1f / _settings.hertz : 0f;

        if (_settings.pause)
        {
            if (_settings.singleStep)
            {
                _settings.singleStep = false;
            }
            else
            {
                timeStep = 0f;
            }

            _debugDraw.DrawString(5, TextLine, "****PAUSED****");
            TextLine += TextIncrement;
        }

        uint flags = 0;

        if (_settings.drawShapes)
        {
            flags += (uint)DrawFlags.ShapeBit;
        }

        if (_settings.drawJoints)
        {
            flags += (uint)DrawFlags.JointBit;
        }

        if (_settings.drawAABBs)
        {
            flags += (uint)DrawFlags.AabbBit;
        }

        if (_settings.drawCOMs)
        {
            flags += (uint)DrawFlags.CenterOfMassBit;
        }

        _debugDraw.Flags = (DrawFlags)flags;

        World.AllowSleeping = _settings.enableSleep;
        World.WarmStarting = _settings.enableWarmStarting;
        World.ContinuousPhysics = _settings.enableContinuous;
        World.SubStepping = _settings.enableSubStepping;

        PointCount = 0;

        World.Step(timeStep, 8, 3);
        World.DebugDraw();

        _debugDraw.Flush();

        if (timeStep > 0f)
        {
            StepCount++;
        }

        if (_settings.drawStats)
        {
            var bodyCount = World.BodyCount;
            var contactCount = World.ContactCount;
            var jointCount = World.JointCount;
            _debugDraw.DrawString(5, TextLine, $"bodies/contacts/joints = {bodyCount}/{contactCount}/{jointCount}");
            TextLine += TextIncrement;

            var proxyCount = World.ProxyCount;
            var height = World.TreeHeight;
            var balance = World.TreeBalance;
            var quality = World.TreeQuality;
            _debugDraw.DrawString(5, TextLine, $"proxies/height/balance/quality = {proxyCount}/{height}/{balance}/{quality}");
            TextLine += TextIncrement;
        }

        World.GetProfile(out var profile);

        MaxProfile = new()
        {
            Step = Math.Max(MaxProfile.Step, profile.Step),
            Collide = Math.Max(MaxProfile.Collide, profile.Collide),
            Solve = Math.Max(MaxProfile.Solve, profile.Solve),
            SolveInit = Math.Max(MaxProfile.SolveInit, profile.SolveInit),
            SolveVelocity = Math.Max(MaxProfile.SolveVelocity, profile.SolveVelocity),
            SolvePosition = Math.Max(MaxProfile.SolvePosition, profile.SolvePosition),
            SolveTOI = Math.Max(MaxProfile.SolveTOI, profile.SolveTOI),
            Broadphase = Math.Max(MaxProfile.Broadphase, profile.Broadphase),
        };

        TotalProfile = new()
        {
            Step = TotalProfile.Step + profile.Step,
            Collide = TotalProfile.Collide + profile.Collide,
            Solve = TotalProfile.Solve + profile.Solve,
            SolveInit = TotalProfile.SolveInit + profile.SolveInit,
            SolveVelocity = TotalProfile.SolveVelocity + profile.SolveVelocity,
            SolvePosition = TotalProfile.SolvePosition + profile.SolvePosition,
            SolveTOI = TotalProfile.SolveTOI + profile.SolveTOI,
            Broadphase = TotalProfile.Broadphase + profile.Broadphase,
        };

        if (_settings.drawProfile)
        {
            var averageProfile = new Profile();

            if (StepCount > 0)
            {
                var scale = 1f / StepCount;
                averageProfile.Step = scale * TotalProfile.Step;
                averageProfile.Collide = scale * TotalProfile.Collide;
                averageProfile.Solve = scale * TotalProfile.Solve;
                averageProfile.SolveInit = scale * TotalProfile.SolveInit;
                averageProfile.SolveVelocity = scale * TotalProfile.SolveVelocity;
                averageProfile.SolvePosition = scale * TotalProfile.SolvePosition;
                averageProfile.SolveTOI = scale * TotalProfile.SolveTOI;
                averageProfile.Broadphase = scale * TotalProfile.Broadphase;
            }

            _debugDraw.DrawString(5, TextLine, $"step [ave] (max) = {profile.Step:.00} [{averageProfile.Step:.00}] ({MaxProfile.Step:.00})");
            TextLine += TextIncrement;
            _debugDraw.DrawString(5, TextLine, $"collide [ave] (max) = {profile.Collide:.00} [{averageProfile.Collide:.00}] ({MaxProfile.Collide:.00})");
            TextLine += TextIncrement;
            _debugDraw.DrawString(5, TextLine, $"solve [ave] (max) = {profile.Solve:.00} [{averageProfile.Solve:.00}] ({MaxProfile.Solve:.00})");
            TextLine += TextIncrement;
            _debugDraw.DrawString(5, TextLine, $"solve init [ave] (max) = {profile.SolveInit:.00} [{averageProfile.SolveInit:.00}] ({MaxProfile.SolveInit:.00})");
            TextLine += TextIncrement;
            _debugDraw.DrawString(5, TextLine, $"solve velocity [ave] (max) = {profile.SolveVelocity:.00} [{averageProfile.SolveVelocity:.00}] ({MaxProfile.SolveVelocity:.00})");
            TextLine += TextIncrement;
            _debugDraw.DrawString(5, TextLine, $"solve position [ave] (max) = {profile.SolvePosition:.00} [{averageProfile.SolvePosition:.00}] ({MaxProfile.SolvePosition:.00})");
            TextLine += TextIncrement;
            _debugDraw.DrawString(5, TextLine, $"solveTOI [ave] (max) = {profile.SolveTOI:.00} [{averageProfile.SolveTOI:.00}] ({MaxProfile.SolveTOI:.00})");
            TextLine += TextIncrement;
            _debugDraw.DrawString(5, TextLine, $"broad-phase [ave] (max) = {profile.Broadphase:.00} [{averageProfile.Broadphase:.00}] ({MaxProfile.Broadphase:.00})");
            TextLine += TextIncrement;
        }

        // TODO: Bomb spawning.

        if (_settings.drawContactPoints)
        {
            var impulseScale = 0.1f;
            var axisScale = 0.3f;

            Console.WriteLine(PointCount);

            for (var i = 0; i < PointCount; i++)
            {
                ref var point = ref Points[i];

                if (point.State == PointState.Add)
                {
                    _debugDraw.DrawPoint(point.Position, 10f, new(0.3f, 0.95f, 0.3f));
                }
                else if (point.State == PointState.Persist)
                {
                    _debugDraw.DrawPoint(point.Position, 5f, new(0.3f, 0.3f, 0.95f));
                }

                if (_settings.drawContactNormals)
                {
                    var p1 = point.Position;
                    var p2 = p1 + axisScale * point.Normal;
                    _debugDraw.DrawSegment(p1, p2, new(0.9f, 0.9f, 0.9f));
                }
                else if (_settings.drawContactImpulse)
                {
                    var p1 = point.Position;
                    var p2 = p1 + impulseScale * point.NormalImpulse * point.Normal;
                    _debugDraw.DrawSegment(p1, p2, new(0.9f, 0.9f, 0.3f));
                }

                if (_settings.drawFrictionImpulse)
                {
                    var tangent = point.Normal.Cross(1f);
                    var p1 = point.Position;
                    var p2 = p1 + impulseScale * point.TangentImpulse * tangent;
                    _debugDraw.DrawSegment(p1, p2, new(0.9f, 0.9f, 0.3f));
                }
            }
        }
    }

    public void DrawTitle(string title)
    {
        _debugDraw.DrawString(5, 5, title);
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
            Points[PointCount] = new()
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
