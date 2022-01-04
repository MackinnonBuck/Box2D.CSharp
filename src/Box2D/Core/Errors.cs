using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Box2D.Core;

using static Config.Conditionals;

internal enum InvalidAccessReason
{
    NullInstance,
    DisposedInstance,
    ImplicitlyDestroyedInstance,
}

internal static class Errors
{
    public static void ThrowIfNullManagedPointer(IntPtr ptr, string targetTypeName)
    {
        if (ptr == IntPtr.Zero)
        {
            throw new InvalidOperationException($"The pointer to managed type {targetTypeName} was null.");
        }
    }

    public static void ThrowInvalidManagedPointer(string targeTypeName)
        => throw new InvalidOperationException($"The pointer to managed type {targeTypeName} was invalid.");

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(IntPtr ptr, string typeName)
    {
        if (ptr == IntPtr.Zero)
        {
            ThrowInvalidAccess(typeName, InvalidAccessReason.NullInstance);
        }
    }

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(IntPtr ptr, Box2DObject instance)
    {
        if (ptr == IntPtr.Zero)
        {
            ThrowInvalidAccess(instance.GetType().Name, instance switch
            {
                Box2DDisposableObject => InvalidAccessReason.DisposedInstance,
                Box2DSubObject => InvalidAccessReason.ImplicitlyDestroyedInstance,
                _ => InvalidAccessReason.NullInstance,
            });
        }
    }

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowInvalidAccess(string typeName, InvalidAccessReason reason)
        => throw new InvalidOperationException(reason switch
        {
            InvalidAccessReason.NullInstance => $"Attempted access of a null {typeName} instance.",
            InvalidAccessReason.DisposedInstance => $"Attempted access of a disposed {typeName} instance.",
            InvalidAccessReason.ImplicitlyDestroyedInstance =>
                $"Attempted access of an implicitly destroyed {typeName}. " +
                $"See 'https://github.com/erincatto/box2d/blob/master/docs/loose_ends.md#implicit-destruction' " +
                $"for more information.",
            _ => string.Empty,
        });
}
