namespace Box2D.Dynamics.Callbacks;

public interface IQueryCallback
{
    bool ReportFixture(Fixture fixture);
}
