using Box2D.Core;
using Box2D.Math;
using System;

namespace Box2D;

using static Interop.NativeMethods;
using static Errors;

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
    internal IntPtr Native { get; }

    public bool IsValid => Native != IntPtr.Zero;

    public Body? Other
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return Body.FromIntPtr.Get(b2JointEdge_get_other(Native));
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2JointEdge_set_other(Native, value?.Native ?? IntPtr.Zero);
        }
    }

    public Joint? Joint
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return Joint.FromIntPtr.Get(b2JointEdge_get_joint(Native));
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2JointEdge_set_joint(Native, value?.Native ?? IntPtr.Zero);
        }
    }

    public JointEdge Prev
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return new(b2JointEdge_get_prev(Native));
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2JointEdge_set_prev(Native, value.Native);
        }
    }

    public JointEdge Next
    {
        get
        {
            ThrowIfInvalidAccess(Native);
            return new(b2JointEdge_get_next(Native));
        }
        set
        {
            ThrowIfInvalidAccess(Native);
            b2JointEdge_set_next(Native, value.Native);
        }
    }

    internal JointEdge(IntPtr native)
    {
        Native = native;
    }

    public Enumerator GetEnumerator()
        => new(Native);

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

public abstract class JointDef : Box2DObject
{
    public object? UserData { get; set; }

    public JointType Type
    {
        get
        {
            ThrowIfDisposed();
            return b2JointDef_get_type(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2JointDef_set_type(Native, value);
        } 
    }

    public Body? BodyA
    {
        get
        {
            ThrowIfDisposed();
            return Body.FromIntPtr.Get(b2JointDef_get_bodyA(Native));
        }
        set
        {
            ThrowIfDisposed();
            b2JointDef_set_bodyA(Native, value?.Native ?? IntPtr.Zero);
        }
    }

    public Body? BodyB
    {
        get
        {
            ThrowIfDisposed();
            return Body.FromIntPtr.Get(b2JointDef_get_bodyB(Native));
        }
        set
        {
            ThrowIfDisposed();
            b2JointDef_set_bodyB(Native, value?.Native ?? IntPtr.Zero);
        }
    }

    public bool CollideConnected
    {
        get
        {
            ThrowIfDisposed();
            return b2JointDef_get_collideConnected(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2JointDef_set_collideConnected(Native, value);
        }
    }

    internal IntPtr InternalUserData
    {
        get
        {
            ThrowIfDisposed();
            return b2JointDef_get_userData(Native);
        }
        set
        {
            ThrowIfDisposed();
            b2JointDef_set_userData(Native, value);
        }
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

    public Body BodyA
    {
        get
        {
            ThrowIfDisposed();
            return Body.FromIntPtr.Get(b2Joint_GetBodyA(Native))!;
        }
    }

    public Body BodyB
    {
        get
        {
            ThrowIfDisposed();
            return Body.FromIntPtr.Get(b2Joint_GetBodyB(Native))!;
        }
    }

    public Vec2 AnchorA
    {
        get
        {
            ThrowIfDisposed();
            b2Joint_GetAnchorA(Native, out var value);
            return value;
        }
    }

    public Vec2 AnchorB
    {
        get
        {
            ThrowIfDisposed();
            b2Joint_GetAnchorB(Native, out var value);
            return value;
        }
    }

    public Joint? Next
    {
        get
        {
            ThrowIfDisposed();
            return FromIntPtr.Get(b2Joint_GetNext(Native));
        }
    }

    public bool IsEnabled
    {
        get
        {
            ThrowIfDisposed();
            return b2Joint_IsEnabled(Native);
        }
    }

    public bool CollideConnected
    {
        get
        {
            ThrowIfDisposed();
            return b2Joint_GetCollideConnected(Native);
        }
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

    public Vec2 GetReactionForce(float invDt)
    {
        ThrowIfDisposed();
        b2Joint_GetReactionForce(Native, invDt, out var value);
        return value;
    }

    public float GetReactionTorque(float invDt)
    {
        ThrowIfDisposed();
        return b2Joint_GetReactionTorque(Native, invDt);
    }

    public void ShiftOrigin(Vec2 newOrigin)
    {
        ThrowIfDisposed();
        b2Joint_ShiftOrigin(Native, ref newOrigin);
    }
}
