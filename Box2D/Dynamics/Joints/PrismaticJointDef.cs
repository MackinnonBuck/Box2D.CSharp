using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class PrismaticJointDef : JointDef
{
    public Vector2 LocalAnchorA
    {
        get
        {
            b2PrismaticJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAnchorA(Native, ref value);
    }

    public Vector2 LocalAnchorB
    {
        get
        {
            b2PrismaticJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAnchorB(Native, ref value);
    }

    public Vector2 LocalAxisA
    {
        get
        {
            b2PrismaticJointDef_get_localAxisA(Native, out var value);
            return value;
        }
        set => b2PrismaticJointDef_set_localAxisA(Native, ref value);
    }

    public float ReferenceAngle
    {
        get => b2PrismaticJointDef_get_referenceAngle(Native);
        set => b2PrismaticJointDef_set_referenceAngle(Native, value);
    }

    public bool EnableLimit
    {
        get => b2PrismaticJointDef_get_enableLimit(Native);
        set => b2PrismaticJointDef_set_enableLimit(Native, value);
    }

    public float LowerTranslation
    {
        get => b2PrismaticJointDef_get_lowerTranslation(Native);
        set => b2PrismaticJointDef_set_lowerTranslation(Native, value);
    }

    public float UpperTranslation
    {
        get => b2PrismaticJointDef_get_upperTranslation(Native);
        set => b2PrismaticJointDef_set_upperTranslation(Native, value);
    }

    public bool EnableMotor
    {
        get => b2PrismaticJointDef_get_enableMotor(Native);
        set => b2PrismaticJointDef_set_enableMotor(Native, value);
    }

    public float MaxMotorForce
    {
        get => b2PrismaticJointDef_get_maxMotorForce(Native);
        set => b2PrismaticJointDef_set_maxMotorForce(Native, value);
    }

    public float MotorSpeed
    {
        get => b2PrismaticJointDef_get_motorSpeed(Native);
        set => b2PrismaticJointDef_set_motorSpeed(Native, value);
    }

    public PrismaticJointDef()
    {
        var native = b2PrismaticJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vector2 anchor, Vector2 axis)
        => b2PrismaticJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor, ref axis);

    protected override void Dispose(bool disposing)
        => b2PrismaticJointDef_delete(Native);
}
