namespace Box2D;

using static NativeMethods;

public sealed class MouseJointDef : JointDef
{
    public Vec2 Target
    {
        get
        {
            ThrowIfDisposed();
            b2MouseJointDef_get_target(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJointDef_set_target(Native, ref value);
        }
    }

    public float MaxForce
    {
        get
        {
            ThrowIfDisposed();
            return b2MouseJointDef_get_maxForce(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJointDef_set_maxForce(Native, value);
        }
    }

    public float Stiffness
    {
        get
        {
            ThrowIfDisposed();
            return b2MouseJointDef_get_stiffness(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJointDef_set_stiffness(Native, value);
        }
    }

    public float Damping
    {
        get
        {
            ThrowIfDisposed();
            return b2MouseJointDef_get_damping(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJointDef_set_damping(Native, value);
        }
    }

    public MouseJointDef()
    {
        var native = b2MouseJointDef_new();
        Initialize(native);
    }

    protected override void Dispose(bool disposing)
    {
        b2MouseJointDef_delete(Native);
    }
}

public sealed class MouseJoint : Joint
{
    public override JointType Type => JointType.Mouse;

    public Vec2 Target
    {
        get
        {
            ThrowIfDisposed();
            b2MouseJoint_GetTarget(Native, out var value);
            return value;
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJoint_SetTarget(Native, ref value);
        }
    }

    public float MaxForce
    {
        get
        {
            ThrowIfDisposed();
            return b2MouseJoint_GetMaxForce(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJoint_SetMaxForce(Native, value);
        }
    }

    public float Stiffness
    {
        get
        {
            ThrowIfDisposed();
            return b2MouseJoint_GetStiffness(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJoint_SetStiffness(Native, value);
        }
    }

    public float Damping
    {
        get
        {
            ThrowIfDisposed();
            return b2MouseJoint_GetDamping(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2MouseJoint_SetDamping(Native, value);
        }
    }

    public MouseJoint(object? userData) : base(userData)
    {
    }
}
