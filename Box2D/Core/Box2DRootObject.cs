using System;

namespace Box2D.Core;

public abstract class Box2DRootObject : Box2DObject
{
    internal bool IsUserOwned { get; }

    private protected Box2DRootObject(bool isUserOwned)
    {
        IsUserOwned = isUserOwned;

        if (!IsUserOwned)
        {
            GC.SuppressFinalize(this);
        }
    }
}
