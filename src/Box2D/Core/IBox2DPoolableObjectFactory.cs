namespace Box2D.Core;

public interface IBox2DPoolableObjectFactory<T> where T : notnull
{
    internal T Create();
}
