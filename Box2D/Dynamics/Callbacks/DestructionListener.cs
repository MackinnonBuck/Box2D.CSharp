namespace Box2D;

public abstract class DestructionListener
{
    protected internal virtual void SayGoodbye(Joint joint)
    {
    }

    protected internal virtual void SayGoodbye(Fixture fixture)
    {
    }
}
