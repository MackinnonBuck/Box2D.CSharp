namespace Box2D.Core;

internal interface IBox2DList<T> where T : IBox2DList<T>
{
    public bool IsNull { get; }

    public T? Next { get; }

    public struct Enumerator
    {
        private T? _list;

        public T Current { get; private set; } = default!;

        public Enumerator(IBox2DList<T>? list)
        {
            _list = (T?)list;
        }

        public bool MoveNext()
        {
            if (_list is null || _list.IsNull)
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
    public static IBox2DList<T>.Enumerator GetEnumerator<T>(this T? list) where T : IBox2DList<T>
        => new(list);
}
