using Box2D.Collision;
using Box2D.Core;
using Box2D.Drawing;
using Box2D.Dynamics.Callbacks;
using Box2D.Profiling;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// Manages all physics entities, dynamic simulation, and asynchronous queries.
/// </summary>
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

    /// <summary>
    /// Gets the world body list. With the returned body, use <see cref="Body.Next"/> to get
    /// the next body in the world list. A body whose <see cref="Body.IsNull"/> is <see langword="true"/>
    /// indicates the end of the list.
    /// </summary>
    public Body BodyList => new(b2World_GetBodyList(Native));

    /// <summary>
    /// Gets the world joint list. With the returned joint, use <see cref="Joint.Next"/> to get
    /// the next joint in the world list. A <see langword="null"/> joint indicates the end of the list.
    /// </summary>
    public Joint? JointList => Joint.FromIntPtr(b2World_GetJointList(Native));

    /// <summary>
    /// Gets the world contact list. With the returned contact, use <see cref="Contact.Next"/> to get
    /// the next contact in the world list. A contact whose <see cref="Contact.IsNull"/> is <see langword="true"/>
    /// indicates the end of the list.
    /// </summary>
    public Contact ContactList => new(b2World_GetContactList(Native));

    /// <summary>
    /// Gets or sets whether sleeping is enabled.
    /// </summary>
    public bool AllowSleeping
    {
        get => b2World_GetAllowSleeping(Native);
        set => b2World_SetAllowSleeping(Native, value);
    }

    /// <summary>
    /// Gets or sets whether warm starting is enabled.
    /// </summary>
    public bool WarmStarting
    {
        get => b2World_GetWarmStarting(Native);
        set => b2World_SetWarmStarting(Native, value);
    }

    /// <summary>
    /// Gets or sets whether continuous physics is enabled.
    /// </summary>
    public bool ContinuousPhysics
    {
        get => b2World_GetContinuousPhysics(Native);
        set => b2World_SetContinuousPhysics(Native, value);
    }

    /// <summary>
    /// Gets or sets whether single stepped continuous physics is enabled.
    /// </summary>
    public bool SubStepping
    {
        get => b2World_GetSubStepping(Native);
        set => b2World_SetSubStepping(Native, value);
    }

    /// <summary>
    /// Gets the number of broad-phase proxies.
    /// </summary>
    public int ProxyCount => b2World_GetProxyCount(Native);

    /// <summary>
    /// Gets the number of bodies.
    /// </summary>
    public int BodyCount => b2World_GetBodyCount(Native);

    /// <summary>
    /// Gets the number of joints.
    /// </summary>
    public int JointCount => b2World_GetJointCount(Native);

    /// <summary>
    /// Gets the number of contacts.
    /// </summary>
    public int ContactCount => b2World_GetContactCount(Native);

    /// <summary>
    /// Gets the height of the dynamic tree.
    /// </summary>
    public int TreeHeight => b2World_GetTreeHeight(Native);

    /// <summary>
    /// Gets the balance of the dynamic tree.
    /// </summary>
    public int TreeBalance => b2World_GetTreeBalance(Native);

    /// <summary>
    /// Gets the quality metric of the dynamic tree. The smaller the better.
    /// The minimum is 1.
    /// </summary>
    public float TreeQuality => b2World_GetTreeQuality(Native);

    /// <summary>
    /// Gets or sets the global gravity vector.
    /// </summary>
    public Vector2 Gravity
    {
        get
        {
            b2World_GetGravity(Native, out var value);
            return value;
        }
        set => b2World_SetGravity(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the drawing flags.
    /// </summary>
    public DrawFlags DrawFlags
    {
        get => _drawFlags;
        set
        {
            _drawFlags = value;
            _internalDraw?.SetFlags(value);
        }
    }

    /// <summary>
    /// Constructs a new <see cref="World"/> instance.
    /// </summary>
    /// <param name="gravity">The world gravity vector.</param>
    public World(Vector2 gravity) : base(isUserOwned: true)
    {
        var native = b2World_new(ref gravity);
        Initialize(native);

        _internalDestructionListener = new();
        _internalQueryCallback = new();
        _internalRayCastCallback = new();

        b2World_SetDestructionListener(Native, _internalDestructionListener.Native);
    }

    /// <summary>
    /// Registers a destruction listener.
    /// </summary>
    public void SetDestructionListener(IDestructionListener listener)
        => _internalDestructionListener.SetUserListener(listener);

    /// <summary>
    /// Registers a contact event listener.
    /// </summary>
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

    /// <summary>
    /// Registers a routine for debug drawing. The debug draw functions are
    /// called with <see cref="DebugDraw"/>.
    /// </summary>
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

    /// <summary>
    /// Creates a rigid body given a definition.
    /// </summary>
    public Body CreateBody(BodyDef def)
        => new(Native, def);

    /// <summary>
    /// Creates a rigid body.
    /// </summary>
    public Body CreateBody(BodyType type = BodyType.Static, Vector2 position = default, float angle = 0f, object? userData = null)
        => new(Native, type, ref position, angle, userData);

    /// <summary>
    /// Destroys a rigid body. No references to the body are retained.
    /// </summary>
    public void DestroyBody(Body body)
        => body.Destroy(Native);

    /// <summary>
    /// Creates a joint to constrain bodies together. No reference to the definition
    /// is retained. This may cause the connected bodies to cease colliding.
    /// </summary>
    public Joint CreateJoint(JointDef def)
        => Joint.Create(Native, def);

    /// <summary>
    /// Destroys a joint. This may cause the connected bodies to begin colliding.
    /// </summary>
    public void DestroyJoint(Joint joint)
    {
        b2World_DestroyJoint(Native, joint.Native);
        joint.Invalidate();
    }

    /// <summary>
    /// Takes a time step. This performs collision detection, integration, and constraint
    /// solution.
    /// </summary>
    /// <param name="timeStep">The amount of time to simulate. This should not vary.</param>
    /// <param name="velocityIterations">The number of iterations for the velocity constraint solver.</param>
    /// <param name="positionIterations">The number of iterations for the position constraint solver.</param>
    public void Step(float timeStep, int velocityIterations, int positionIterations)
        => b2World_Step(Native, timeStep, velocityIterations, positionIterations);

    /// <summary>
    /// Manually clear the force buffer on all bodies. By default, forces are cleared automatically
    /// after each call to <see cref="Step(float, int, int)"/>.
    /// The purpose of this function is to support sub-stepping. Sub-stepping is often used to maintain
    /// a fixed sized time step under a variable frame-rate.
    /// When you perform sub-stepping you will disable auto clearing of forces and instead call
    /// <see cref="ClearForces"/> after all sub-steps are complete in one pass of your game loop.
    /// </summary>
    public void ClearForces()
        => b2World_ClearForces(Native);

    /// <summary>
    /// Draws shapes and other debug draw data.
    /// </summary>
    public void DebugDraw()
        => b2World_DebugDraw(Native);

    /// <summary>
    /// Queries the world for all fixtures that potentially overlap the provided AABB.
    /// </summary>
    /// <param name="callback">A user-implemented callback instance.</param>
    /// <param name="aabb">The query box.</param>
    public void QueryAABB(IQueryCallback callback, AABB aabb)
        => _internalQueryCallback.QueryAABB(Native, callback, ref aabb);

    /// <summary>
    /// Ray-casts the world for all fixtures in the path of the ray. Your callback
    /// controls whether you get the closest point, any point, or n-points.
    /// The ray-cast ignores shapes that contain the starting point.
    /// </summary>
    /// <param name="callback">A user-implemented callback instance.</param>
    /// <param name="point1">The ray starting point.</param>
    /// <param name="point2">The ray ending point.</param>
    public void RayCast(IRayCastCallback callback, Vector2 point1, Vector2 point2)
        => _internalRayCastCallback.RayCast(Native, callback, ref point1, ref point2);

    /// <summary>
    /// Shifts the world origin. Useful for large worlds.
    /// The body shift formula is <c>position -= newOrigin</c>.
    /// </summary>
    /// <param name="newOrigin">The new origin with respect to the old origin.</param>
    public void ShiftOrigin(Vector2 newOrigin)
        => b2World_ShiftOrigin(Native, ref newOrigin);

    /// <summary>
    /// Gets the current profile.
    /// </summary>
    public void GetProfile(out Profile profile)
        => b2World_GetProfile(Native, out profile);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        var body = BodyList;

        while (!body.IsNull)
        {
            var nextBody = body.Next;
            var fixture = body.FixtureList;

            while (!fixture.IsNull)
            {
                var nextFixture = fixture.Next;
                fixture.Invalidate();
                fixture = nextFixture;
            }

            body.Invalidate();
            body = nextBody;
        }

        var joint = JointList;

        while (joint is not null)
        {
            var nextJoint = joint.Next;
            joint.Invalidate();
            joint = nextJoint;
        }

        b2World_delete(Native);

        _internalDestructionListener.Dispose();
        _internalQueryCallback.Dispose();
        _internalRayCastCallback.Dispose();
        _internalContactListener?.Dispose();
        _internalDraw?.Dispose();
    }
}
