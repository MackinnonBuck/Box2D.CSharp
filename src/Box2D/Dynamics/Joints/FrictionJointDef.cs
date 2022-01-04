using Box2D.Core.Allocation;
using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// A friction joint definition.
/// </summary>
public sealed class FrictionJointDef : JointDef
{
    private static readonly IAllocator<FrictionJointDef> _allocator = Allocator.Create<FrictionJointDef>(static () => new());

    /// <summary>
    /// Gets or sets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2FrictionJointDef_get_localAnchorA(Native, out var value);
            return value;
        }
        set => b2FrictionJointDef_set_localAnchorA(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the local anchor point relative to body B's origin.
    /// </summary>
    public Vector2 LocalAnchorB
    {
        get
        {
            b2FrictionJointDef_get_localAnchorB(Native, out var value);
            return value;
        }
        set => b2FrictionJointDef_set_localAnchorB(Native, ref value);
    }

    /// <summary>
    /// Gets or sets the maximum friction force in N.
    /// </summary>
    public float MaxForce
    {
        get => b2FrictionJointDef_get_maxForce(Native);
        set => b2FrictionJointDef_set_maxForce(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum friction torque in N-m.
    /// </summary>
    public float MaxTorque
    {
        get => b2FrictionJointDef_get_maxTorque(Native);
        set => b2FrictionJointDef_set_maxTorque(Native, value);
    }

    /// <summary>
    /// Creates a new <see cref="FrictionJointDef"/> instance.
    /// </summary>
    public static FrictionJointDef Create()
        => _allocator.Allocate();

    private FrictionJointDef()
    {
        var native = b2FrictionJointDef_new();
        Initialize(native);
    }

    /// <summary>
    /// Initialize the bodies and anchors using the world anchor.
    /// </summary>
    public void Initialize(Body bodyA, Body bodyB, Vector2 anchor)
        => b2FrictionJointDef_Initialize(Native, bodyA.Native, bodyB.Native, ref anchor);

    private protected override bool TryRecycle()
        => _allocator.TryRecycle(this);

    private protected override void Reset()
        => b2FrictionJointDef_reset(Native);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
        => b2FrictionJointDef_delete(Native);
}
