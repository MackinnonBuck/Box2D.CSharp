namespace Box2D;

using static NativeMethods;

public class World : Box2DObject
{
    private ContactListener? _contactListener;
    private DestructionListener? _destructionListener;
    private Draw? _debugDraw;

    public Body? BodyList
    {
        get
        {
            ThrowIfDisposed();
            return Body.FromIntPtr.Get(b2World_GetBodyList(Native));
        }
    }

    public Joint? JointList
    {
        get
        {
            ThrowIfDisposed();
            return Joint.FromIntPtr.Get(b2World_GetJointList(Native));
        }
    }

    public Contact ContactList
    {
        get
        {
            ThrowIfDisposed();
            return new(b2World_GetContactList(Native));
        }
    }

    public bool AllowSleeping
    {
        get
        {
            ThrowIfDisposed();
            return b2World_GetAllowSleeping(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2World_SetAllowSleeping(Native, value);
        }
    }

    public bool WarmStarting
    {
        get
        {
            ThrowIfDisposed();
            return b2World_GetWarmStarting(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2World_SetWarmStarting(Native, value);
        }
    }

    public bool ContinuousPhysics
    {
        get
        {
            ThrowIfDisposed();
            return b2World_GetContinuousPhysics(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2World_SetContinuousPhysics(Native, value);
        }
    }

    public bool SubStepping
    {
        get
        {
            ThrowIfDisposed();
            return b2World_GetSubStepping(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2World_SetSubStepping(Native, value);
        }
    }

    public Vec2 Gravity
    {
        get
        {
            ThrowIfDisposed();
            b2World_GetGravity(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2World_SetGravity(Native, ref value);
        }
    }

    public World(Vec2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);

        Initialize(native);
    }

    public void SetDestructionListener(DestructionListener listener)
    {
        ThrowIfDisposed();

        _destructionListener = listener;
    }

    public void SetContactListener(ContactListener listener)
    {
        ThrowIfDisposed();

        _contactListener = listener;
        b2World_SetContactListener(Native, _contactListener.Native);
    }

    public void SetDebugDraw(Draw debugDraw)
    {
        ThrowIfDisposed();

        _debugDraw = debugDraw;
        b2World_SetDebugDraw(Native, _debugDraw.Native);
    }

    public Body CreateBody(in BodyDef def)
    {
        ThrowIfDisposed();
        return new(Native, in def);
    }

    public void DestroyBody(Body body)
    {
        ThrowIfDisposed();

        foreach (var joint in body.JointList)
        {
            _destructionListener?.SayGoodbye(joint);
            joint.Dispose();
        }

        foreach (var fixture in body.FixtureList)
        {
            _destructionListener?.SayGoodbye(fixture);
            fixture.Dispose();
        }

        b2World_DestroyBody(Native, body.Native);

        body.Dispose();
    }

    public Joint CreateJoint(JointDef def)
    {
        ThrowIfDisposed();
        return Joint.Create(Native, def);
    }

    public void Step(float timeStep, int velocityIterations, int positionIterations)
    {
        ThrowIfDisposed();
        b2World_Step(Native, timeStep, velocityIterations, positionIterations);
    }

    public void ClearForces()
    {
        ThrowIfDisposed();
        b2World_ClearForces(Native);
    }

    public void DebugDraw()
    {
        ThrowIfDisposed();
        b2World_DebugDraw(Native);
    }

    protected override void Dispose(bool disposing)
    {
        // TODO: See if there's anything else to do here (do we care about the disposing parameter?).
        // Might want to be careful that if this instance is being disposed due to having no references left,
        // then we might invalidate other instances (bodies, etc.) actually in use.
        // It would be a weird scenario, but it's feasible that this could happen.
        // It might solve itself if each body has a reference to its containing world.
        // This would have parity with the C++ implementation, too.

        foreach (var body in BodyList)
        {
            foreach (var fixture in body.FixtureList)
            {
                fixture.Dispose();
            }

            body.Dispose();
        }

        foreach (var joint in JointList)
        {
            joint.Dispose();
        }

        b2World_delete(Native);
    }
}
