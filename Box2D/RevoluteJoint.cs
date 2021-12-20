using Box2D.Math;

namespace Box2D;

using static Interop.NativeMethods;

public sealed class RevoluteJointDef : JointDef
{
    public Vec2 LocalAnchorA
    {
        get
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_localAnchorA(Native, ref value);
        }
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_localAnchorB(Native, ref value);
        }
    }

    public float ReferenceAngle
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJointDef_get_referenceAngle(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_referenceAngle(Native, value);
        }
    }

    public bool EnableLimit
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJointDef_get_enableLimit(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_enableLimit(Native, value);
        }
    }

    public float LowerAngle
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJointDef_get_lowerAngle(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_lowerAngle(Native, value);
        }
    }

    public float UpperAngle
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJointDef_get_upperAngle(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_upperAngle(Native, value);
        }
    }

    public bool EnableMotor
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJointDef_get_enableMotor(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_enableMotor(Native, value);
        }
    }

    public float MotorSpeed
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJointDef_get_motorSpeed(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_motorSpeed(Native, value);
        }
    }

    public float MaxMotorTorque
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJointDef_get_maxMotorTorque(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJointDef_set_maxMotorTorque(Native, value);
        }
    }

    public RevoluteJointDef()
    {
        var native = b2RevoluteJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vec2 anchor)
    {
        ThrowIfDisposed();
        b2RevoluteJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor);
    }

    private protected override void Dispose(bool disposing)
    {
        b2RevoluteJointDef_delete(Native);
    }
}

public sealed class RevoluteJoint : Joint
{
    public override JointType Type => JointType.Revolute;

    public Vec2 LocalAnchorA
    {
        get
        {
            ThrowIfDisposed();
            b2RevoluteJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            ThrowIfDisposed();
            b2RevoluteJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public float ReferenceAngle
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_GetReferenceAngle(Native);
        }
    }

    public float JointAngle
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_GetJointAngle(Native);
        }
    }

    public float JointSpeed
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_GetJointSpeed(Native);
        }
    }

    public bool LimitEnabled
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_IsLimitEnabled(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJoint_EnableLimit(Native, value);
        }
    }

    public float LowerLimit
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_GetLowerLimit(Native);
        }
    }

    public float UpperLimit
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_GetUpperLimit(Native);
        }
    }

    public bool MotorEnabled
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_IsMotorEnabled(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJoint_EnableMotor(Native, value);
        }
    }

    public float MotorSpeed
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_GetMotorSpeed(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJoint_SetMotorSpeed(Native, value);
        }
    }

    public float MaxMotorTorque
    {
        get
        {
            ThrowIfDisposed();
            return b2RevoluteJoint_GetMaxMotorTorque(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2RevoluteJoint_SetMaxMotorTorque(Native, value);
        }
    }

    public RevoluteJoint(object? userData) : base(userData)
    {
    }

    public void SetLimits(float lower, float upper)
    {
        ThrowIfDisposed();
        b2RevoluteJoint_SetLimits(Native, lower, upper);
    }

    public float GetMotorTorque(float invDt)
    {
        ThrowIfDisposed();
        return b2RevoluteJoint_GetMotorTorque(Native, invDt);
    }
}
