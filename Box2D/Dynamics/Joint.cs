using Box2D.Core;
using Box2D.Dynamics.Joints;
using System;
using System.Numerics;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

public abstract class Joint : Box2DSubObject, IBox2DList<Joint>
{
    internal static JointFromIntPtr FromIntPtr { get; } = new();

    internal class JointFromIntPtr : IGetFromIntPtr<Joint>
    {
        public IntPtr GetManagedHandle(IntPtr obj)
            => b2Joint_GetUserData(obj);
    }

    public object? UserData { get; set; }

    public abstract JointType Type { get; }

    public Body BodyA => Body.FromIntPtr.Get(b2Joint_GetBodyA(Native))!;

    public Body BodyB => Body.FromIntPtr.Get(b2Joint_GetBodyB(Native))!;

    public Vector2 AnchorA
    {
        get
        {
            b2Joint_GetAnchorA(Native, out var value);
            return value;
        }
    }

    public Vector2 AnchorB
    {
        get
        {
            b2Joint_GetAnchorB(Native, out var value);
            return value;
        }
    }

    public Joint? Next => FromIntPtr.Get(b2Joint_GetNext(Native));

    public bool IsEnabled => b2Joint_IsEnabled(Native);

    public bool CollideConnected => b2Joint_GetCollideConnected(Native);

    internal static Joint Create(IntPtr worldNative, JointDef def)
    {
        var userData = def.UserData;
        Joint joint = def.Type switch
        {
            JointType.Unknown => throw new NotImplementedException(),
            JointType.Revolute => new RevoluteJoint(userData),
            JointType.Prismatic => new PrismaticJoint(userData),
            JointType.Distance => new DistanceJoint(userData),
            JointType.Pulley => throw new NotImplementedException(),
            JointType.Mouse => new MouseJoint(userData),
            JointType.Gear => throw new NotImplementedException(),
            JointType.Wheel => new WheelJoint(userData),
            JointType.Weld => throw new NotImplementedException(),
            JointType.Friction => throw new NotImplementedException(),
            JointType.Rope => throw new NotImplementedException(),
            JointType.Motor => throw new NotImplementedException(),
            var x => throw new InvalidOperationException($"Invalid joint type '{x}'."),
        };

        def.InternalUserData = joint.Handle;
        var native = b2World_CreateJoint(worldNative, def.Native);
        joint.Initialize(native);

        return joint;
    }

    public static void LinearStiffness(out float stiffness, out float damping, float frequencyHertz, float dampingRatio, Body bodyA, Body bodyB)
        => b2LinearStiffness_wrap(out stiffness, out damping, frequencyHertz, dampingRatio, bodyA.Native, bodyB.Native);

    public static void AngularStiffness(out float stiffness, out float damping, float frequencyHertz, float dampingRatio, Body bodyA, Body bodyB)
        => b2AngularStiffness_wrap(out stiffness, out damping, frequencyHertz, dampingRatio, bodyA.Native, bodyB.Native);

    internal Joint(object? userData)
    {
        UserData = userData;
    }

    public Vector2 GetReactionForce(float invDt)
    {
        b2Joint_GetReactionForce(Native, invDt, out var value);
        return value;
    }

    public float GetReactionTorque(float invDt)
        => b2Joint_GetReactionTorque(Native, invDt);

    public void ShiftOrigin(Vector2 newOrigin)
        => b2Joint_ShiftOrigin(Native, ref newOrigin);
}
