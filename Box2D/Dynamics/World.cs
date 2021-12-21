namespace Box2D;

using static NativeMethods;

public class World : Box2DObject
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "CodeQuality",
        "IDE0052:Remove unread private members",
        Justification = "Need to maintain a reference to the contact listener to keep it from getting garbage collected.")]
    private ContactListener? _contactListener;

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

    public World(Vec2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);

        Initialize(native);
    }

    public void SetContactListener(ContactListener listener)
    {
        ThrowIfDisposed();

        _contactListener = listener;
        b2World_SetContactListener(Native, listener.Native);
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
            joint.Dispose();
        }

        foreach (var fixture in body.FixtureList)
        {
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

    private protected override void Dispose(bool disposing)
    {
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

        // TODO: See if there's anything else to do here (do we care about the disposing parameter?).
        // Might want to be careful that if this instance is being disposed due to having no references left,
        // then we might invalidate other instances (bodies, etc.) actually in use.
        // It would be a weird scenario, but it's feasible that this could happen.
        // It might solve itself if each body has a reference to its containing world.
        // This would have parity with the C++ implementation, too.
        b2World_delete(Native);
    }
}
