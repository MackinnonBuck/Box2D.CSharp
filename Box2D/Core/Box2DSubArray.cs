using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D;

public class Box2DSubArray<T> : Box2DObject where T : struct
{
    private static readonly int _elementSize = Marshal.SizeOf<T>();

    public int Length { get; }

    public T this[int index]
    {
        get
        {
            ThrowIfInvalidIndex(index);
            return Marshal.PtrToStructure<T>(Native + _elementSize * index);
        }
        set
        {
            ThrowIfInvalidIndex(index);
            Marshal.StructureToPtr(value, Native + _elementSize * index, false);
        }
    }

    internal Box2DSubArray(IntPtr native, int length)
    {
        Length = length;
        Initialize(native);
    }

    internal void Invalidate()
        => Uninitialize();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfInvalidIndex(int index)
    {
        if (index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }
    }

    public Enumerator GetEnumerator()
        => new(this);

    public struct Enumerator
    {
        private readonly Box2DSubArray<T> _source;
        private int _index = -1;

        public T Current => _source[_index];

        public Enumerator(Box2DSubArray<T> source)
        {
            _source = source;
        }

        public bool MoveNext()
            => ++_index < _source.Length;
    }
}
