using Box2D.Core;
using System;
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

    public bool IsValid { get; }

    public Body? Other { get; }

    public Joint? Joint { get; }

    public JointEdge Prev => GetFromIntPtr(_prev);

    public JointEdge Next => GetFromIntPtr(_next);

    internal static JointEdge GetFromIntPtr(IntPtr obj)
    {
        if (obj == IntPtr.Zero)
        {
            return default;
        }

        var source = Marshal.PtrToStructure<JointEdgeInternal>(obj);
        return new(ref source);
    }

    private JointEdge(ref JointEdgeInternal source)
    {
        _prev = source.prev;
        _next = source.next;

        IsValid = true;
        Other = Body.FromIntPtr.Get(source.other);
        Joint = Joint.FromIntPtr.Get(source.joint);
    }

    public Enumerator GetEnumerator()
        => new(in this);

    public struct Enumerator
    {
        private JointEdge _currentEdge;

        public Joint Current { get; private set; } = default!;

        public Enumerator(in JointEdge edge)
        {
            _currentEdge = edge;
        }

        public bool MoveNext()
        {
            if (!_currentEdge.IsValid)
            {
                return false;
            }

            Current = _currentEdge.Joint!;
            _currentEdge = _currentEdge.Next;

            return true;
        }
    }
}

public abstract class JointDef : Box2DRootObject
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

    internal struct JointFromIntPtr : IGetFromIntPtr<Joint>
    {
        public IntPtr GetManagedHandle(IntPtr obj)
            => b2Joint_GetUserData(obj);
    }

    public object? UserData { get; set; }

    public Joint? Next => FromIntPtr.Get(b2Joint_GetNext(Native));

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
    }
}
