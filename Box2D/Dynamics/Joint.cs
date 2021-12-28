using Box2D.Core;
using Box2D.Dynamics.Joints;
using Box2D.Math;
using System;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

public enum JointType
{
    Unknown,
    Revolute,
    Prismatic,
    Distance,
    Pulley,
    Mouse,
    Gear,
    Wheel,
    Weld,
    Friction,
    Rope,
    Motor,
}

public readonly ref struct JointEdge
{
    private readonly IntPtr _native;

    internal IntPtr Native
    {
        get
        {
            Errors.ThrowIfInvalidAccess(_native);
            return _native;
        }
    }

    public bool IsValid => _native != IntPtr.Zero;

    public Body? Other
    {
        get => Body.FromIntPtr.Get(b2JointEdge_get_other(Native));
        set => b2JointEdge_set_other(Native, value?.Native ?? IntPtr.Zero);
    }

    public Joint? Joint
    {
        get => Joint.FromIntPtr.Get(b2JointEdge_get_joint(Native));
        set => b2JointEdge_set_joint(Native, value?.Native ?? IntPtr.Zero);
    }

    public JointEdge Prev
    {
        get => new(b2JointEdge_get_prev(Native));
        set => b2JointEdge_set_prev(Native, value.Native);
    }

    public JointEdge Next
    {
        get => new(b2JointEdge_get_next(Native));
        set => b2JointEdge_set_next(Native, value.Native);
    }

    internal JointEdge(IntPtr native)
    {
        _native = native;
    }

    public Enumerator GetEnumerator()
        => new(_native);

    public struct Enumerator
    {
        private IntPtr _current;
        private IntPtr _next;

        public Joint Current => Joint.FromIntPtr.Get(b2JointEdge_get_joint(_current))!;

        public Enumerator(IntPtr native)
        {
            _current = IntPtr.Zero;
            _next = native;
        }

        public bool MoveNext()
        {
            if (_next == IntPtr.Zero)
            {
                return false;
            }

            _current = _next;
            _next = b2JointEdge_get_next(_next);

            return true;
        }
    }
}

public abstract class JointDef : Box2DDisposableObject
{
    public object? UserData { get; set; }

    public JointType Type
    {
        get => b2JointDef_get_type(Native);
        set => b2JointDef_set_type(Native, value);
    }

    public Body? BodyA
    {
        get => Body.FromIntPtr.Get(b2JointDef_get_bodyA(Native));
        set => b2JointDef_set_bodyA(Native, value?.Native ?? IntPtr.Zero);
    }

    public Body? BodyB
    {
        get => Body.FromIntPtr.Get(b2JointDef_get_bodyB(Native));
        set => b2JointDef_set_bodyB(Native, value?.Native ?? IntPtr.Zero);
    }

    public bool CollideConnected
    {
        get => b2JointDef_get_collideConnected(Native);
        set => b2JointDef_set_collideConnected(Native, value);
    }

    internal IntPtr InternalUserData
    {
        get => b2JointDef_get_userData(Native);
        set => b2JointDef_set_userData(Native, value);
    }

    protected JointDef() : base(isUserOwned: true)
    {
    }
}

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

    public Vec2 AnchorA
    {
        get
        {
            b2Joint_GetAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 AnchorB
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

    public Vec2 GetReactionForce(float invDt)
    {
        b2Joint_GetReactionForce(Native, invDt, out var value);
        return value;
    }

    public float GetReactionTorque(float invDt)
        => b2Joint_GetReactionTorque(Native, invDt);

    public void ShiftOrigin(Vec2 newOrigin)
        => b2Joint_ShiftOrigin(Native, ref newOrigin);
}
