using Box2D.Core;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Collections;

/// <summary>
/// Represents a managed reference to an unmanaged Box2D array.
/// </summary>
/// <typeparam name="T">The array item type.</typeparam>
public readonly ref struct ArrayRef<T> where T : struct
{
    private static readonly int _elementSize = Marshal.SizeOf<T>();

    private readonly IntPtr _native;

    /// <summary>
    /// Gets the length of the array.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Gets whether the <see cref="ArrayRef{T}"/> points to a null
    /// unmanaged array.
    /// </summary>
    public bool IsNull => _native == IntPtr.Zero;

    /// <summary>
    /// Gets or sets the value of an item in the array.
    /// </summary>
    /// <param name="index">The index of the item in the array.</param>
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
        Errors.ThrowIfNull(_native, nameof(ArrayRef<T>));

        if (index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }
    }

    /// <summary>
    /// Gets an <see cref="Enumerator"/> for the current <see cref="ArrayRef{T}"/> instance.
    /// </summary>
    public Enumerator GetEnumerator()
        => new(in this);

    /// <summary>
    /// An enumerator for <see cref="ArrayRef{T}"/> instances.
    /// </summary>
    public ref struct Enumerator
    {
        private readonly ArrayRef<T> _source;
        private int _index = -1;

        /// <summary>
        /// Gets the current element.
        /// </summary>
        public T Current => _source[_index];

        /// <summary>
        /// Constructs a new <see cref="Enumerator"/> instance.
        /// </summary>
        public Enumerator(in ArrayRef<T> source)
        {
            _source = source;
        }

        /// <summary>
        /// Moves to the next element.
        /// </summary>
        public bool MoveNext()
            => ++_index < _source.Length;
    }
}
