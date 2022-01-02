using Box2D.Collision;
using Box2D.Core;
using Box2D.Core.Allocation;
using System;

namespace Box2D.Dynamics;

using static Interop.NativeMethods;

/// <summary>
/// Used to create a fixture. This class defines an abstract fixture
/// definition. You can reuse fixture definitions safely.
/// </summary>
public sealed class FixtureDef : Box2DDisposableObject, IBox2DRecyclableObject
{
    private static readonly IAllocator<FixtureDef> _allocator = Allocator.Create<FixtureDef>(static () => new());

    private Shape? _shape;

    /// <summary>
    /// Gets or sets application-specific fixture data.
    /// </summary>
    public object? UserData { get; set; }

    /// <summary>
    /// Gets or sets the shape, which cannot be <c>null</c>. The shape will be
    /// cloned, so you can reuse and mutate shape instances.
    /// </summary>
    public Shape? Shape
    {
        get => _shape;
        set
        {
            _shape = value;
            b2FixtureDef_set_shape(Native, value?.Native ?? IntPtr.Zero);
        }
    }

    /// <summary>
    /// Gets or sets the friction coefficient, usually in the range [0,1].
    /// </summary>
    public float Friction
    {
        get => b2FixtureDef_get_friction(Native);
        set => b2FixtureDef_set_friction(Native, value);
    }

    /// <summary>
    /// Gets or sets the restitution (elasticity), usually in the range [0,1].
    /// </summary>
    public float Restitution
    {
        get => b2FixtureDef_get_restitution(Native);
        set => b2FixtureDef_set_restitution(Native, value);
    }

    /// <summary>
    /// Gets or sets the restitution velocity threshold, usually in m/s. Collisions above this
    /// speed have restitution applied (will bounce).
    /// </summary>
    public float RestitutionThreshold
    {
        get => b2FixtureDef_get_restitutionThreshold(Native);
        set => b2FixtureDef_set_restitutionThreshold(Native, value);
    }

    /// <summary>
    /// Gets or sets the density, usually in kg/m^2.
    /// </summary>
    public float Density
    {
        get => b2FixtureDef_get_density(Native);
        set => b2FixtureDef_set_density(Native, value);
    }

    /// <summary>
    /// Gets or sets whether the fixture should be a sensor.
    /// A sensor shape collects contact information but never generates a collision
    /// response.
    /// </summary>
    public bool IsSensor
    {
        get => b2FixtureDef_get_isSensor(Native);
        set => b2FixtureDef_set_isSensor(Native, value);
    }

    /// <summary>
    /// Gets or sets contact filtering data.
    /// </summary>
    public Filter Filter
    {
        get
        {
            b2FixtureDef_get_filter(Native, out var value);
            return value;
        }
        set => b2FixtureDef_set_filter(Native, ref value);
    }

    /// <summary>
    /// Creates a new <see cref="FixtureDef"/> instance.
    /// </summary>
    public static FixtureDef Create()
        => _allocator.Allocate();

    private FixtureDef() : base(isUserOwned: true)
    {
        var native = b2FixtureDef_new();
        Initialize(native);
    }

    bool IBox2DRecyclableObject.TryRecycle()
        => _allocator.TryRecycle(this);

    void IBox2DRecyclableObject.Reset()
    {
        UserData = null;
        b2FixtureDef_reset(Native);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
        => b2FixtureDef_delete(Native);
}
