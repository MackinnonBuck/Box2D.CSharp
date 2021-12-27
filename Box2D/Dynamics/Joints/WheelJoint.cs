using Box2D.Math;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class WheelJointDef : JointDef
{
    public Vec2 LocalAnchorA
    {
        get
        {
            b2WheelJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAnchorA(Native, ref value);
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            b2WheelJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAnchorB(Native, ref value);
    }

    public Vec2 LocalAxisA
    {
        get
        {
            b2WheelJointDef_get_localAxisA(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAxisA(Native, ref value);
    }

    public bool EnableLimit
    {
        get => b2WheelJointDef_get_enableLimit(Native);
        set => b2WheelJointDef_set_enableLimit(Native, value);
    }

    public float LowerTranslation
    {
        get => b2WheelJointDef_get_lowerTranslation(Native);
        set => b2WheelJointDef_set_lowerTranslation(Native, value);
    }

    public float UpperTranslation
    {
        get => b2WheelJointDef_get_upperTranslation(Native);
        set => b2WheelJointDef_set_upperTranslation(Native, value);
    }

    public bool EnableMotor
    {
        get => b2WheelJointDef_get_enableMotor(Native);
        set => b2WheelJointDef_set_enableMotor(Native, value);
    }

    public float MaxMotorTorque
    {
        get => b2WheelJointDef_get_maxMotorTorque(Native);
        set => b2WheelJointDef_set_maxMotorTorque(Native, value);
    }

    public float MotorSpeed
    {
        get => b2WheelJointDef_get_motorSpeed(Native);
        set => b2WheelJointDef_set_motorSpeed(Native, value);
    }

    public float Stiffness
    {
        get => b2WheelJointDef_get_stiffness(Native);
        set => b2WheelJointDef_set_stiffness(Native, value);
    }

    public float Damping
    {
        get => b2WheelJointDef_get_damping(Native);
        set => b2WheelJointDef_set_damping(Native, value);
    }

    public WheelJointDef()
    {
        var native = b2WheelJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vec2 anchor, Vec2 axis)
        => b2WheelJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor, ref axis);

    protected override void Dispose(bool disposing)
        => b2WheelJointDef_delete(Native);
}

public sealed class WheelJoint : Joint
{
    public override JointType Type => JointType.Wheel;

    public Vec2 LocalAnchorA
    {
        get
        {
            b2WheelJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            b2WheelJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAxisA
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

    public WheelJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
        => b2WheelJoint_SetLimits(Native, lower, upper);

    public float GetMotorTorque(float invDt)
        => b2WheelJoint_GetMotorTorque(Native, invDt);
}
