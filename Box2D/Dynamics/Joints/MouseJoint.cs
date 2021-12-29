using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class MouseJointDef : JointDef
{
    public Vector2 Target
    {
        get
        {
            b2MouseJointDef_get_target(Native, out var value);
            return value;
        }
        set => b2MouseJointDef_set_target(Native, ref value);
    }

    public float MaxForce
    {
        get => b2MouseJointDef_get_maxForce(Native);
        set => b2MouseJointDef_set_maxForce(Native, value);
    }

    public float Stiffness
    {
        get => b2MouseJointDef_get_stiffness(Native);
        set => b2MouseJointDef_set_stiffness(Native, value);
    }

    public float Damping
    {
        get => b2MouseJointDef_get_damping(Native);
        set => b2MouseJointDef_set_damping(Native, value);
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

    public Vector2 Target
    {
        get
        {
            b2MouseJoint_GetTarget(Native, out var value);
            return value;
        }
        set => b2MouseJoint_SetTarget(Native, ref value);
    }

    public float MaxForce
    {
        get => b2MouseJoint_GetMaxForce(Native);
        set => b2MouseJoint_SetMaxForce(Native, value);
    }

    public float Stiffness
    {
        get => b2MouseJoint_GetStiffness(Native);
        set => b2MouseJoint_SetStiffness(Native, value);
    }

    public float Damping
    {
        get => b2MouseJoint_GetDamping(Native);
        set => b2MouseJoint_SetDamping(Native, value);
    }

    public MouseJoint(object? userData) : base(userData)
    {
    }
}
