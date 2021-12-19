using Box2D.Core;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Box2D;

using static Interop.NativeMethods;

// TODO: Should this be a ref struct so we don't persist the information longer than we should?
// Or it could be a normal struct and we just use Marshal.PtrToStructure()?
public readonly struct ContactImpulse
{
    private readonly IntPtr _native;

    internal ContactImpulse(IntPtr native)
    {
        _native = native;
    }

    // TODO: Implement
}

public abstract class ContactListener : Box2DRootObject
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

    public ContactListener() : base(isUserOwned: true)
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

    private void PreSolveUnmanaged(IntPtr contact, IntPtr manifold)
        => PreSolve(new(contact), new(manifold));

    private void PostSolveUnmanaged(IntPtr contact, IntPtr impulse)
        => PostSolve(new(contact), new(impulse));

    public virtual void BeginContact(in Contact contact)
    {
    }

    public virtual void EndContact(in Contact contact)
    {
    }

    public virtual void PreSolve(in Contact contact, in Manifold manifold)
    {
    }

    public virtual void PostSolve(in Contact contact, in ContactImpulse impulse)
    {
    }

    private protected override void Dispose(bool disposing)
    {
        b2ContactListenerWrapper_delete(Native);
    }
}
