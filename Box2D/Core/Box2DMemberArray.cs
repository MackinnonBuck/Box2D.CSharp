using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Box2D.Core;

using static Config.Conditionals;

public readonly struct Box2DMemberArray<T> where T : struct
{
    private static readonly int _elementSize = Marshal.SizeOf<T>();

    private readonly Box2DObject _owner;

    private readonly IntPtr _native;

    public int Length { get; }

    public T this[int index]
    {
        get
        {
            ThrowIfInvalidAccess(index);
            return Marshal.PtrToStructure<T>(_native + _elementSize);
        }
        set
        {
            ThrowIfInvalidAccess(index);
            Marshal.StructureToPtr(value, _native + _elementSize, false);
        }
    }

    internal Box2DMemberArray(Box2DObject owner, IntPtr native, int length)
    {
        _owner = owner;
        _native = native;
        Length = length;
    }

    [Conditional(BOX2D_VALID_ACCESS_CHECKING)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ThrowIfInvalidAccess(int index)
    {
        if (_owner.IsDisposed)
        {
            throw new InvalidOperationException("The owner of the array was disposed.");
        }

        if (index < 0 || index >= Length)
        {
            throw new IndexOutOfRangeException();
        }
    }
}
