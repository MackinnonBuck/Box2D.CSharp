using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D;

using static Config.Conditionals;

public readonly ref struct Box2DArray<T> where T : struct
{
    private static readonly int _elementSize = Marshal.SizeOf<T>();

    private readonly IntPtr _native;

    public int Length { get; }

    public bool IsValid => _native != IntPtr.Zero;

    public T this[int index]
    {
        get
        {
            ThrowIfInvalidAccess(index);
            return Marshal.PtrToStructure<T>(_native + _elementSize * index);
        }
        set
        {
            ThrowIfInvalidAccess(index);
            Marshal.StructureToPtr(value, _native + _elementSize * index, false);
        }
    }

    internal Box2DArray(IntPtr native, int length)
    {
        _native = native;
        Length = length;
    }

    public Enumerator GetEnumerator()
        => new(in this);

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfInvalidAccess(int index)
    {
        Errors.ThrowIfInvalidAccess(_native);

        if (index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }
    }

    public ref struct Enumerator
    {
        private readonly Box2DArray<T> _source;
        private int _index;

        public T Current { get; private set; } = default;

        public Enumerator(in Box2DArray<T> source)
        {
            _index = 0;
            _source = source;
        }

        public bool MoveNext()
        {
            if (_index >= _source.Length)
            {
                return false;
            }

            Current = _source[_index++];
            return true;
        }
    }
}

public readonly struct Box2DOwnedArray<T> where T : struct
{
    private static readonly int _elementSize = Marshal.SizeOf<T>();

    private readonly Box2DObject _owner;

    private readonly IntPtr _native;

    public int Length { get; }

    public bool IsValid => _native != IntPtr.Zero;

    public T this[int index]
    {
        get
        {
            ThrowIfInvalidAccess(index);
            return Marshal.PtrToStructure<T>(_native + _elementSize * index);
        }
        set
        {
            ThrowIfInvalidAccess(index);
            Marshal.StructureToPtr(value, _native + _elementSize * index, false);
        }
    }

    internal Box2DOwnedArray(Box2DObject owner, IntPtr native, int length)
    {
        _owner = owner;
        _native = native;
        Length = length;
    }

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfInvalidAccess(int index)
    {
        Errors.ThrowIfInvalidAccess(_native);

        if (_owner.IsDisposed)
        {
            throw new InvalidOperationException("The owner of the array was disposed.");
        }

        if (index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }
    }
    public struct Enumerator
    {
        private readonly Box2DOwnedArray<T> _source;
        private int _index;

        public T Current { get; private set; } = default;

        public Enumerator(in Box2DOwnedArray<T> source)
        {
            _index = 0;
            _source = source;
        }

        public bool MoveNext()
        {
            if (_index >= _source.Length)
            {
                return false;
            }

            Current = _source[_index++];
            return true;
        }
    }
}
