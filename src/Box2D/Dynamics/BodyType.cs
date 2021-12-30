namespace Box2D.Dynamics;

/// <summary>
/// The type of a body.
/// </summary>
public enum BodyType
{
    /// <summary>
    /// Zero mass, zero velocity, may be manually moved.
    /// </summary>
    Static,

    /// <summary>
    /// Zero mass, non-zero velocity set by user, moved by solver.
    /// </summary>
    Kinematic,

    /// <summary>
    /// Positive mass, non-zero velocity determined by forces, moved by solver.
    /// </summary>
    Dynamic,
}
