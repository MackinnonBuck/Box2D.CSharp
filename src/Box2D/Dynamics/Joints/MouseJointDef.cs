using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// Mouse joint definition. This requires a world target point,
/// tuning parameters, and the time step.
/// </summary>
public sealed class MouseJointDef : JointDef
{
    /// <summary>
    /// Gets or sets the initial world target point. This is assumed to coincide
    /// with the body anchor initially.
    /// </summary>
    public Vector2 Target
    {
        get
        {
            b2MouseJointDef_get_target(Native, out var value);
            return value;
        }
        set => b2MouseJointDef_set_target(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the maximum constraint force that can be exerted
    /// to move the candidate body. Usually you will express
    /// as some multiple of the weight <c>(multiplier * mass * gravity)</c>.
    /// </summary>
    public float MaxForce
    {
        get => b2MouseJointDef_get_maxForce(Native);
        set => b2MouseJointDef_set_maxForce(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear stiffness in N/m.
    /// </summary>
    public float Stiffness
    {
        get => b2MouseJointDef_get_stiffness(Native);
        set => b2MouseJointDef_set_stiffness(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear damping in N*s/m.
    /// </summary>
    public float Damping
    {
        get => b2MouseJointDef_get_damping(Native);
        set => b2MouseJointDef_set_damping(Native, value);
    }

    /// <summary>
    /// Constructs a new <see cref="MouseJointDef"/> instance.
    /// </summary>
    public MouseJointDef()
    {
        var native = b2MouseJointDef_new();
        Initialize(native);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        b2MouseJointDef_delete(Native);
    }
}
