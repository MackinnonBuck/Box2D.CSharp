using System.Numerics;

namespace Box2D.Dynamics.Joints;

using static Interop.NativeMethods;

/// <summary>
/// A distance joint constrains two points on two bodies to remain at a fixed
/// distance from each other. You can view this as a massless, rigid rod.
/// </summary>
public sealed class DistanceJoint : Joint
{
    /// <inheritdoc/>
    public override JointType Type => JointType.Distance;

    /// <summary>
    /// Gets the local anchor point relative to body A's origin.
    /// </summary>
    public Vector2 LocalAnchorA
    {
        get
        {
            b2DistanceJoint_GetLocalAnchorA(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets the local anchor point relative to body B's origin.
    /// </summary>
    public Vector2 LocalAnchorB
    {
        get
        {
            b2DistanceJoint_GetLocalAnchorB(Native, out var value);
            return value;
        }
    }

    /// <summary>
    /// Gets or sets the rest length.
    /// </summary>
    public float Length
    {
        get => b2DistanceJoint_GetLength(Native);
        set => b2DistanceJoint_SetLength(Native, value);
    }

    /// <summary>
    /// Gets or sets the minimum length.
    /// </summary>
    public float MinLength
    {
        get => b2DistanceJoint_GetMinLength(Native);
        set => b2DistanceJoint_SetMinLength(Native, value);
    }

    /// <summary>
    /// Gets or sets the maximum length.
    /// </summary>
    public float MaxLength
    {
        get => b2DistanceJoint_GetMaxLength(Native);
        set => b2DistanceJoint_SetMaxLength(Native, value);
    }

    /// <summary>
    /// Gets the current length.
    /// </summary>
    public float CurrentLength => b2DistanceJoint_GetCurrentLength(Native);

    /// <summary>
    /// Gets or sets the linear stiffness in N/m.
    /// </summary>
    public float Stiffness
    {
        get => b2DistanceJoint_GetStiffness(Native);
        set => b2DistanceJoint_SetStiffness(Native, value);
    }

    /// <summary>
    /// Gets or sets the linear damping in N*s/m.
    /// </summary>
    public float Damping
    {
        get => b2DistanceJoint_GetDamping(Native);
        set => b2DistanceJoint_SetDamping(Native, value);
    }

    internal DistanceJoint(object? userData) : base(userData)
    {
    }
}
