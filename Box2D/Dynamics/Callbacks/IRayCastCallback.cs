using System.Numerics;

namespace Box2D.Dynamics.Callbacks;

public interface IRayCastCallback
{
    float ReportFixture(Fixture fixture, Vector2 point, Vector2 normal, float fraction);
}
