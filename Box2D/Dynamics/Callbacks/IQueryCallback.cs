namespace Box2D.Dynamics.Callbacks;

/// <summary>
/// Callback interface for AABB queries.
/// See <see cref="World.QueryAABB(IQueryCallback, Collision.AABB)"/>.
/// </summary>
public interface IQueryCallback
{
    /// <summary>
    /// Called for each fixture found in the query AABB.
    /// </summary>
    /// <returns><c>false</c> to terminate the query.</returns>
    bool ReportFixture(Fixture fixture);
}
