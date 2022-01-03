using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Core;

using static Config.Conditionals;

// This destroyer is used during invalidation.
internal readonly struct EmptyNativeResourceDestroyer : INativeResourceDestroyer
{
    public void Destroy(IntPtr native)
    {
    }
}

internal readonly partial struct NativeHandle<TReviver> where TReviver : struct, IManagedHandleReviver
{
    public IntPtr Ptr
    {
        get
        {
            ThrowIfInvalid();
            return _ptr;
        }
    }

    public bool IsNull => _ptr == IntPtr.Zero;

    public void Invalidate()
        => Destroy(default(EmptyNativeResourceDestroyer));
}

#if BOX2D_VALID_ACCESS_CHECKING
partial struct NativeHandle<TReviver>
{
    private readonly NativeHandleValidationToken? _token;
    private readonly IntPtr _ptr;

    public NativeHandle(IntPtr nativePtr, in ManagedHandle managedHandle)
    {
        _ptr = nativePtr;
        _token = managedHandle.ValidationToken;
    }

    public NativeHandle(IntPtr nativePtr)
    {
        _ptr = nativePtr;

        if (_ptr == IntPtr.Zero)
        {
            _token = null;
            return;
        }

        var managedHandle = default(TReviver).ReviveManagedHandle(nativePtr);

        if (managedHandle == IntPtr.Zero)
        {
            throw new InvalidOperationException("The handle to a managed object was null.");
        }
        
        if (GCHandle.FromIntPtr(managedHandle).Target is not NativeHandleValidationToken token)
        {
            throw new InvalidOperationException("The handle to a managed object was not valid.");
        }

        _token = token;
    }

    public void Destroy<TDestroyer>(in TDestroyer destroyer) where TDestroyer : struct, INativeResourceDestroyer
    {
        ThrowIfInvalid();

        // Get the managed handle before the resource is destroyed.
        var managedHandle = default(TReviver).ReviveManagedHandle(_ptr);

        // Destroy the resource. This might involve unmanaged code invoking into
        // managed code, which is why we can't free the handle yet.
        destroyer.Destroy(_ptr);

        // Free the managed handle.
        GCHandle.FromIntPtr(managedHandle).Free();

        // Mark this handle as invalid.
        _token!.IsValid = false;
    }

    public object? GetUserData()
    {
        ThrowIfInvalid();
        return _token!.UserData;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfInvalid()
    {
        if (_ptr == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Attempted to access a null {default(TReviver).FriendlyManagedTypeName}.");
        }

        if (_token is null || !_token.IsValid)
        {
            throw new InvalidOperationException($"Attempted to access a destroyed {default(TReviver).FriendlyManagedTypeName}.");
        }
    }
}
#else
partial struct NativeHandle<TReviver>
{
    private readonly IntPtr _ptr;

    public NativeHandle(IntPtr nativePtr, in ManagedHandle _) : this(nativePtr)
    {
    }

    public NativeHandle(IntPtr nativePtr)
    {
        _ptr = nativePtr;
    }

    public object? GetUserData()
    {
        var managedHandle = default(TReviver).ReviveManagedHandle(Ptr);
        
        if (managedHandle == IntPtr.Zero)
        {
            return null;
        }

        if (GCHandle.FromIntPtr(managedHandle).Target is not { } userData)
        {
            throw new InvalidOperationException($"A handle's target was not valid. It may have already been freed.");
        }

        return userData;
    }

    public void Destroy<TDestroyer>(in TDestroyer destroyer) where TDestroyer : struct, INativeResourceDestroyer
    {
        // Get the managed handle before the resource is destroyed.
        var managedHandle = default(TReviver).ReviveManagedHandle(Ptr);

        // Destroy the resource. This might involve unmanaged code invoking into
        // managed code, which is why we can't free the handle yet.
        destroyer.Destroy(_ptr);

        // Free the managed handle, if it exists.
        if (managedHandle != IntPtr.Zero)
        {
            GCHandle.FromIntPtr(managedHandle).Free();
        }
    }

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    private void ThrowIfInvalid()
    {
    }
}
#endif
