using Box2D.Core;
using Box2D.Dynamics.Joints;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// The base joint class. Joints are used to constrain two bodies together in
/// various fashions. Some joints also feature limits and motors.
/// </summary>
public abstract class Joint : Box2DSubObject
{
    /// <summary>
    /// Gets or sets the user data object.
    /// </summary>
    public object? UserData { get; set; }

    /// <summary>
    /// Gets the type of the concrete joint.
    /// </summary>
    public abstract JointType Type { get; }

    /// <summary>
    /// Gets the first body attached to this joint.
    /// </summary>
    public Body BodyA => new(b2Joint_GetBodyA(Native));

    /// <summary>
    /// Gets the second body attached to this joint.
    /// </summary>
    public Body BodyB => new(b2Joint_GetBodyB(Native));

    /// <summary>
    /// Gets the anchor point on <see cref="BodyA"/> in world coordinates.
    /// </summary>
    public Vector2 AnchorA
    {
        get
        {
            b2Joint_GetAnchorA(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the anchor point on <see cref="BodyB"/> in world coordinates.
    /// </summary>
    public Vector2 AnchorB
    {
        get
        {
            b2Joint_GetAnchorB(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the next joint in the world joint list.
    /// </summary>
    public Joint? Next => FromIntPtr(b2Joint_GetNext(Native));

    /// <summary>
    /// Gets whether either body is enabled.
    /// </summary>
    public bool IsEnabled => b2Joint_IsEnabled(Native);

    /// <summary>
    /// Gets whether the two connected bodies should collide.
    /// </summary>
    /// <remarks>
    /// Note: modifying the collide connect flag won't work correctly because
    /// the flag is only checked when fixture AABBs begin to overlap.
    /// </remarks>
    public bool CollideConnected => b2Joint_GetCollideConnected(Native);

    internal static Joint? FromIntPtr(IntPtr obj)
    {
        if (obj == IntPtr.Zero)
        {
            return null;
        }

        var userData = b2Joint_GetUserData(obj);

        Errors.ThrowIfNullManagedPointer(userData, nameof(Joint));

        if (GCHandle.FromIntPtr(userData).Target is not Joint instance)
        {
            Errors.ThrowInvalidManagedPointer(nameof(Joint));
            throw null!; // Will not be reached since the previous method never returns.
        }

        return instance;
    }

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
            JointType.Friction => new FrictionJoint(userData),
            JointType.Rope => throw new NotImplementedException(),
            JointType.Motor => throw new NotImplementedException(),
            var x => throw new InvalidOperationException($"Invalid joint type '{x}'."),
        };

        var native = b2World_CreateJoint(worldNative, def.Native, joint.Handle);
        joint.Initialize(native);

        return joint;
    }

    /// <summary>
    /// Utility to compute linear stiffness values from frequency and damping ratio.
    /// </summary>
    public static void LinearStiffness(out float stiffness, out float damping, float frequencyHertz, float dampingRatio, Body bodyA, Body bodyB)
        => b2LinearStiffness_wrap(out stiffness, out damping, frequencyHertz, dampingRatio, bodyA.Native, bodyB.Native);

    /// <summary>
    /// Utility to compute rotational stiffness values from frequency and damping ratio.
    /// </summary>
    public static void AngularStiffness(out float stiffness, out float damping, float frequencyHertz, float dampingRatio, Body bodyA, Body bodyB)
        => b2AngularStiffness_wrap(out stiffness, out damping, frequencyHertz, dampingRatio, bodyA.Native, bodyB.Native);

    internal Joint(object? userData)
    {
        UserData = userData;
    }

    /// <summary>
    /// Gets the reaction force on <see cref="BodyB"/> at the joint anchor in Newtons.
    /// </summary>
    public Vector2 GetReactionForce(float invDt)
    {
        b2Joint_GetReactionForce(Native, invDt, out var value);
        return value;
    }

    /// <summary>
    /// Gets the reaction torque on <see cref="BodyB"/> in N*m.
    /// </summary>
    public float GetReactionTorque(float invDt)
        => b2Joint_GetReactionTorque(Native, invDt);

    /// <summary>
    /// Shifts the origin for any points stored in world coordinates.
    /// </summary>
    public void ShiftOrigin(Vector2 newOrigin)
        => b2Joint_ShiftOrigin(Native, ref newOrigin);
}
