using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class WheelJointDef : JointDef
{
    public Vector2 LocalAnchorA
    {
        get
        {
            b2WheelJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAnchorA(Native, ref value);
    }

    public Vector2 LocalAnchorB
    {
        get
        {
            b2WheelJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAnchorB(Native, ref value);
    }

    public Vector2 LocalAxisA
    {
        get
        {
            b2WheelJointDef_get_localAxisA(Native, out var value);
            return value;
        }
        set => b2WheelJointDef_set_localAxisA(Native, ref value);
    }

    public bool EnableLimit
    {
        get => b2WheelJointDef_get_enableLimit(Native);
        set => b2WheelJointDef_set_enableLimit(Native, value);
    }

    public float LowerTranslation
    {
        get => b2WheelJointDef_get_lowerTranslation(Native);
        set => b2WheelJointDef_set_lowerTranslation(Native, value);
    }

    public float UpperTranslation
    {
        get => b2WheelJointDef_get_upperTranslation(Native);
        set => b2WheelJointDef_set_upperTranslation(Native, value);
    }

    public bool EnableMotor
    {
        get => b2WheelJointDef_get_enableMotor(Native);
        set => b2WheelJointDef_set_enableMotor(Native, value);
    }

    public float MaxMotorTorque
    {
        get => b2WheelJointDef_get_maxMotorTorque(Native);
        set => b2WheelJointDef_set_maxMotorTorque(Native, value);
    }

    public float MotorSpeed
    {
        get => b2WheelJointDef_get_motorSpeed(Native);
        set => b2WheelJointDef_set_motorSpeed(Native, value);
    }

    public float Stiffness
    {
        get => b2WheelJointDef_get_stiffness(Native);
        set => b2WheelJointDef_set_stiffness(Native, value);
    }

    public float Damping
    {
        get => b2WheelJointDef_get_damping(Native);
        set => b2WheelJointDef_set_damping(Native, value);
    }

    public WheelJointDef()
    {
        var native = b2WheelJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vector2 anchor, Vector2 axis)
        => b2WheelJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor, ref axis);

    protected override void Dispose(bool disposing)
        => b2WheelJointDef_delete(Native);
}
