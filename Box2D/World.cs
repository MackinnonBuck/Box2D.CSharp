using Box2D.Math;

namespace Box2D;

using static Interop.NativeMethods;

public class World : Box2DDisposableObject
{
    public Body? BodyList => Body.FromIntPtr(b2World_GetBodyList(Native));

    public Joint? JointList => Joint.FromIntPtr(b2World_GetJointList(Native));

    public World(Vec2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);

        Initialize(native);
    }

    public Body CreateBody(in BodyDef def)
        => new(Native, in def);

    public void DestroyBody(Body body)
    {
        // TODO: Would be really nice if we had a way to automatically track ownership
        // and take care of things like this through some layer of abstraction.
        foreach (var joint in body.JointList)
        {
            joint.InvalidateInstance();
        }

        foreach (var fixture in body.FixtureList)
        {
            fixture.InvalidateInstance();
        }

        b2World_DestroyBody(Native, body.Native);
        body.InvalidateInstance();
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
                fixture.InvalidateInstance();
            }

            body.InvalidateInstance();
        }

        foreach (var joint in JointList)
        {
            joint.InvalidateInstance();
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
