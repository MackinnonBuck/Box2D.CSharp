using Box2D.Math;

namespace Box2D;

using static Interop.NativeMethods;

public class DistanceJointDef : JointDef
{
    public Vec2 LocalAnchorA
    {
        get
        {
            b2DistanceJointDef_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 LocalAnchorB
    {
        get
        {
            b2DistanceJointDef_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    public float Length
    {
        get => b2DistanceJointDef_GetLength(Native);
        set => b2DistanceJointDef_SetLength(Native, value);
    }

    public float MinLength
    {
        get => b2DistanceJointDef_GetMinLength(Native);
        set => b2DistanceJointDef_SetMinLength(Native, value);
    }

    public float MaxLength
    {
        get => b2DistanceJointDef_GetMaxLength(Native);
        set => b2DistanceJointDef_SetMaxLength(Native, value);
    }

    public float Stiffness
    {
        get => b2DistanceJointDef_GetStiffness(Native);
        set => b2DistanceJointDef_SetStiffness(Native, value);
    }

    public float Damping
    {
        get => b2DistanceJointDef_GetDamping(Native);
        set => b2DistanceJointDef_SetDamping(Native, value);
    }

    public DistanceJointDef()
    {
        var native = b2DistanceJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vec2 anchorA, Vec2 anchorB)
        => b2DistanceJointDef_Initialize(Native, bodyA.Native, bodyB.Native, anchorA, anchorB);

    private protected override void Dispose(bool disposing)
    {
        b2DistanceJointDef_delete(Native);
    }
}

public class DistanceJoint : Joint
{
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
