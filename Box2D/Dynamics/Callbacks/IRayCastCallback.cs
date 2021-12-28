using Box2D.Math;

namespace Box2D.Dynamics.Callbacks;

public interface IRayCastCallback
{
    float ReportFixture(Fixture fixture, Vec2 point, Vec2 normal, float fraction);
}
