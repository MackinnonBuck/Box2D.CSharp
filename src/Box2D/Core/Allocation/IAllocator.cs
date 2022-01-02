namespace Box2D.Core.Allocation;

internal interface IAllocator<T> where T : class, IBox2DRecyclableObject
{
    T Allocate();
    bool TryRecycle(T obj);
}
