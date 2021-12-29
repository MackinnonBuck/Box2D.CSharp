using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class DistanceJointDef : JointDef
{
    public Vector2 LocalAnchorA
    {
        get
        {
            b2DistanceJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2DistanceJointDef_set_localAnchorA(Native, ref value);
    }

    public Vector2 LocalAnchorB
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

    public void Initialize(Body bodyA, Body bodyB, Vector2 anchorA, Vector2 anchorB)
        => b2DistanceJointDef_Initialize(Native, bodyA.Native, bodyB.Native, anchorA, anchorB);

    protected override void Dispose(bool disposing)
        => b2DistanceJointDef_delete(Native);
}
