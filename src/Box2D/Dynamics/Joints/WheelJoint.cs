using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// A wheel joint. This joint provides two degrees of freedom: translation
/// along an axis fixed in body A and rotation in the plane. In other words, it is a point to
/// line constraint with a rotational motor and a linear spring/damper. The spring/damper is
/// initialized upon creation. This joint is designed for vehicle suspensions.
/// </summary>
public sealed class WheelJoint : Joint
{
    /// <inheritdoc/>
    public override JointType Type => JointType.Wheel;

    /// <summary>
    /// Gets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2WheelJoint_GetLocalAnchorA(Native, out var value);
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
            b2WheelJoint_GetLocalAnchorB(Native, out var value);
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
            b2WheelJoint_GetLocalAxisA(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the current joint translation, usually in meters.
    /// </summary>
    public float JointTranslation => b2WheelJoint_GetJointTranslation(Native);

    /// <summary>
    /// Gets the current joint linear speed, usually in meters per second.
    /// </summary>
    public float JointLinearSpeed => b2WheelJoint_GetJointLinearSpeed(Native);

    /// <summary>
    /// Gets the current joint angle in radians.
    /// </summary>
    public float JointAngle => b2WheelJoint_GetJointAngle(Native);

    /// <summary>
    /// Gets the current joint angular speed in radians per second.
    /// </summary>
    public float JointAngularSpeed => b2WheelJoint_GetJointAngularSpeed(Native);

    /// <summary>
    /// Gets or sets whether the joint limit is enabled.
    /// </summary>
    public bool LimitEnabled
    {
        get => b2WheelJoint_IsLimitEnabled(Native);
        set => b2WheelJoint_EnableLimit(Native, value);
    }

    /// <summary>
    /// Gets the lower joint translation limit, usually in meters.
    /// </summary>
    public float LowerLimit => b2WheelJoint_GetLowerLimit(Native);

    /// <summary>
    /// Gets the upper joint translation limit, usuallyin meters.
    /// </summary>
    public float UpperLimit => b2WheelJoint_GetUpperLimit(Native);

    /// <summary>
    /// Gets or sets whether the joint motor is enabled.
    /// </summary>
    public bool MotorEnabled
    {
        get => b2WheelJoint_IsMotorEnabled(Native);
        set => b2WheelJoint_EnableMotor(Native, value);
    }

    /// <summary>
    /// Gets or sets the joint motor speed, usually in radians per second.
    /// </summary>
    public float MotorSpeed
    {
        get => b2WheelJoint_GetMotorSpeed(Native);
        set => b2WheelJoint_SetMotorSpeed(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum motor torque, usually in N-m.
    /// </summary>
    public float MaxMotorTorque
    {
        get => b2WheelJoint_GetMaxMotorTorque(Native);
        set => b2WheelJoint_SetMaxMotorTorque(Native, value);
    }

    /// <summary>
    /// Gets or sets spring stiffness.
    /// </summary>
    public float Stiffness
    {
        get => b2WheelJoint_GetStiffness(Native);
        set => b2WheelJoint_SetStiffness(Native, value);
    }

    /// <summary>
    /// Gets or sets spring damping.
    /// </summary>
    public float Damping
    {
        get => b2WheelJoint_GetDamping(Native);
        set => b2WheelJoint_SetDamping(Native, value);
    }

    internal WheelJoint(object? userData) : base(userData)
    {
    }

    /// <summary>
    /// Sets the joint translation limits, usually in meters.
    /// </summary>
    public void SetLimits(float lower, float upper)
        => b2WheelJoint_SetLimits(Native, lower, upper);

    /// <summary>
    /// Gets the current motor torque given the inverse time step, usually in N-m.
    /// </summary>
    public float GetMotorTorque(float invDt)
        => b2WheelJoint_GetMotorTorque(Native, invDt);
}
