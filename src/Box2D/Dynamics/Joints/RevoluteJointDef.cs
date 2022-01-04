using Box2D.Core.Allocation;
using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// <para>
/// Revolute joint definition. This requires defining an anchor point where the
/// bodies are joined. The definition uses local anchor points so that the
/// initial configuration can violate the constraint slightly. You also need to
/// specify the initial relative angle for joint limits. This helps when saving
/// and loading a game.
/// </para>
/// <para>
/// The local anchor points are measured from the body's origin
/// rather than the center of mass because:
/// <list type="number">
/// <item>You might not know where the center of mass will be.</item>
/// <item>If you add/remove shapes from a body and recompute the mass, the joints will be broken.</item>
/// </list>
/// </para>
/// </summary>
public sealed class RevoluteJointDef : JointDef
{
    private static readonly IAllocator<RevoluteJointDef> _allocator = Allocator.Create<RevoluteJointDef>(static () => new());

    /// <summary>
    /// Gets or sets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2RevoluteJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2RevoluteJointDef_set_localAnchorA(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local anchor point relative to body B's origin.
    /// </summary>
    public Vector2 LocalAnchorB
    {
        get
        {
            b2RevoluteJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2RevoluteJointDef_set_localAnchorB(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the reference angle in radians.
    /// </summary>
    public float ReferenceAngle
    {
        get => b2RevoluteJointDef_get_referenceAngle(Native);
        set => b2RevoluteJointDef_set_referenceAngle(Native, value);
    }

    /// <summary>
    /// Gets or sets whether joint limits are enabled.
    /// </summary>
    public bool EnableLimit
    {
        get => b2RevoluteJointDef_get_enableLimit(Native);
        set => b2RevoluteJointDef_set_enableLimit(Native, value);
    }

    /// <summary>
    /// Gets or sets the lower angle for the joint limit (radians).
    /// </summary>
    public float LowerAngle
    {
        get => b2RevoluteJointDef_get_lowerAngle(Native);
        set => b2RevoluteJointDef_set_lowerAngle(Native, value);
    }

    /// <summary>
    /// Gets or sets the upper angle for the joint limit (radians).
    /// </summary>
    public float UpperAngle
    {
        get => b2RevoluteJointDef_get_upperAngle(Native);
        set => b2RevoluteJointDef_set_upperAngle(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the joint motor is enabled.
    /// </summary>
    public bool EnableMotor
    {
        get => b2RevoluteJointDef_get_enableMotor(Native);
        set => b2RevoluteJointDef_set_enableMotor(Native, value);
    }

    /// <summary>
    /// Gets or sets the desired motor speed in radians per second.
    /// </summary>
    public float MotorSpeed
    {
        get => b2RevoluteJointDef_get_motorSpeed(Native);
        set => b2RevoluteJointDef_set_motorSpeed(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum motor torque used to achieve the desired motor speed.
    /// Usually in N-m.
    /// </summary>
    public float MaxMotorTorque
    {
        get => b2RevoluteJointDef_get_maxMotorTorque(Native);
        set => b2RevoluteJointDef_set_maxMotorTorque(Native, value);
    }

    /// <summary>
    /// Creates a new <see cref="RevoluteJointDef"/> instance.
    /// </summary>
    public static RevoluteJointDef Create()
        => _allocator.Allocate();

    private RevoluteJointDef()
    {
        var native = b2RevoluteJointDef_new();
        Initialize(native);
    }

    /// <summary>
    /// Initialize the bodies, anchors, and reference angle using a world anchor point.
    /// </summary>
    public void Initialize(Body bodyA, Body bodyB, Vector2 anchor)
        => b2RevoluteJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor);

    private protected override bool TryRecycle()
        => _allocator.TryRecycle(this);

    private protected override void Reset()
        => b2RevoluteJointDef_reset(Native);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
        => b2RevoluteJointDef_delete(Native);
}
