using Box2D.Collision;
using Box2D.Core;
using Box2D.Drawing;
using Box2D.Dynamics.Callbacks;
using Box2D.Profiling;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

public sealed class World : Box2DDisposableObject
{
    // Eagerly-initialized callbacks.
    // The overhead for initialization is better spent during the world's initialization
    // rather than when the callbacks are invoked for the first time.
    private readonly InternalDestructionListener _internalDestructionListener;
    private readonly InternalQueryCallback _internalQueryCallback;
    private readonly InternalRayCastCallback _internalRayCastCallback;

    // Lazily-initialized callbacks.
    // We don't want to add unnecessary overhead for managed debug drawing or contact listening
    // when the user has not provided custom callbacks.
    private InternalContactListener? _internalContactListener;
    private InternalDraw? _internalDraw;

    private DrawFlags _drawFlags;

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

    public Vector2 Gravity
    {
        get
        {
            b2World_GetGravity(Native, out var value);
            return value;
        }
        set => b2World_SetGravity(Native, ref value);
    }

    public DrawFlags DrawFlags
    {
        get => _drawFlags;
        set
        {
            _drawFlags = value;
            _internalDraw?.SetFlags(value);
        }
    }

    public World(Vector2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);
        Initialize(native);

        _internalDestructionListener = new();
        _internalQueryCallback = new();
        _internalRayCastCallback = new();

        b2World_SetDestructionListener(Native, _internalDestructionListener.Native);
    }

    public void SetDestructionListener(IDestructionListener listener)
        => _internalDestructionListener.SetUserListener(listener);

    public void SetContactListener(IContactListener listener)
    {
        if (_internalContactListener is null)
        {
            _internalContactListener = new(listener);
            b2World_SetContactListener(Native, _internalContactListener.Native);
        }
        else
        {
            _internalContactListener.SetUserListener(listener);
        }
    }

    public void SetDebugDraw(IDraw debugDraw)
    {
        if (_internalDraw is null)
        {
            _internalDraw = new(debugDraw, _drawFlags);
            b2World_SetDebugDraw(Native, _internalDraw.Native);
        }
        else
        {
            _internalDraw.SetUserDraw(debugDraw);
        }
    }

    public Body CreateBody(BodyDef def)
        => new(this, def);

    public Body CreateBody()
        => new(this);

    public void DestroyBody(Body body)
    {
        b2World_DestroyBody(Native, body.Native);
        body.Invalidate();
    }

    public Joint CreateJoint(JointDef def)
        => Joint.Create(Native, def);

    public void DestroyJoint(Joint joint)
    {
        b2World_DestroyJoint(Native, joint.Native);
        joint.Invalidate();
    }

    public void Step(float timeStep, int velocityIterations, int positionIterations)
        => b2World_Step(Native, timeStep, velocityIterations, positionIterations);

    public void ClearForces()
        => b2World_ClearForces(Native);

    public void DebugDraw()
        => b2World_DebugDraw(Native);

    public void QueryAABB(IQueryCallback callback, AABB aabb)
        => _internalQueryCallback.QueryAABB(Native, callback, ref aabb);

    public void RayCast(IRayCastCallback callback, Vector2 point1, Vector2 point2)
        => _internalRayCastCallback.RayCast(Native, callback, ref point1, ref point2);

    public void ShiftOrigin(Vector2 newOrigin)
        => b2World_ShiftOrigin(Native, ref newOrigin);

    public void GetProfile(out Profile profile)
        => b2World_GetProfile(Native, out profile);

    protected override void Dispose(bool disposing)
    {
        foreach (var body in BodyList)
        {
            foreach (var fixture in body.FixtureList)
            {
                fixture.Invalidate();
            }

            body.Invalidate();
        }

        foreach (var joint in JointList)
        {
            joint.Invalidate();
        }

        b2World_delete(Native);

        _internalDestructionListener.Dispose();
        _internalQueryCallback.Dispose();
        _internalRayCastCallback.Dispose();
        _internalContactListener?.Dispose();
        _internalDraw?.Dispose();
    }
}
