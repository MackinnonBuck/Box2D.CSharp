using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Box2D;

using static Conditionals;

internal static class Errors
{
    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfInvalidAccess(IntPtr ptr)
        => ThrowIfInvalidAccess<object>(null, ptr);

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfInvalidAccess<T>(T? instance, IntPtr ptr)
    {
        if (ptr == IntPtr.Zero)
        {
            throw new InvalidOperationException(InvalidAccessMessage(instance));
        }
    }

    private static string InvalidAccessMessage<T>(T? instance)
        => instance switch
        {
            Box2DDisposableObject x =>
                $"Attempted access of disposed instance of type '{x.GetType()}'.",

            Box2DSubObject x =>
                $"Attempted access of implicitly destroyed instance of type '{x.GetType()}'. " +
                $"See 'https://github.com/erincatto/box2d/blob/master/docs/loose_ends.md#implicit-destruction' " +
                $"for more information.",

            null =>
                $"Attempted access of invalid instance.",

            var x =>
                $"Attempted access of invalid instance of type '{x.GetType()}'.",
        };
}
