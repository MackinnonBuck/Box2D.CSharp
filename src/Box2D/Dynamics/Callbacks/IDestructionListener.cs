namespace Box2D.Dynamics.Callbacks;

/// <summary>
/// Joints and fixtures are destroyed when their associated body is destroyed.
/// Implement this listener to be notified when joints and fixtures are implicitly
/// destroyed.
/// </summary>
public interface IDestructionListener
{
    /// <summary>
    /// Called when any joint is about to be destroyed due to the destruction of
    /// one of its attached bodies.
    /// </summary>
    void SayGoodbye(Joint joint);

    /// <summary>
    /// Called when any fixture is about to be destroyed due to the destruction of
    /// its parent body.
    /// </summary>
    void SayGoodbye(Fixture fixture);
}
