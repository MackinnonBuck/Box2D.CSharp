namespace Box2D.Dynamics.Callbacks;

public abstract class DestructionListener
{
    protected internal virtual void SayGoodbye(Joint joint)
    {
    }

    protected internal virtual void SayGoodbye(Fixture fixture)
    {
    }
}
