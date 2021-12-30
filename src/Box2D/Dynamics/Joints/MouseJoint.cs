using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// <para>
/// A mouse joint is used to make a point on a body track a
/// specified world point. This a soft constraint with a maximum
/// force. This allows the constraint to stretch and without
/// applying huge forces.
/// </para>
/// <para>
/// NOTE: this joint is not documented in the manual because it was
/// developed to be used in the testbed. If you want to learn how to
/// use the mouse joint, look at the testbed.
/// </para>
/// </summary>
public sealed class MouseJoint : Joint
{
    /// <inheritdoc/>
    public override JointType Type => JointType.Mouse;

    /// <summary>
    /// Gets or sets the target point.
    /// </summary>
    public Vector2 Target
    {
        get
        {
            b2MouseJoint_GetTarget(Native, out var value);
            return value;
        }
        set => b2MouseJoint_SetTarget(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the maximum force in Newtons.
    /// </summary>
    public float MaxForce
    {
        get => b2MouseJoint_GetMaxForce(Native);
        set => b2MouseJoint_SetMaxForce(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear stiffness in N/m.
    /// </summary>
    public float Stiffness
    {
        get => b2MouseJoint_GetStiffness(Native);
        set => b2MouseJoint_SetStiffness(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear damping in N*s/m.
    /// </summary>
    public float Damping
    {
        get => b2MouseJoint_GetDamping(Native);
        set => b2MouseJoint_SetDamping(Native, value);
    }

    internal MouseJoint(object? userData) : base(userData)
    {
    }
}
