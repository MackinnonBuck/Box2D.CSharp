using System;

namespace Box2D;

using static NativeMethods;

public readonly ref struct ContactImpulse
{
    private readonly IntPtr _native;

    public bool IsValid => _native != IntPtr.Zero;

    public Box2DArray<float> NormalImpulses { get; }

    public Box2DArray<float> TangentImpulses { get; }

    internal static ContactImpulse Create(IntPtr native)
    {
        if (native == IntPtr.Zero)
        {
            return default;
        }

        b2ContactImpulse_get_impulses(native, out IntPtr normalImpulses, out IntPtr tangentImpulses, out int count);
        return new(native, new(normalImpulses, count), new(tangentImpulses, count));
    }

    private ContactImpulse(IntPtr native, in Box2DArray<float> normalImpulses, in Box2DArray<float> tangentImpulses)
    {
        _native = native;
        NormalImpulses = normalImpulses;
        TangentImpulses = tangentImpulses;
    }
}
