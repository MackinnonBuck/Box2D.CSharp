﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D;

public readonly ref struct Box2DArrayRef<T> where T : struct
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

    internal Box2DArrayRef(IntPtr native, int length)
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
        private readonly Box2DArrayRef<T> _source;
        private int _index = -1;

        public T Current => _source[_index];

        public Enumerator(in Box2DArrayRef<T> source)
        {
            _source = source;
        }

        public bool MoveNext()
            => ++_index < _source.Length;
    }
}