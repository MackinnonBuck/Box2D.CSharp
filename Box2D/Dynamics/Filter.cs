using System.Runtime.InteropServices;

namespace Box2D.Dynamics;

[StructLayout(LayoutKind.Sequential)]
public struct Filter
{
    public ushort CategoryBits { get; set; } = 0x0001;

    public ushort MaskBits { get; set; } = 0xFFFF;

    public short GroupIndex { get; set; } = 0;
}
