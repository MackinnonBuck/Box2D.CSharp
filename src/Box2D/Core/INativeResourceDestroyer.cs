using System;

namespace Box2D.Core;

internal interface INativeResourceDestroyer
{
    void Destroy(IntPtr native);
}
