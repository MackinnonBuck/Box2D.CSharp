namespace Box2D;

using static NativeMethods;

public sealed class DistanceJointDef : JointDef
{
    public Vec2 LocalAnchorA
    {
        get
        {
            b2DistanceJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2DistanceJointDef_set_localAnchorA(Native, ref value);
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            b2DistanceJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2DistanceJointDef_set_localAnchorB(Native, ref value);
    }

    public float Length
    {
        get => b2DistanceJointDef_get_length(Native);
        set => b2DistanceJointDef_set_length(Native, value);
    }

    public float MinLength
    {
        get => b2DistanceJointDef_get_minLength(Native);
        set => b2DistanceJointDef_set_minLength(Native, value);
    }

    public float MaxLength
    {
        get => b2DistanceJointDef_get_maxLength(Native);
        set => b2DistanceJointDef_set_maxLength(Native, value);
    }

    public float Stiffness
    {
        get => b2DistanceJointDef_get_stiffness(Native);
        set => b2DistanceJointDef_set_stiffness(Native, value);
    }

    public float Damping
    {
        get => b2DistanceJointDef_get_damping(Native);
        set => b2DistanceJointDef_set_damping(Native, value);
    }

    public DistanceJointDef()
    {
        var native = b2DistanceJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vec2 anchorA, Vec2 anchorB)
        => b2DistanceJointDef_Initialize(Native, bodyA.Native, bodyB.Native, anchorA, anchorB);

    protected override void Dispose(bool disposing)
        => b2DistanceJointDef_delete(Native);
}

public sealed class DistanceJoint : Joint
{
    public override JointType Type => JointType.Distance;

    public Vec2 LocalAnchorA
    {
        get
        {
            b2DistanceJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAnchorB
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

    public DistanceJoint(object? userData) : base(userData)
    {
    }
}
