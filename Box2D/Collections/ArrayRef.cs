using Box2D.Core;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Collections;

public readonly ref struct ArrayRef<T> where T : struct
{
    private static readonly int _elementSize = Marshal.SizeOf<T>();

    private readonly IntPtr _native;

    public int Length { get; }

    public bool IsValid => _native != IntPtr.Zero;

    public T this[int index]
    {
        get
        {
            ThrowIfInvalidIndex(index);
            return Marshal.PtrToStructure<T>(_native + _elementSize * index);
        }
        set
        {
            ThrowIfInvalidIndex(index);
            Marshal.StructureToPtr(value, _native + _elementSize * index, false);
        }
    }

    internal ArrayRef(IntPtr native, int length)
    {
        _native = native;
        Length = length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfInvalidIndex(int index)
    {
        Errors.ThrowIfInvalidAccess(_native);

        if (index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }
    }

    public Enumerator GetEnumerator()
        => new(in this);

    public ref struct Enumerator
    {
        private readonly ArrayRef<T> _source;
        private int _index = -1;

        public T Current => _source[_index];

        public Enumerator(in ArrayRef<T> source)
        {
            _source = source;
        }

        public bool MoveNext()
            => ++_index < _source.Length;
    }
}
