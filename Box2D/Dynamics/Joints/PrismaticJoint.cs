namespace Box2D;

using static NativeMethods;

public sealed class PrismaticJointDef : JointDef
{
    public Vec2 LocalAnchorA
    {
        get
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_localAnchorA(Native, ref value);
        }
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_localAnchorB(Native, ref value);
        }
    }

    public Vec2 LocalAxisA
    {
        get
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_get_localAxisA(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_localAxisA(Native, ref value);
        }
    }

    public float ReferenceAngle
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJointDef_get_referenceAngle(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_referenceAngle(Native, value);
        }
    }

    public bool EnableLimit
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJointDef_get_enableLimit(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_enableLimit(Native, value);
        }
    }

    public float LowerTranslation
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJointDef_get_lowerTranslation(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_lowerTranslation(Native, value);
        }
    }

    public float UpperTranslation
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJointDef_get_upperTranslation(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_upperTranslation(Native, value);
        }
    }

    public bool EnableMotor
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJointDef_get_enableMotor(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_enableMotor(Native, value);
        }
    }

    public float MaxMotorForce
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJointDef_get_maxMotorForce(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_maxMotorForce(Native, value);
        }
    }

    public float MotorSpeed
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJointDef_get_motorSpeed(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJointDef_set_motorSpeed(Native, value);
        }
    }

    public PrismaticJointDef()
    {
        var native = b2PrismaticJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vec2 anchor, Vec2 axis)
    {
        ThrowIfDisposed();
        b2PrismaticJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor, ref axis);
    }

    private protected override void Dispose(bool disposing)
    {
        b2PrismaticJointDef_delete(Native);
    }
}

public sealed class PrismaticJoint : Joint
{
    public override JointType Type => JointType.Prismatic;

    public Vec2 LocalAnchorA
    {
        get
        {
            ThrowIfDisposed();
            b2PrismaticJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            ThrowIfDisposed();
            b2PrismaticJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAxisA
    {
        get
        {
            ThrowIfDisposed();
            b2PrismaticJoint_GetLocalAxisA(Native, out var value);
            return value;
        }
    }

    public float ReferenceAngle
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_GetReferenceAngle(Native);
        }
    }

    public float JointTranslation
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_GetJointTranslation(Native);
        }
    }

    public float JointSpeed
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_GetJointSpeed(Native);
        }
    }

    public bool LimitEnabled
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_IsLimitEnabled(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJoint_EnableLimit(Native, value);
        }
    }

    public float LowerLimit
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_GetLowerLimit(Native);
        }
    }

    public float UpperLimit
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_GetUpperLimit(Native);
        }
    }

    public bool MotorEnabled
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_IsMotorEnabled(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJoint_EnableMotor(Native, value);
        }
    }

    public float MotorSpeed
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_GetMotorSpeed(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJoint_SetMotorSpeed(Native, value);
        }
    }

    public float MaxMotorForce
    {
        get
        {
            ThrowIfDisposed();
            return b2PrismaticJoint_GetMaxMotorForce(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2PrismaticJoint_SetMaxMotorForce(Native, value);
        }
    }

    public PrismaticJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
    {
        ThrowIfDisposed();
        b2PrismaticJoint_SetLimits(Native, lower, upper);
    }

    public float GetMotorForce(float invDt)
    {
        ThrowIfDisposed();
        return b2PrismaticJoint_GetMotorForce(Native, invDt);
    }
}
