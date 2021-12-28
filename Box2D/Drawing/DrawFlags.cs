using System;

namespace Box2D.Drawing;

[Flags]
public enum DrawFlags : uint
{
    ShapeBit        = 0x0001,
    JointBit        = 0x0002,
    AabbBit         = 0x0004,
    PairBit         = 0x0008,
    CenterOfMassBit = 0x0010,
}
