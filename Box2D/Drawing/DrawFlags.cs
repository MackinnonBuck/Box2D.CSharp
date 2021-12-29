using System;

namespace Box2D.Drawing;

/// <summary>
/// Flags used to control debug drawing.
/// </summary>
[Flags]
public enum DrawFlags : uint
{
    /// <summary>
    /// Draw shapes.
    /// </summary>
    ShapeBit        = 0x0001,

    /// <summary>
    /// Draw joint connections.
    /// </summary>
    JointBit        = 0x0002,

    /// <summary>
    /// Draw axis-aligned bounding boxes.
    /// </summary>
    AabbBit         = 0x0004,

    /// <summary>
    /// Draw broad-phase pairs.
    /// </summary>
    PairBit         = 0x0008,

    /// <summary>
    /// Draw center of mass frame.
    /// </summary>
    CenterOfMassBit = 0x0010,
}
