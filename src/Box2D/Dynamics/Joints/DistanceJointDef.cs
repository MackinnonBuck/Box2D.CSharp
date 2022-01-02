using Box2D.Core;
using Box2D.Core.Allocation;
using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// Distance joint definition. This requires defining an anchor point on both
/// bodies and the non-zero distance of the distance joint. The definition uses
/// local anchor points so that the initial configuration can violate the
/// constraint slightly. This helps when saving and loading a game.
/// </summary>
public sealed class DistanceJointDef : JointDef
{
    private static readonly IAllocator<DistanceJointDef> _allocator = Allocator.Create<DistanceJointDef>(static () => new());

    /// <summary>
    /// Gets or sets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2DistanceJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2DistanceJointDef_set_localAnchorA(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local anchor point relative to body B's origin.
    /// </summary>
    public Vector2 LocalAnchorB
    {
        get
        {
            b2DistanceJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2DistanceJointDef_set_localAnchorB(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the rest length of this joint. Clamped to a stable minimum value.
    /// </summary>
    public float Length
    {
        get => b2DistanceJointDef_get_length(Native);
        set => b2DistanceJointDef_set_length(Native, value);
    }

    /// <summary>
    /// Gets or sets the minimum length. Clamped to a stable minimum value.
    /// </summary>
    public float MinLength
    {
        get => b2DistanceJointDef_get_minLength(Native);
        set => b2DistanceJointDef_set_minLength(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum length. Must be greater than or equal to the minimum length.
    /// </summary>
    public float MaxLength
    {
        get => b2DistanceJointDef_get_maxLength(Native);
        set => b2DistanceJointDef_set_maxLength(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear stiffness in N/m.
    /// </summary>
    public float Stiffness
    {
        get => b2DistanceJointDef_get_stiffness(Native);
        set => b2DistanceJointDef_set_stiffness(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear damping in N*s/m.
    /// </summary>
    public float Damping
    {
        get => b2DistanceJointDef_get_damping(Native);
        set => b2DistanceJointDef_set_damping(Native, value);
    }

    /// <summary>
    /// Creates a new <see cref="DistanceJointDef"/> instance.
    /// </summary>
    public static DistanceJointDef Create()
        => _allocator.Allocate();

    private DistanceJointDef()
    {
        var native = b2DistanceJointDef_new();
        Initialize(native);
    }

    /// <summary>
    /// Initialize the bodies, anchors, and rest length using world space anchors.
    /// The minimum and maximum lengths are set to the rest length.
    /// </summary>
    public void Initialize(Body bodyA, Body bodyB, Vector2 anchorA, Vector2 anchorB)
        => b2DistanceJointDef_Initialize(Native, bodyA.Native, bodyB.Native, anchorA, anchorB);

    private protected override bool TryRecycle()
        => _allocator.TryRecycle(this);

    private protected override void Reset()
        => b2DistanceJointDef_reset(Native);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
        => b2DistanceJointDef_delete(Native);
}
