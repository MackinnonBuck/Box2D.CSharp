using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

public sealed class RevoluteJointDef : JointDef
{
    public Vector2 LocalAnchorA
    {
        get
        {
            b2RevoluteJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2RevoluteJointDef_set_localAnchorA(Native, ref value);
    }

    public Vector2 LocalAnchorB
    {
        get
        {
            b2RevoluteJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2RevoluteJointDef_set_localAnchorB(Native, ref value);
    }

    public float ReferenceAngle
    {
        get => b2RevoluteJointDef_get_referenceAngle(Native);
        set => b2RevoluteJointDef_set_referenceAngle(Native, value);
    }

    public bool EnableLimit
    {
        get => b2RevoluteJointDef_get_enableLimit(Native);
        set => b2RevoluteJointDef_set_enableLimit(Native, value);
    }

    public float LowerAngle
    {
        get => b2RevoluteJointDef_get_lowerAngle(Native);
        set => b2RevoluteJointDef_set_lowerAngle(Native, value);
    }

    public float UpperAngle
    {
        get => b2RevoluteJointDef_get_upperAngle(Native);
        set => b2RevoluteJointDef_set_upperAngle(Native, value);
    }

    public bool EnableMotor
    {
        get => b2RevoluteJointDef_get_enableMotor(Native);
        set => b2RevoluteJointDef_set_enableMotor(Native, value);
    }

    public float MotorSpeed
    {
        get => b2RevoluteJointDef_get_motorSpeed(Native);
        set => b2RevoluteJointDef_set_motorSpeed(Native, value);
    }

    public float MaxMotorTorque
    {
        get => b2RevoluteJointDef_get_maxMotorTorque(Native);
        set => b2RevoluteJointDef_set_maxMotorTorque(Native, value);
    }

    public RevoluteJointDef()
    {
        var native = b2RevoluteJointDef_new();
        Initialize(native);
    }

    public void Initialize(Body bodyA, Body bodyB, Vector2 anchor)
        => b2RevoluteJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor);

    protected override void Dispose(bool disposing)
        => b2RevoluteJointDef_delete(Native);
}
