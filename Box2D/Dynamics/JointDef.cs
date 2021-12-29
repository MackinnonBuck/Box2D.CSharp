using Box2D.Core;
using System;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

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
