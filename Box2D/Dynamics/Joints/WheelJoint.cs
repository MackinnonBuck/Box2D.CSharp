using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class WheelJoint : Joint
{
    public override JointType Type => JointType.Wheel;

    public Vector2 LocalAnchorA
    {
        get
        {
            b2WheelJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vector2 LocalAnchorB
    {
        get
        {
            b2WheelJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public Vector2 LocalAxisA
    {
        get
        {
            b2WheelJoint_GetLocalAxisA(Native, out var value);
            return value;
        }
    }

    public float JointTranslation => b2WheelJoint_GetJointTranslation(Native);

    public float JointLinearSpeed => b2WheelJoint_GetJointLinearSpeed(Native);

    public float JointAngle => b2WheelJoint_GetJointAngle(Native);

    public float JointAngularSpeed => b2WheelJoint_GetJointAngularSpeed(Native);

    public bool LimitEnabled
    {
        get => b2WheelJoint_IsLimitEnabled(Native);
        set => b2WheelJoint_EnableLimit(Native, value);
    }

    public float LowerLimit => b2WheelJoint_GetLowerLimit(Native);

    public float UpperLimit => b2WheelJoint_GetUpperLimit(Native);

    public bool MotorEnabled
    {
        get => b2WheelJoint_IsMotorEnabled(Native);
        set => b2WheelJoint_EnableMotor(Native, value);
    }

    public float MotorSpeed
    {
        get => b2WheelJoint_GetMotorSpeed(Native);
        set => b2WheelJoint_SetMotorSpeed(Native, value);
    }

    public float MaxMotorTorque
    {
        get => b2WheelJoint_GetMaxMotorTorque(Native);
        set => b2WheelJoint_SetMaxMotorTorque(Native, value);
    }

    public float Stiffness
    {
        get => b2WheelJoint_GetStiffness(Native);
        set => b2WheelJoint_SetStiffness(Native, value);
    }

    public float Damping
    {
        get => b2WheelJoint_GetDamping(Native);
        set => b2WheelJoint_SetDamping(Native, value);
    }

    internal WheelJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
        => b2WheelJoint_SetLimits(Native, lower, upper);

    public float GetMotorTorque(float invDt)
        => b2WheelJoint_GetMotorTorque(Native, invDt);
}
