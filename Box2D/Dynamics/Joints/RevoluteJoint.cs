using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class RevoluteJoint : Joint
{
    public override JointType Type => JointType.Revolute;

    public Vector2 LocalAnchorA
    {
        get
        {
            b2RevoluteJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vector2 LocalAnchorB
    {
        get
        {
            b2RevoluteJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public float ReferenceAngle => b2RevoluteJoint_GetReferenceAngle(Native);

    public float JointAngle => b2RevoluteJoint_GetJointAngle(Native);

    public float JointSpeed => b2RevoluteJoint_GetJointSpeed(Native);

    public bool LimitEnabled
    {
        get => b2RevoluteJoint_IsLimitEnabled(Native);
        set => b2RevoluteJoint_EnableLimit(Native, value);
    }

    public float LowerLimit => b2RevoluteJoint_GetLowerLimit(Native);

    public float UpperLimit => b2RevoluteJoint_GetUpperLimit(Native);

    public bool MotorEnabled
    {
        get => b2RevoluteJoint_IsMotorEnabled(Native);
        set => b2RevoluteJoint_EnableMotor(Native, value);
    }

    public float MotorSpeed
    {
        get => b2RevoluteJoint_GetMotorSpeed(Native);
        set => b2RevoluteJoint_SetMotorSpeed(Native, value);
    }

    public float MaxMotorTorque
    {
        get => b2RevoluteJoint_GetMaxMotorTorque(Native);
        set => b2RevoluteJoint_SetMaxMotorTorque(Native, value);
    }

    internal RevoluteJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
        => b2RevoluteJoint_SetLimits(Native, lower, upper);

    public float GetMotorTorque(float invDt)
        => b2RevoluteJoint_GetMotorTorque(Native, invDt);
}
