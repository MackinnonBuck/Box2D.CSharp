using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class DistanceJoint : Joint
{
    public override JointType Type => JointType.Distance;

    public Vector2 LocalAnchorA
    {
        get
        {
            b2DistanceJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vector2 LocalAnchorB
    {
        get
        {
            b2DistanceJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public float Length
    {
        get => b2DistanceJoint_GetLength(Native);
        set => b2DistanceJoint_SetLength(Native, value);
    }

    public float MinLength
    {
        get => b2DistanceJoint_GetMinLength(Native);
        set => b2DistanceJoint_SetMinLength(Native, value);
    }

    public float MaxLength
    {
        get => b2DistanceJoint_GetMaxLength(Native);
        set => b2DistanceJoint_SetMaxLength(Native, value);
    }

    public float CurrentLength => b2DistanceJoint_GetCurrentLength(Native);

    public float Stiffness
    {
        get => b2DistanceJoint_GetStiffness(Native);
        set => b2DistanceJoint_SetStiffness(Native, value);
    }

    public float Damping
    {
        get => b2DistanceJoint_GetDamping(Native);
        set => b2DistanceJoint_SetDamping(Native, value);
    }

    internal DistanceJoint(object? userData) : base(userData)
    {
    }
}
