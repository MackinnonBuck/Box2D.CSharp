using Box2D.Core;
using Box2D.Core.Allocation;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// Holds all the data needed to construct a rigid body.
/// You can safely re-use body definitions. Shapes are added to a body after
/// construction.
/// </summary>
public sealed class BodyDef : Box2DDisposableObject, IBox2DRecyclableObject
{
    private static readonly IAllocator<BodyDef> _allocator = Allocator.Create<BodyDef>(static () => new());

    /// <summary>
    /// Gets or sets the application-specific body data.
    /// </summary>
    public object? UserData { get; set; }

    /// <summary>
    /// Gets or sets the type of the body.
    /// </summary>
    /// <remarks>
    /// Note: If a dynamic body would have zero mass, the mass is set to one.
    /// </remarks>
    public BodyType Type
    {
        get => b2BodyDef_get_type(Native);
        set => b2BodyDef_set_type(Native, value);
    }

    /// <summary>
    /// Gets or sets the world position of the body. Avoid creating bodies at the origin
    /// since this can lead to overlapping shapes.
    /// </summary>
    public Vector2 Position
    {
        get
        {
            b2BodyDef_get_position(Native, out var value);
            return value;
        }
        set => b2BodyDef_set_position(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the world angle of the body in radians.
    /// </summary>
    public float Angle
    {
        get => b2BodyDef_get_angle(Native);
        set => b2BodyDef_set_angle(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear velocity of the body's origin in world coordinates.
    /// </summary>
    public Vector2 LinearVelocity
    {
        get
        {
            b2BodyDef_get_linearVelocity(Native, out var value);
            return value;
        }
        set => b2BodyDef_set_linearVelocity(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the angular velocity of the body.
    /// </summary>
    public float AngularVelocity
    {
        get => b2BodyDef_get_angularVelocity(Native);
        set => b2BodyDef_set_angularVelocity(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear damping of the body.
    /// Linear damping is use to reduce the linear velocity. The damping parameter
    /// can be larger than 1.0f but the damping effect becomes sensitive to the
    /// time step when the damping parameter is large.
    /// Units are 1/time.
    /// </summary>
    public float LinearDamping
    {
        get => b2BodyDef_get_linearDamping(Native);
        set => b2BodyDef_set_linearDamping(Native, value);
    }

    /// <summary>
    /// Gets or sets the angular velocity of the body.
    /// </summary>
    public float AngularDamping
    {
        get => b2BodyDef_get_angularDamping(Native);
        set => b2BodyDef_set_angularDamping(Native, value);
    }

    /// <summary>
    /// Gets or sets whether this body should fall asleep.
    /// Defaults to <see langword="true"/>.
    /// </summary>
    public bool AllowSleep
    {
        get => b2BodyDef_get_allowSleep(Native);
        set => b2BodyDef_set_allowSleep(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the body is initially awake.
    /// </summary>
    public bool Awake
    {
        get => b2BodyDef_get_awake(Native);
        set => b2BodyDef_set_awake(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the body should be prevented from rotating.
    /// </summary>
    public bool FixedRotation
    {
        get => b2BodyDef_get_fixedRotation(Native);
        set => b2BodyDef_set_fixedRotation(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the body is a bullet.
    /// Is this a fast moving body that should be prevented from tunneling through
    /// other moving bodies? Note that all bodies are prevented from tunneling through
    /// kinematic and static bodies. This setting is only considered on dynamic bodies.
    /// </summary>
    public bool Bullet
    {
        get => b2BodyDef_get_bullet(Native);
        set => b2BodyDef_set_bullet(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the body starts out enabled.
    /// </summary>
    public bool Enabled
    {
        get => b2BodyDef_get_enabled(Native);
        set => b2BodyDef_set_enabled(Native, value);
    }

    /// <summary>
    /// Gets or sets the gravity scale applied to the body.
    /// </summary>
    public float GravityScale
    {
        get => b2BodyDef_get_gravityScale(Native);
        set => b2BodyDef_set_gravityScale(Native, value);
    }

    /// <summary>
    /// Creates a new <see cref="BodyDef"/> instance.
    /// </summary>
    public static BodyDef Create()
        => _allocator.Allocate();

    private BodyDef() : base(isUserOwned: true)
    {
        var native = b2BodyDef_new();
        Initialize(native);
    }

    bool IBox2DRecyclableObject.TryRecycle()
        => _allocator.TryRecycle(this);

    void IBox2DRecyclableObject.Reset()
    {
        UserData = null;
        b2BodyDef_reset(Native);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
        => b2BodyDef_delete(Native);
}
