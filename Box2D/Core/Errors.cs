using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Box2D.Core;

using static Config.Conditionals;

internal static class Errors
{
    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfInvalidAccess(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero)
        {
            throw new InvalidOperationException("Cannot access an invalid instance.");
        }
    }
}
