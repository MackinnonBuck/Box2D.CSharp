using System;

namespace Box2D.Core;

internal interface IPersistentDataReviver
{
    string RevivedObjectName { get; }

    IntPtr GetPersistentDataPointer(IntPtr native);
}
