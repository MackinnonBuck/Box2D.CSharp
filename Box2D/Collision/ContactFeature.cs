using System.Runtime.InteropServices;

namespace Box2D.Collision;

/// <summary>
/// The features that intersect to form the contact point.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ContactFeature
{
    /// <summary>
    /// Gets or sets the feature index on shape A.
    /// </summary>
    public byte IndexA { get; set; }

    /// <summary>
    /// Gets or sets the feature index on shape B.
    /// </summary>
    public byte IndexB { get; set; }

    /// <summary>
    /// Gets the feature type on shape A.
    /// </summary>
    public ContactFeatureType TypeA { get; set; }

    /// <summary>
    /// Gets the feature type on shape B.
    /// </summary>
    public ContactFeatureType TypeB { get; set; }
}
