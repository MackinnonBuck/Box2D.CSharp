using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Core;

using static Config.Conditionals;

// This type represents a handle to a native resource. The handle can be revived through
// use of a pointer to persisted data, which requires the native resource to have a "user data"
// field. The TReviver generic type parameter specifies how the persisted data pointer
// is obtained.
// When access validity checking is enabled, an object is allocated to track validity of the handle,
// so all copies of the handle can determine if an invalid access occurs. When access validity checking
// is disabled, no such object is allocated.
internal readonly partial struct NativeHandle<TReviver> : IEquatable<NativeHandle<TReviver>>
    where TReviver : struct, IPersistentDataReviver
{
    // This destroyer is used during invalidation.
    private readonly struct EmptyNativeResourceDestroyer : INativeResourceDestroyer
    {
        public void Destroy(IntPtr native)
        {
        }
    }

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

    public bool Equals(NativeHandle<TReviver> other)
        => _ptr.Equals(other._ptr);

    public override bool Equals(object obj)
        => obj is NativeHandle<TReviver> other && Equals(other);

    public override int GetHashCode()
        => _ptr.GetHashCode();
}

#if BOX2D_VALID_ACCESS_CHECKING
partial struct NativeHandle<TReviver>
{
    private readonly PersistentData? _data;
    private readonly IntPtr _ptr;

    public NativeHandle(IntPtr nativePtr, in PersistentDataHandle persistentDataHandle)
    {
        _ptr = nativePtr;
        _data = persistentDataHandle.Data;
    }

    public NativeHandle(IntPtr nativePtr)
    {
        _ptr = nativePtr;

        if (_ptr == IntPtr.Zero)
        {
            _data = null;
            return;
        }

        var dataPointer = default(TReviver).GetPersistentDataPointer(nativePtr);

        Errors.ThrowIfNullManagedPointer(dataPointer, nameof(PersistentData));

        if (GCHandle.FromIntPtr(dataPointer).Target is not PersistentData data)
        {
            Errors.ThrowInvalidManagedPointer(nameof(PersistentData));
            throw null!; // Will not be reached since the previous method never returns.
        }

        _data = data;
    }

    public void Destroy<TDestroyer>(in TDestroyer destroyer) where TDestroyer : struct, INativeResourceDestroyer
    {
        ThrowIfInvalid();

        // Get the managed handle before the resource is destroyed.
        var managedHandle = default(TReviver).GetPersistentDataPointer(_ptr);

        // Destroy the resource. This might involve unmanaged code invoking into
        // managed code, which is why we can't free the handle yet.
        destroyer.Destroy(_ptr);

        // Free the managed handle.
        GCHandle.FromIntPtr(managedHandle).Free();

        // Mark this handle as invalid.
        _data!.IsValid = false;
    }

    public object? GetUserData()
    {
        ThrowIfInvalid();
        return _data!.UserData;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfInvalid()
    {
        if (_ptr == IntPtr.Zero)
        {
            Errors.ThrowInvalidAccess(default(TReviver).RevivedObjectName, InvalidAccessReason.NullInstance);
        }

        if (_data is null || !_data.IsValid)
        {
            Errors.ThrowInvalidAccess(default(TReviver).RevivedObjectName, InvalidAccessReason.ImplicitlyDestroyedInstance);
        }
    }
}
#else
partial struct NativeHandle<TReviver>
{
    private readonly IntPtr _ptr;

    public NativeHandle(IntPtr nativePtr, in PersistentDataHandle _) : this(nativePtr)
    {
    }

    public NativeHandle(IntPtr nativePtr)
    {
        _ptr = nativePtr;
    }

    public object? GetUserData()
    {
        var dataPointer = default(TReviver).GetPersistentDataPointer(Ptr);

        if (dataPointer == IntPtr.Zero)
        {
            return null;
        }

        if (GCHandle.FromIntPtr(dataPointer).Target is not { } userData)
        {
            Errors.ThrowInvalidManagedPointer(nameof(Object));
            throw null!; // Will not be reached since the previous method never returns.
        }

        return userData;
    }

    public void Destroy<TDestroyer>(in TDestroyer destroyer) where TDestroyer : struct, INativeResourceDestroyer
    {
        // Get the managed handle before the resource is destroyed.
        var dataPointer = default(TReviver).GetPersistentDataPointer(Ptr);

        // Destroy the resource. This might involve unmanaged code invoking into
        // managed code, which is why we can't free the handle yet.
        destroyer.Destroy(_ptr);

        // Free the managed handle, if it exists.
        if (dataPointer != IntPtr.Zero)
        {
            GCHandle.FromIntPtr(dataPointer).Free();
        }
    }

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    private void ThrowIfInvalid()
    {
    }
}
#endif
