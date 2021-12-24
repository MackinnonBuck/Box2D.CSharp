namespace Box2D;

using static NativeMethods;

public class World : Box2DDisposableObject
{
    private ContactListener? _contactListener;
    private DestructionListener? _destructionListener;
    private Draw? _debugDraw;

    public Body? BodyList => Body.FromIntPtr.Get(b2World_GetBodyList(Native));

    public Joint? JointList => Joint.FromIntPtr.Get(b2World_GetJointList(Native));

    public Contact ContactList => new(b2World_GetContactList(Native));

    public bool AllowSleeping
    {
        get => b2World_GetAllowSleeping(Native);
        set => b2World_SetAllowSleeping(Native, value);
    }

    public bool WarmStarting
    {
        get => b2World_GetWarmStarting(Native);
        set => b2World_SetWarmStarting(Native, value);
    }

    public bool ContinuousPhysics
    {
        get => b2World_GetContinuousPhysics(Native);
        set => b2World_SetContinuousPhysics(Native, value);
    }

    public bool SubStepping
    {
        get => b2World_GetSubStepping(Native);
        set => b2World_SetSubStepping(Native, value);
    }

    public int ProxyCount => b2World_GetProxyCount(Native);

    public int BodyCount => b2World_GetBodyCount(Native);

    public int JointCount => b2World_GetJointCount(Native);

    public int ContactCount => b2World_GetContactCount(Native);

    public int TreeHeight => b2World_GetTreeHeight(Native);

    public int TreeBalance => b2World_GetTreeBalance(Native);

    public float TreeQuality => b2World_GetTreeQuality(Native);

    public Vec2 Gravity
    {
        get
        {
            b2World_GetGravity(Native, out var value);
            return value;
        }
        set => b2World_SetGravity(Native, ref value);
    }

    public World(Vec2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);

        Initialize(native);
    }

    public void SetDestructionListener(DestructionListener listener)
    {
        _destructionListener = listener;
    }

    public void SetContactListener(ContactListener listener)
    {
        _contactListener = listener;
        b2World_SetContactListener(Native, _contactListener.Native);
    }

    public void SetDebugDraw(Draw debugDraw)
    {
        _debugDraw = debugDraw;
        b2World_SetDebugDraw(Native, _debugDraw.Native);
    }

    public Body CreateBody(in BodyDef def)
        => new(this, in def);

    public void DestroyBody(Body body)
    {
        foreach (var joint in body.JointList)
        {
            _destructionListener?.SayGoodbye(joint);
            joint.FreeHandle();
        }

        foreach (var fixture in body.FixtureList)
        {
            _destructionListener?.SayGoodbye(fixture);
            fixture.FreeHandle();
        }

        b2World_DestroyBody(Native, body.Native);
        body.FreeHandle();
    }

    public Joint CreateJoint(JointDef def)
        => Joint.Create(Native, def);

    public void DestroyJoint(Joint joint)
    {
        b2World_DestroyJoint(Native, joint.Native);
        joint.FreeHandle();
    }

    public void Step(float timeStep, int velocityIterations, int positionIterations)
        => b2World_Step(Native, timeStep, velocityIterations, positionIterations);

    public void ClearForces()
        => b2World_ClearForces(Native);

    public void DebugDraw()
        => b2World_DebugDraw(Native);

    public void QueryAABB(QueryCallback callback, AABB aabb)
        => b2World_QueryAABB(Native, callback.Native, ref aabb);

    public void RayCast(RayCastCallback callback, Vec2 point1, Vec2 point2)
        => b2World_RayCast(Native, callback.Native, ref point1, ref point2);

    public void ShiftOrigin(Vec2 newOrigin)
        => b2World_ShiftOrigin(Native, ref newOrigin);

    public void GetProfile(out Profile profile)
        => b2World_GetProfile(Native, out profile);

    protected override void Dispose(bool disposing)
    {
        foreach (var body in BodyList)
        {
            foreach (var fixture in body.FixtureList)
            {
                fixture.FreeHandle();
            }

            body.FreeHandle();
        }

        foreach (var joint in JointList)
        {
            joint.FreeHandle();
        }

        b2World_delete(Native);
    }
}
