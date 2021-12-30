using System.Runtime.InteropServices;

namespace Box2D.Drawing;

/// <summary>
/// Represents a color for debug drawing. Each value has the range [0,1].
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Color
{
    /// <summary>
    /// Gets or sets the red component.
    /// </summary>
    public float R { get; set; }

    /// <summary>
    /// Gets or sets the green component.
    /// </summary>
    public float G { get; set; }

    /// <summary>
    /// Gets or sets the blue component.
    /// </summary>
    public float B { get; set; }

    /// <summary>
    /// Gets or sets the alpha component.
    /// </summary>
    public float A { get; set; }
    
    /// <summary>
    /// Constructs a new <see cref="Color"/> instance.
    /// </summary>
    public Color(float r, float g, float b, float a = 1f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}
