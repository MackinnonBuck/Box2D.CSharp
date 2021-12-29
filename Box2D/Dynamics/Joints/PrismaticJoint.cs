using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class PrismaticJoint : Joint
{
    public override JointType Type => JointType.Prismatic;

    public Vector2 LocalAnchorA
    {
        get
        {
            b2PrismaticJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vector2 LocalAnchorB
    {
        get
        {
            b2PrismaticJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public Vector2 LocalAxisA
    {
        get
        {
            b2PrismaticJoint_GetLocalAxisA(Native, out var value);
            return value;
        }
    }

    public float ReferenceAngle => b2PrismaticJoint_GetReferenceAngle(Native);

    public float JointTranslation => b2PrismaticJoint_GetJointTranslation(Native);

    public float JointSpeed => b2PrismaticJoint_GetJointSpeed(Native);

    public bool LimitEnabled
    {
        get => b2PrismaticJoint_IsLimitEnabled(Native);
        set => b2PrismaticJoint_EnableLimit(Native, value);
    }

    public float LowerLimit => b2PrismaticJoint_GetLowerLimit(Native);

    public float UpperLimit => b2PrismaticJoint_GetUpperLimit(Native);

    public bool MotorEnabled
    {
        get => b2PrismaticJoint_IsMotorEnabled(Native);
        set => b2PrismaticJoint_EnableMotor(Native, value);
    }

    public float MotorSpeed
    {
        get => b2PrismaticJoint_GetMotorSpeed(Native);
        set => b2PrismaticJoint_SetMotorSpeed(Native, value);
    }

    public float MaxMotorForce
    {
        get => b2PrismaticJoint_GetMaxMotorForce(Native);
        set => b2PrismaticJoint_SetMaxMotorForce(Native, value);
    }

    internal PrismaticJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
        => b2PrismaticJoint_SetLimits(Native, lower, upper);

    public float GetMotorForce(float invDt)
        => b2PrismaticJoint_GetMotorForce(Native, invDt);
}
