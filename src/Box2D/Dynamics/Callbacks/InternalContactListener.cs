using Box2D.Collision;
using Box2D.Core;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D.Dynamics.Callbacks;

using static Interop.NativeMethods;

internal sealed class InternalContactListener : Box2DDisposableObject
{
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void BeginContactUnmanagedDelegate(IntPtr contact);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void EndContactUnmanagedDelegate(IntPtr contact);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void PreSolveUnmanagedDelegate(IntPtr contact, IntPtr oldManifold);
    [UnmanagedFunctionPointer(Conv), SuppressUnmanagedCodeSecurity]
    private delegate void PostSolveUnmanagedDelegate(IntPtr contact, IntPtr impulse);

    private readonly BeginContactUnmanagedDelegate _beginContact;
    private readonly EndContactUnmanagedDelegate _endContact;
    private readonly PreSolveUnmanagedDelegate _preSolve;
    private readonly PostSolveUnmanagedDelegate _postSolve;

    private IContactListener _userListener;

    public InternalContactListener(IContactListener userListener) : base(isUserOwned: true)
    {
        _userListener = userListener;

        _beginContact = BeginContactUnmanaged;
        _endContact = EndContactUnmanaged;
        _preSolve = PreSolveUnmanaged;
        _postSolve = PostSolveUnmanaged;

        var native = b2ContactListenerWrapper_new(
            Marshal.GetFunctionPointerForDelegate(_beginContact),
            Marshal.GetFunctionPointerForDelegate(_endContact),
            Marshal.GetFunctionPointerForDelegate(_preSolve),
            Marshal.GetFunctionPointerForDelegate(_postSolve));
        Initialize(native);
    }

    public void SetUserListener(IContactListener listener)
        => _userListener = listener;

    private void BeginContactUnmanaged(IntPtr contact)
        => _userListener.BeginContact(new(contact));

    private void EndContactUnmanaged(IntPtr contact)
        => _userListener.EndContact(new(contact));

    private void PreSolveUnmanaged(IntPtr contact, IntPtr oldManifold)
        => _userListener.PreSolve(new(contact), Manifold.Create(oldManifold));

    private void PostSolveUnmanaged(IntPtr contact, IntPtr impulse)
        => _userListener.PostSolve(new(contact), ContactImpulse.Create(impulse));

    protected override void Dispose(bool disposing)
        => b2ContactListenerWrapper_delete(Native);
}
