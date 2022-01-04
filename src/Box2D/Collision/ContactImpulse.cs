using Box2D.Collections;
using System;

namespace Box2D.Collision;

using static Interop.NativeMethods;

/// <summary>
/// Contact impulses for reporting. Impulses are used instead of forces because
/// sub-step forces may approach infinity for rigid body collisions. These
/// match up one-to-one with the contact points in <see cref="Manifold"/>.
/// </summary>
public readonly ref struct ContactImpulse
{
    private readonly IntPtr _native;

    /// <summary>
    /// Gets whether this <see cref="ContactImpulse"/> points to a null unmanaged contact impulse.
    /// </summary>
    public bool IsNull => _native == IntPtr.Zero;

    /// <summary>
    /// Gets the normal impulses array.
    /// </summary>
    public ArrayRef<float> NormalImpulses { get; }

    /// <summary>
    /// Gets the tangent impulses array.
    /// </summary>
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
