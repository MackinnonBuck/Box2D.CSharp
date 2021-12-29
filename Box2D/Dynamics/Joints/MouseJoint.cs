using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

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

    internal MouseJoint(object? userData) : base(userData)
    {
    }
}
