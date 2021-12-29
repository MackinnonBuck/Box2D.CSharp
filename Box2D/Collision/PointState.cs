namespace Box2D.Collision;

/// <summary>
/// Used for determining the state of contact points.
/// </summary>
public enum PointState
{
    /// <summary>
    /// The point does not exist.
    /// </summary>
    Null,

    /// <summary>
    /// The point was added in the update.
    /// </summary>
    Add,

    /// <summary>
    /// The point was persisted across the update.
    /// </summary>
    Persist,

    /// <summary>
    /// The point was removed in the update.
    /// </summary>
    Remove,
}
