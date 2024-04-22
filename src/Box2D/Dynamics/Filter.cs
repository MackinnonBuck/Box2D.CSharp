using System.Runtime.InteropServices;

namespace Box2D.Dynamics;

/// <summary>
/// Holds contact filtering data.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Filter
{
    /// <summary>
    /// Gets or sets collision category bits. Normally you would just set one bit.
    /// </summary>
    public ushort CategoryBits { get; set; } = 0x0001;

    /// <summary>
    /// Gets or sets the collision mask bits. This states the categories that this
    /// shape would accept for collision.
    /// </summary>
    public ushort MaskBits { get; set; } = 0xFFFF;

    /// <summary>
    /// Gets or sets the group index.
    /// Collision groups allow a certain group of objects to never collide (negative)
    /// or always collide (positive). Zero means no collision group. Non-zero group
    /// filtering always wins against the mask bits.
    /// </summary>
    public short GroupIndex { get; set; } = 0;

    /// <summary>
    /// Constructs a new <see cref="Filter"/>
    /// </summary>
    public Filter()
    {
    }
}
