using System;

namespace Box2D.Core;

internal interface IManagedHandleReviver
{
    public string FriendlyManagedTypeName { get; }

    public IntPtr ReviveManagedHandle(IntPtr native);
}
