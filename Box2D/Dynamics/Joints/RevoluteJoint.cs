using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// A revolute joint constrains two bodies to share a common point while they
/// are free to rotate about the point. The relative rotation about the shared
/// point is the joint angle. You can limit the relative rotation with
/// a joint limit that specifies a lower and upper angle. You can use a motor
/// to drive the relative rotation about the shared point. A maximum motor torque
/// is provided so that infinite forces are not generated.
/// </summary>
public sealed class RevoluteJoint : Joint
{
    /// <inheritdoc/>
    public override JointType Type => JointType.Revolute;

    /// <summary>
    /// Gets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2RevoluteJoint_GetLocalAnchorA(Native, out var value);
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
            b2RevoluteJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the reference angle.
    /// </summary>
    public float ReferenceAngle => b2RevoluteJoint_GetReferenceAngle(Native);

    /// <summary>
    /// Gets the current joint angle in radians.
    /// </summary>
    public float JointAngle => b2RevoluteJoint_GetJointAngle(Native);

    /// <summary>
    /// Gets the current joint speed in radians per second.
    /// </summary>
    public float JointSpeed => b2RevoluteJoint_GetJointSpeed(Native);

    /// <summary>
    /// Gets or sets whether the joint limit is enabled.
    /// </summary>
    public bool LimitEnabled
    {
        get => b2RevoluteJoint_IsLimitEnabled(Native);
        set => b2RevoluteJoint_EnableLimit(Native, value);
    }

    /// <summary>
    /// Gets the lower joint limit in radians.
    /// </summary>
    public float LowerLimit => b2RevoluteJoint_GetLowerLimit(Native);

    /// <summary>
    /// Gets the upper joint limit in radians.
    /// </summary>
    public float UpperLimit => b2RevoluteJoint_GetUpperLimit(Native);

    /// <summary>
    /// Gets or sets whether the joint motor is enabled.
    /// </summary>
    public bool MotorEnabled
    {
        get => b2RevoluteJoint_IsMotorEnabled(Native);
        set => b2RevoluteJoint_EnableMotor(Native, value);
    }

    /// <summary>
    /// Gets or sets the motor speed in radians per second.
    /// </summary>
    public float MotorSpeed
    {
        get => b2RevoluteJoint_GetMotorSpeed(Native);
        set => b2RevoluteJoint_SetMotorSpeed(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum motor torque, usually in N-m.
    /// </summary>
    public float MaxMotorTorque
    {
        get => b2RevoluteJoint_GetMaxMotorTorque(Native);
        set => b2RevoluteJoint_SetMaxMotorTorque(Native, value);
    }

    internal RevoluteJoint(object? userData) : base(userData)
    {
    }

    /// <summary>
    /// Sets the joint limits in radians.
    /// </summary>
    public void SetLimits(float lower, float upper)
        => b2RevoluteJoint_SetLimits(Native, lower, upper);

    /// <summary>
    /// Gets the current motor torque given the inverse time step.
    /// </summary>
    public float GetMotorTorque(float invDt)
        => b2RevoluteJoint_GetMotorTorque(Native, invDt);
}
