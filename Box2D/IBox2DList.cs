namespace Box2D;

internal interface IBox2DList<T> where T : class, IBox2DList<T>
{
    public T? Next { get; }

    public struct Enumerator
    {
        private T? _list;

        public T Current { get; private set; } = default!;

        public Enumerator(IBox2DList<T>? list)
        {
            _list = list as T;
        }

        public bool MoveNext()
        {
            if (_list is null)
            {
                return false;
            }

            Current = _list;
            _list = _list.Next;

            return true;
        }
    }
}

internal static class Box2DListExtensions
{
    public static IBox2DList<T>.Enumerator GetEnumerator<T>(this IBox2DList<T>? list) where T : class, IBox2DList<T>
        => new(list);
}
