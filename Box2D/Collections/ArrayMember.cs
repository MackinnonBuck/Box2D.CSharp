﻿using Box2D.Core;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Collections;

/// <summary>
/// Represents a managed reference to an unmanaged Box2D array owned by a
/// <see cref="Box2DObject"/>.
/// </summary>
/// <typeparam name="T">The array item type.</typeparam>
public sealed class ArrayMember<T> : Box2DObject where T : struct
{
    private static readonly int _elementSize = Marshal.SizeOf<T>();

    /// <summary>
    /// Gets the length of the array.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Gets or sets the value of an item in the array.
    /// </summary>
    /// <param name="index">The index of the item in the array.</param>
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

    internal ArrayMember(IntPtr native, int length)
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

    /// <summary>
    /// Gets an <see cref="Enumerator"/> for the current <see cref="ArrayMember{T}"/> instance.
    /// </summary>
    public Enumerator GetEnumerator()
        => new(this);

    /// <summary>
    /// An enumerator for <see cref="ArrayMember{T}"/> instances.
    /// </summary>
    public struct Enumerator
    {
        private readonly ArrayMember<T> _source;
        private int _index = -1;

        /// <summary>
        /// Gets the current element.
        /// </summary>
        public T Current => _source[_index];

        /// <summary>
        /// Constructs a new <see cref="Enumerator"/> instance.
        /// </summary>
        public Enumerator(ArrayMember<T> source)
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
