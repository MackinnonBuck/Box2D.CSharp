namespace Box2D;

using static NativeMethods;

public sealed class RevoluteJointDef : JointDef
{
    public Vec2 LocalAnchorA
    {
        get
        {
            b2RevoluteJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2RevoluteJointDef_set_localAnchorA(Native, ref value);
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            b2RevoluteJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2RevoluteJointDef_set_localAnchorB(Native, ref value);
    }

    public float ReferenceAngle
    {
        get => b2RevoluteJointDef_get_referenceAngle(Native);
        set => b2RevoluteJointDef_set_referenceAngle(Native, value);
    }

    public bool EnableLimit
    {
        get => b2RevoluteJointDef_get_enableLimit(Native);
        set => b2RevoluteJointDef_set_enableLimit(Native, value);
    }

    public float LowerAngle
    {
        get => b2RevoluteJointDef_get_lowerAngle(Native);
        set => b2RevoluteJointDef_set_lowerAngle(Native, value);
    }

    public float UpperAngle
    {
        get => b2RevoluteJointDef_get_upperAngle(Native);
        set => b2RevoluteJointDef_set_upperAngle(Native, value);
    }

    public bool EnableMotor
    {
        get => b2RevoluteJointDef_get_enableMotor(Native);
        set => b2RevoluteJointDef_set_enableMotor(Native, value);
    }

    public float MotorSpeed
    {
        get => b2RevoluteJointDef_get_motorSpeed(Native);
        set => b2RevoluteJointDef_set_motorSpeed(Native, value);
    }

    public float MaxMotorTorque
    {
        get => b2RevoluteJointDef_get_maxMotorTorque(Native);
        set => b2RevoluteJointDef_set_maxMotorTorque(Native, value);
    }

    public RevoluteJointDef()
    {
        var native = b2RevoluteJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vec2 anchor)
        => b2RevoluteJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor);

    protected override void Dispose(bool disposing)
        => b2RevoluteJointDef_delete(Native);
}

public sealed class RevoluteJoint : Joint
{
    public override JointType Type => JointType.Revolute;

    public Vec2 LocalAnchorA
    {
        get
        {
            b2RevoluteJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAnchorB
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

    public RevoluteJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
        => b2RevoluteJoint_SetLimits(Native, lower, upper);

    public float GetMotorTorque(float invDt)
        => b2RevoluteJoint_GetMotorTorque(Native, invDt);
}
