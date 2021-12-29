using Box2D.Core;
using System;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

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
