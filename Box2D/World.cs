using Box2D.Core;
using Box2D.Math;

namespace Box2D;

using static Interop.NativeMethods;

public class World : Box2DRootObject
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "CodeQuality",
        "IDE0052:Remove unread private members",
        Justification = "Need to maintain a reference to the contact listener to keep it from getting garbage collected.")]
    private ContactListener? _contactListener;

    public Body? BodyList => Body.FromIntPtr.Get(b2World_GetBodyList(Native));

    public Joint? JointList => Joint.FromIntPtr.Get(b2World_GetJointList(Native));

    public World(Vec2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);

        Initialize(native);
    }

    public void SetContactListener(ContactListener listener)
    {
        _contactListener = listener;
        b2World_SetContactListener(Native, listener.Native);
    }

    public Body CreateBody(in BodyDef def)
        => new(Native, in def);

    public void DestroyBody(Body body)
    {
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
        => Joint.Create(Native, def);

    public void Step(float timeStep, int velocityIterations, int positionIterations)
    {
        b2World_Step(Native, timeStep, velocityIterations, positionIterations);
    }

    public void ClearForces()
    {
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
