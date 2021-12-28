using Box2D.Math;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class PrismaticJointDef : JointDef
{
    public Vec2 LocalAnchorA
    {
        get
        {
            b2PrismaticJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAnchorA(Native, ref value);
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            b2PrismaticJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAnchorB(Native, ref value);
    }

    public Vec2 LocalAxisA
    {
        get
        {
            b2PrismaticJointDef_get_localAxisA(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAxisA(Native, ref value);
    }

    public float ReferenceAngle
    {
        get => b2PrismaticJointDef_get_referenceAngle(Native);
        set => b2PrismaticJointDef_set_referenceAngle(Native, value);
    }

    public bool EnableLimit
    {
        get => b2PrismaticJointDef_get_enableLimit(Native);
        set => b2PrismaticJointDef_set_enableLimit(Native, value);
    }

    public float LowerTranslation
    {
        get => b2PrismaticJointDef_get_lowerTranslation(Native);
        set => b2PrismaticJointDef_set_lowerTranslation(Native, value);
    }

    public float UpperTranslation
    {
        get => b2PrismaticJointDef_get_upperTranslation(Native);
        set => b2PrismaticJointDef_set_upperTranslation(Native, value);
    }

    public bool EnableMotor
    {
        get => b2PrismaticJointDef_get_enableMotor(Native);
        set => b2PrismaticJointDef_set_enableMotor(Native, value);
    }

    public float MaxMotorForce
    {
        get => b2PrismaticJointDef_get_maxMotorForce(Native);
        set => b2PrismaticJointDef_set_maxMotorForce(Native, value);
    }

    public float MotorSpeed
    {
        get => b2PrismaticJointDef_get_motorSpeed(Native);
        set => b2PrismaticJointDef_set_motorSpeed(Native, value);
    }

    public PrismaticJointDef()
    {
        var native = b2PrismaticJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vec2 anchor, Vec2 axis)
        => b2PrismaticJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor, ref axis);

    protected override void Dispose(bool disposing)
        => b2PrismaticJointDef_delete(Native);
}

public sealed class PrismaticJoint : Joint
{
    public override JointType Type => JointType.Prismatic;

    public Vec2 LocalAnchorA
    {
        get
        {
            b2PrismaticJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            b2PrismaticJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAxisA
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

    public PrismaticJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
        => b2PrismaticJoint_SetLimits(Native, lower, upper);

    public float GetMotorForce(float invDt)
        => b2PrismaticJoint_GetMotorForce(Native, invDt);
}
