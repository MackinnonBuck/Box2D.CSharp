using Box2D.Collision;

namespace Box2D.Dynamics.Callbacks;

public interface IContactListener
{
    void BeginContact(in Contact contact);
    void EndContact(in Contact contact);
    void PreSolve(in Contact contact, in Manifold oldManifold);
    void PostSolve(in Contact contact, in ContactImpulse impulse);
}
