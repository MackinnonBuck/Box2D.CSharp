using System;
using System.Runtime.InteropServices;

namespace Box2D;

internal interface IGetFromIntPtr<T>
{
    public IntPtr GetManagedHandle(IntPtr obj);
}

internal static class GetFromIntPtrExtensions
{
    public static T? Get<T>(this IGetFromIntPtr<T> fromIntPtr, IntPtr obj)
    {
        if (obj == IntPtr.Zero)
        {
            return default;
        }

        var managedHandle = fromIntPtr.GetManagedHandle(obj);
       
        if (managedHandle == IntPtr.Zero)
        {
            throw new InvalidOperationException("The native pointer does not have an associated managed object.");
        }

        if (GCHandle.FromIntPtr(managedHandle).Target is not T instance)
        {
            throw new InvalidOperationException($"The managed {typeof(T).Name} object could not be revived.");
        }

        return instance;
    }
}
