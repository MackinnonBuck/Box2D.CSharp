using System;

namespace Box2D.Core;

internal interface IPersistentDataReviver
{
    public string RevivedObjectName { get; }

    public IntPtr GetPersistentDataPointer(IntPtr native);
}
