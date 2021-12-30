using System.Numerics;

namespace Box2D.Dynamics.Callbacks;

/// <summary>
/// Callback interface for ray casts.
/// See <see cref="World.RayCast(IRayCastCallback, Vector2, Vector2)"/>.
/// </summary>
public interface IRayCastCallback
{
    /// <summary>
    /// Called for each fixture found in the query. You control how the ray cast
    /// proceeds by returning a float.
    /// </summary>
    /// <param name="fixture">The fixture hit by the ray.</param>
    /// <param name="point">The point of initial intersection.</param>
    /// <param name="normal">The normal vector at the point of intersection.</param>
    /// <param name="fraction">Teh fraction along the ray at the point of intersection.</param>
    /// <returns>
    /// -1 to filter, 0 to terminate, fraction to clip the ray for closest hit,
    /// 1 to continue.
    /// </returns>
    float ReportFixture(Fixture fixture, Vector2 point, Vector2 normal, float fraction);
}
