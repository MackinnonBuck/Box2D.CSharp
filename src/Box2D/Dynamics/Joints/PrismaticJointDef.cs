using Box2D.Core.Allocation;
using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// Prismatic joint definition. This requires defining a line of
/// motion using an axis and an anchor point. The definition uses local
/// anchor points and a local axis so that the initial configuration
/// can violate the constraint slightly. The joint translation is zero
/// when the local anchor points coincide in world space. Using local
/// anchors and a local axis helps when saving and loading a game.
/// </summary>
public sealed class PrismaticJointDef : JointDef
{
    private static readonly IAllocator<PrismaticJointDef> _allocator = Allocator.Create<PrismaticJointDef>(static () => new());

    /// <summary>
    /// Gets or sets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2PrismaticJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAnchorA(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local anchor point relative to body B's origin.
    /// </summary>
    public Vector2 LocalAnchorB
    {
        get
        {
            b2PrismaticJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAnchorB(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local translation unit axis in body A.
    /// </summary>
    public Vector2 LocalAxisA
    {
        get
        {
            b2PrismaticJointDef_get_localAxisA(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAxisA(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the constrained angle between the bodies.
    /// </summary>
    public float ReferenceAngle
    {
        get => b2PrismaticJointDef_get_referenceAngle(Native);
        set => b2PrismaticJointDef_set_referenceAngle(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the joint limit is enabled.
    /// </summary>
    public bool EnableLimit
    {
        get => b2PrismaticJointDef_get_enableLimit(Native);
        set => b2PrismaticJointDef_set_enableLimit(Native, value);
    }

    /// <summary>
    /// Gets or sets the lower translation limit, usually in meters.
    /// </summary>
    public float LowerTranslation
    {
        get => b2PrismaticJointDef_get_lowerTranslation(Native);
        set => b2PrismaticJointDef_set_lowerTranslation(Native, value);
    }

    /// <summary>
    /// Gets or sets the upper translation limit, usually in meters.
    /// </summary>
    public float UpperTranslation
    {
        get => b2PrismaticJointDef_get_upperTranslation(Native);
        set => b2PrismaticJointDef_set_upperTranslation(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the joint motor is enabled.
    /// </summary>
    public bool EnableMotor
    {
        get => b2PrismaticJointDef_get_enableMotor(Native);
        set => b2PrismaticJointDef_set_enableMotor(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum motor force, usually in N-m.
    /// </summary>
    public float MaxMotorForce
    {
        get => b2PrismaticJointDef_get_maxMotorForce(Native);
        set => b2PrismaticJointDef_set_maxMotorForce(Native, value);
    }

    /// <summary>
    /// Gets or sets the desired motor speed in radians per second.
    /// </summary>
    public float MotorSpeed
    {
        get => b2PrismaticJointDef_get_motorSpeed(Native);
        set => b2PrismaticJointDef_set_motorSpeed(Native, value);
    }

    /// <summary>
    /// Creates a new <see cref="PrismaticJointDef"/> instance.
    /// </summary>
    public static PrismaticJointDef Create()
        => _allocator.Allocate();

    private PrismaticJointDef()
    {
        var native = b2PrismaticJointDef_new();
        Initialize(native);
    }

    /// <summary>
    /// Initializes the bodies, anchors, axis, and reference angle using the world anchor
    /// and unit world axis.
    /// </summary>
    public void Initialize(Body bodyA, Body bodyB, Vector2 anchor, Vector2 axis)
        => b2PrismaticJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor, ref axis);

    private protected override bool TryRecycle()
        => _allocator.TryRecycle(this);

    private protected override void Reset()
        => b2PrismaticJointDef_reset(Native);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
        => b2PrismaticJointDef_delete(Native);
}
