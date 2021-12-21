using System.Runtime.InteropServices;

namespace Box2D;

public enum ContactFeatureType : byte
{
    Vertex = 0,
    Face = 1,
}

[StructLayout(LayoutKind.Sequential)]
public struct ContactFeature
{
    public byte IndexA { get; set; }

    public byte IndexB { get; set; }

    public ContactFeatureType TypeA { get; set; }

    public ContactFeatureType TypeB { get; set; }
}
