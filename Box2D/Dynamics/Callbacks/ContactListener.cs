using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D;

using static NativeMethods;

public abstract class ContactListener : Box2DObject
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

    protected ContactListener() : base(isUserOwned: true)
    {
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

    private void BeginContactUnmanaged(IntPtr contact)
        => BeginContact(new(contact));

    private void EndContactUnmanaged(IntPtr contact)
        => EndContact(new(contact));

    private void PreSolveUnmanaged(IntPtr contact, IntPtr oldManifold)
        => PreSolve(new(contact), Manifold.Create(oldManifold));

    private void PostSolveUnmanaged(IntPtr contact, IntPtr impulse)
        => PostSolve(new(contact), ContactImpulse.Create(impulse));

    protected virtual void BeginContact(in Contact contact)
    {
    }

    protected virtual void EndContact(in Contact contact)
    {
    }

    protected virtual void PreSolve(in Contact contact, in Manifold oldManifold)
    {
    }

    protected virtual void PostSolve(in Contact contact, in ContactImpulse impulse)
    {
    }

    protected override void Dispose(bool disposing)
    {
        b2ContactListenerWrapper_delete(Native);
    }
}
