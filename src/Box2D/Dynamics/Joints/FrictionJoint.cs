using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// A friction joint. This is used for top-down friction.
/// It provides 2D translational friction and angular friction.
/// </summary>
public sealed class FrictionJoint : Joint
{
    public override JointType Type => JointType.Friction;

    /// <summary>
    /// Gets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2FrictionJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the local anchor point relative to body B's origin.
    /// </summary>
    public Vector2 LocalAnchorB
    {
        get
        {
            b2FrictionJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets or sets the maximum friction force in N.
    /// </summary>
    public float MaxForce
    {
        get => b2FrictionJoint_GetMaxForce(Native);
        set => b2FrictionJoint_SetMaxForce(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum friction torque in N*m.
    /// </summary>
    public float MaxTorque
    {
        get => b2FrictionJoint_GetMaxTorque(Native);
        set => b2FrictionJoint_SetMaxTorque(Native, value);
    }

    internal FrictionJoint(object? userData) : base(userData)
    {
    }
}
