using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// A prismatic joint. This joint provides one degree of freedom: translation
/// along an axis fixed in body A. Relative rotation is prevented. You can
/// use a joint limit to restrict the range of motion and a joint motor to
/// drive the motion or to model joint friction.
/// </summary>
public sealed class PrismaticJoint : Joint
{
    /// <inheritdoc/>
    public override JointType Type => JointType.Prismatic;

    /// <summary>
    /// Gets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2PrismaticJoint_GetLocalAnchorA(Native, out var value);
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
            b2PrismaticJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the local joint axis relative to body A.
    /// </summary>
    public Vector2 LocalAxisA
    {
        get
        {
            b2PrismaticJoint_GetLocalAxisA(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the reference angle.
    /// </summary>
    public float ReferenceAngle => b2PrismaticJoint_GetReferenceAngle(Native);

    /// <summary>
    /// Gets the current joint translation, usually in meters.
    /// </summary>
    public float JointTranslation => b2PrismaticJoint_GetJointTranslation(Native);

    /// <summary>
    /// Gets the current joint speed, usually in meters per second.
    /// </summary>
    public float JointSpeed => b2PrismaticJoint_GetJointSpeed(Native);

    /// <summary>
    /// Gets or sets whether the limit is enabled.
    /// </summary>
    public bool LimitEnabled
    {
        get => b2PrismaticJoint_IsLimitEnabled(Native);
        set => b2PrismaticJoint_EnableLimit(Native, value);
    }

    /// <summary>
    /// Gets the lower joint limit, usually in meters.
    /// </summary>
    public float LowerLimit => b2PrismaticJoint_GetLowerLimit(Native);

    /// <summary>
    /// Gets the upper joint limit, usually in meters.
    /// </summary>
    public float UpperLimit => b2PrismaticJoint_GetUpperLimit(Native);

    /// <summary>
    /// Gets or sets whether the joint motor is enabled.
    /// </summary>
    public bool MotorEnabled
    {
        get => b2PrismaticJoint_IsMotorEnabled(Native);
        set => b2PrismaticJoint_EnableMotor(Native, value);
    }

    /// <summary>
    /// Gets or sets the motor speed, usually in meters per second.
    /// </summary>
    public float MotorSpeed
    {
        get => b2PrismaticJoint_GetMotorSpeed(Native);
        set => b2PrismaticJoint_SetMotorSpeed(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum motor force, usually in N.
    /// </summary>
    public float MaxMotorForce
    {
        get => b2PrismaticJoint_GetMaxMotorForce(Native);
        set => b2PrismaticJoint_SetMaxMotorForce(Native, value);
    }

    internal PrismaticJoint(object? userData) : base(userData)
    {
    }

    /// <summary>
    /// Sets the joint limits, usually in meters.
    /// </summary>
    public void SetLimits(float lower, float upper)
        => b2PrismaticJoint_SetLimits(Native, lower, upper);

    /// <summary>
    /// Gets the current motor force given the inverse time step, usually in N.
    /// </summary>
    public float GetMotorForce(float invDt)
        => b2PrismaticJoint_GetMotorForce(Native, invDt);
}
