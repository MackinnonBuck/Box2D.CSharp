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
}
