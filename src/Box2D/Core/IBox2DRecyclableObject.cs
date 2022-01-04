namespace Box2D.Core;

internal interface IBox2DRecyclableObject
{
    // This method can be used to perform a recycling procedure.
    // If recycling was attempted, then the method should return true, regardless
    // of whether recycling was successful.
    // If recycling is not attempted, then the method should return false.
    bool TryRecycle();

    // This method defines how an instances should
    // be reset when returned to a pool during recycling.
    void Reset();
}
