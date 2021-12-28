namespace Box2D.Dynamics.Callbacks;

public interface IDestructionListener
{
    void SayGoodbye(Joint joint);
    void SayGoodbye(Fixture fixture);
}
