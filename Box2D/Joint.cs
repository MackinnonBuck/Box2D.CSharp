using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Box2D;

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

[StructLayout(LayoutKind.Sequential)]
internal struct JointEdgeInternal
{
    public IntPtr other;
    public IntPtr joint;
    public IntPtr prev;
    public IntPtr next;
}

public readonly struct JointEdge
{
    private readonly IntPtr _prev;

    private readonly IntPtr _next;

    public Body? Other { get; }

    public Joint? Joint { get; }

    public JointEdge? Prev => FromIntPtr(_prev);

    public JointEdge? Next => FromIntPtr(_next);

    internal static JointEdge? FromIntPtr(IntPtr obj)
    {
        if (obj == IntPtr.Zero)
        {
            return null;
        }

        var source = Marshal.PtrToStructure<JointEdgeInternal>(obj);
        return new(ref source);
    }

    private JointEdge(ref JointEdgeInternal source)
    {
        _prev = source.prev;
        _next = source.next;

        Other = Body.FromIntPtr(source.other);
        Joint = Joint.FromIntPtr(source.joint);
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
        get => Body.FromIntPtr(b2JointDef_get_bodyA(Native));
        set => b2JointDef_set_bodyA(Native, value?.Native ?? IntPtr.Zero);
    }

    public Body? BodyB
    {
        get => Body.FromIntPtr(b2JointDef_get_bodyB(Native));
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

public abstract class Joint : Box2DObject
{
    public object? UserData { get; set; }

    public Joint? Next => FromIntPtr(b2Joint_GetNext(Native));

    internal IntPtr Handle { get; private set; }

    internal static Joint? FromIntPtr(IntPtr obj)
    {
        if (obj == IntPtr.Zero)
        {
            return null;
        }

        var userData = b2Joint_GetUserData(obj);

        if (userData == IntPtr.Zero)
        {
            throw new InvalidOperationException("The Box2D joint does not have an associated managed object.");
        }

        if (GCHandle.FromIntPtr(userData).Target is not Joint joint)
        {
            throw new InvalidOperationException($"The managed {nameof(Joint)} object could not be revived.");
        }

        return joint;
    }

    internal static Joint Create(IntPtr worldNative, JointDef def)
    {
        var userData = def.UserData;
        var joint = def.Type switch
        {
            JointType.Unknown => throw new NotImplementedException(),
            JointType.Revolute => throw new NotImplementedException(),
            JointType.Prismatic => throw new NotImplementedException(),
            JointType.Distance => new DistanceJoint(userData),
            JointType.Pulley => throw new NotImplementedException(),
            JointType.Mouse => throw new NotImplementedException(),
            JointType.Gear => throw new NotImplementedException(),
            JointType.Wheel => throw new NotImplementedException(),
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

    internal Joint(object? userData)
    {
        UserData = userData;
        Handle = GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Weak));
    }

    internal void InvalidateInstance()
    {
        Invalidate();

        Debug.Assert(Handle != IntPtr.Zero);
        GCHandle.FromIntPtr(Handle).Free();
        Handle = IntPtr.Zero;
    }
}
