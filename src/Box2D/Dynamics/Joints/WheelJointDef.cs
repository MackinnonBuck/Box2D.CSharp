using Box2D.Core.Allocation;
using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// Wheel joint definition. This requires defining a line of
/// motion using an axis and an anchor point. The definition uses local
/// anchor points and a local axis so that the initial configuration
/// can violate the constraint slightly. The joint translation is zero
/// when the local anchor points coincide in world space. Using local
/// anchors and a local axis helps when saving and loading a game.
/// </summary>
public sealed class WheelJointDef : JointDef
{
    private static readonly IAllocator<WheelJointDef> _allocator = Allocator.Create<WheelJointDef>(static () => new());

    /// <summary>
    /// Gets or sets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2WheelJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAnchorA(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local anchor point relative to body B's origin.
    /// </summary>
    public Vector2 LocalAnchorB
    {
        get
        {
            b2WheelJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAnchorB(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local translation axis in body A.
    /// </summary>
    public Vector2 LocalAxisA
    {
        get
        {
            b2WheelJointDef_get_localAxisA(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAxisA(Native, ref value);
    }

    /// <summary>
    /// Gets or sets whether the joint limit is enabled.
    /// </summary>
    public bool EnableLimit
    {
        get => b2WheelJointDef_get_enableLimit(Native);
        set => b2WheelJointDef_set_enableLimit(Native, value);
    }

    /// <summary>
    /// Gets or sets the lower translation limit, usually in meters.
    /// </summary>
    public float LowerTranslation
    {
        get => b2WheelJointDef_get_lowerTranslation(Native);
        set => b2WheelJointDef_set_lowerTranslation(Native, value);
    }

    /// <summary>
    /// Gets or sets the upper translation limit, usually in meters.
    /// </summary>
    public float UpperTranslation
    {
        get => b2WheelJointDef_get_upperTranslation(Native);
        set => b2WheelJointDef_set_upperTranslation(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the joint motor is enabled.
    /// </summary>
    public bool EnableMotor
    {
        get => b2WheelJointDef_get_enableMotor(Native);
        set => b2WheelJointDef_set_enableMotor(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum motor torque, usually in N-m.
    /// </summary>
    public float MaxMotorTorque
    {
        get => b2WheelJointDef_get_maxMotorTorque(Native);
        set => b2WheelJointDef_set_maxMotorTorque(Native, value);
    }

    /// <summary>
    /// Gets or sets the desired motor speed in radians per second.
    /// </summary>
    public float MotorSpeed
    {
        get => b2WheelJointDef_get_motorSpeed(Native);
        set => b2WheelJointDef_set_motorSpeed(Native, value);
    }

    /// <summary>
    /// Gets or sets the suspension stiffness. Typically in units N/m.
    /// </summary>
    public float Stiffness
    {
        get => b2WheelJointDef_get_stiffness(Native);
        set => b2WheelJointDef_set_stiffness(Native, value);
    }

    /// <summary>
    /// Gets or sets the suspension damping. Typically in units of N*s/m.
    /// </summary>
    public float Damping
    {
        get => b2WheelJointDef_get_damping(Native);
        set => b2WheelJointDef_set_damping(Native, value);
    }

    /// <summary>
    /// Creates a new <see cref="WheelJointDef"/> instance.
    /// </summary>
    public static WheelJointDef Create()
        => _allocator.Allocate();

    private WheelJointDef()
    {
        var native = b2WheelJointDef_new();
        Initialize(native);
    }

    /// <summary>
    /// Initializes the bodies, anchors, axis, and reference angle using the world anchor and world axis.
    /// </summary>
    public void Initialize(Body bodyA, Body bodyB, Vector2 anchor, Vector2 axis)
        => b2WheelJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor, ref axis);

    private protected override bool TryRecycle()
        => _allocator.TryRecycle(this);

    private protected override void Reset()
        => b2WheelJointDef_reset(Native);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
        => b2WheelJointDef_delete(Native);
}
