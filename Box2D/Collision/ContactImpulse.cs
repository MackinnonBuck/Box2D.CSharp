using Box2D.Collections;
using System;

namespace Box2D.Collision;

using static Interop.NativeMethods;

public readonly ref struct ContactImpulse
{
    private readonly IntPtr _native;

    public bool IsValid => _native != IntPtr.Zero;

    public ArrayRef<float> NormalImpulses { get; }

    public ArrayRef<float> TangentImpulses { get; }

    internal static ContactImpulse Create(IntPtr native)
    {
        if (native == IntPtr.Zero)
        {
            return default;
        }

        b2ContactImpulse_get_impulses(native, out IntPtr normalImpulses, out IntPtr tangentImpulses, out int count);
        return new(native, new(normalImpulses, count), new(tangentImpulses, count));
    }

    private ContactImpulse(IntPtr native, in ArrayRef<float> normalImpulses, in ArrayRef<float> tangentImpulses)
    {
        _native = native;
        NormalImpulses = normalImpulses;
        TangentImpulses = tangentImpulses;
    }
}
