using Box2D.Core;
using System;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// Used to connect bodies and joints together
/// in a joint graph where each body is a node and each joint
/// is an edge. A joint edge belongs to a doubly-linked list
/// maintained in each attached body. Each joint has two joint
/// nodes, one for each attached body.
/// </summary>
/// <remarks>
/// Do not mutate joint edges unless you know exactly what you
/// are doing.
/// </remarks>
public readonly ref struct JointEdge
{
    private readonly IntPtr _native;

    internal IntPtr Native
    {
        get
        {
            Errors.ThrowIfNull(_native, nameof(JointEdge));
            return _native;
        }
    }

    /// <summary>
    /// Gets whether the <see cref="JointEdge"/> points to a null
    /// unmanaged joint edge.
    /// </summary>
    public bool IsNull => _native != IntPtr.Zero;

    /// <summary>
    /// Gets or sets the other attached body.
    /// </summary>
    public Body Other
    {
        get => new(b2JointEdge_get_other(Native));
        set => b2JointEdge_set_other(Native, value.IsNull ? IntPtr.Zero : value.Native);
    }

    /// <summary>
    /// Gets or sets the joint.
    /// </summary>
    public Joint? Joint
    {
        get => Joint.FromIntPtr(b2JointEdge_get_joint(Native));
        set => b2JointEdge_set_joint(Native, value?.Native ?? IntPtr.Zero);
    }

    /// <summary>
    /// Gets or sets the previous joint edge in the body's joint list.
    /// </summary>
    public JointEdge Prev
    {
        get => new(b2JointEdge_get_prev(Native));
        set => b2JointEdge_set_prev(Native, value.Native);
    }

    /// <summary>
    /// Gets or sets the next joint edge in the body's joint list.
    /// </summary>
    public JointEdge Next
    {
        get => new(b2JointEdge_get_next(Native));
        set => b2JointEdge_set_next(Native, value.Native);
    }

    internal JointEdge(IntPtr native)
    {
        _native = native;
    }

    /// <summary>
    /// Gets an <see cref="Enumerator"/> for the current <see cref="JointEdge"/> instance.
    /// </summary>
    public Enumerator GetEnumerator()
        => new(_native);

    /// <summary>
    /// An enumerator for <see cref="JointEdge"/> instances.
    /// </summary>
    public struct Enumerator
    {
        private IntPtr _current;
        private IntPtr _next;

        /// <summary>
        /// Gets the current element.
        /// </summary>
        public Joint Current => Joint.FromIntPtr(b2JointEdge_get_joint(_current))!;

        /// <summary>
        /// Constructs a new <see cref="Enumerator"/> instance.
        /// </summary>
        public Enumerator(IntPtr native)
        {
            _current = IntPtr.Zero;
            _next = native;
        }

        /// <summary>
        /// Moves to the next element.
        /// </summary>
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
