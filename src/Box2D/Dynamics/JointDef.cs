using Box2D.Core;
using System;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// Used to construct joints.
/// </summary>
public abstract class JointDef : Box2DDisposableObject, IBox2DRecyclableObject
{
    /// <summary>
    /// Gets or sets the application-specific data for the joint.
    /// </summary>
    public object? UserData { get; set; }

    /// <summary>
    /// Gets or sets the joint type.
    /// </summary>
    /// <remarks>
    /// This is set automatically for concrete joint types, so you shouldn't need to specify
    /// this manually.
    /// </remarks>
    public JointType Type
    {
        get => b2JointDef_get_type(Native);
        set => b2JointDef_set_type(Native, value);
    }

    /// <summary>
    /// Gets or sets the first attached body.
    /// </summary>
    public Body BodyA
    {
        get => new(b2JointDef_get_bodyA(Native));
        set => b2JointDef_set_bodyA(Native, value.IsNull ? IntPtr.Zero : value.Native);
    }

    /// <summary>
    /// Gets or sets the second attached body.
    /// </summary>
    public Body BodyB
    {
        get => new(b2JointDef_get_bodyB(Native));
        set => b2JointDef_set_bodyB(Native, value.IsNull ? IntPtr.Zero : value.Native);
    }

    /// <summary>
    /// Gets or sets whether the attached bodies should collide.
    /// </summary>
    public bool CollideConnected
    {
        get => b2JointDef_get_collideConnected(Native);
        set => b2JointDef_set_collideConnected(Native, value);
    }

    /// <summary>
    /// Constructs a new <see cref="JointDef"/> instance.
    /// </summary>
    protected JointDef() : base(isUserOwned: true)
    {
    }

    bool IBox2DRecyclableObject.TryRecycle()
        => TryRecycle();

    void IBox2DRecyclableObject.Reset()
    {
        UserData = null;
        Reset();
    }

    private protected abstract bool TryRecycle();

    private protected abstract void Reset();
}
