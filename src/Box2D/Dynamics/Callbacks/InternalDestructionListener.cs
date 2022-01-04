using Box2D.Core;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Dynamics.Callbacks;

using static Interop.NativeMethods;

internal sealed class InternalDestructionListener : Box2DDisposableObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void SayGoodbyeJointDelegate(IntPtr joint);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void SayGoodbyeFixtureDelegate(IntPtr fixture);

    private readonly SayGoodbyeJointDelegate _sayGoodbyeJointDelegate;
    private readonly SayGoodbyeFixtureDelegate _sayGoodbyeFixtureDelegate;

    private IDestructionListener? _userListener;

    public InternalDestructionListener() : base(isUserOwned: true)
    {
        _sayGoodbyeJointDelegate = SayGoodbyeJointUnmanaged;
        _sayGoodbyeFixtureDelegate = SayGoodbyeFixtureUnmanaged;

        var native = b2DestructionListenerWrapper_new(
            Marshal.GetFunctionPointerForDelegate(_sayGoodbyeJointDelegate),
            Marshal.GetFunctionPointerForDelegate(_sayGoodbyeFixtureDelegate));
        Initialize(native);
    }

    public void SetUserListener(IDestructionListener? listener)
        => _userListener = listener;

    private void SayGoodbyeJointUnmanaged(IntPtr jointNative)
    {
        var joint = Joint.FromIntPtr(jointNative)!;
        _userListener?.SayGoodbye(joint);
        joint.Invalidate();
    }

    private void SayGoodbyeFixtureUnmanaged(IntPtr fixtureNative)
    {
        var fixture = new Fixture(fixtureNative);
        _userListener?.SayGoodbye(fixture);
        fixture.Invalidate();
    }

    protected override void Dispose(bool disposing)
        => b2DestructionListenerWrapper_delete(Native);
}
